using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// fazer com que possamos mudar as funções NSkill (qualquer skill) para cada character, fazer com que skills que exijam o player clicar em algo (ex.: a skill irá causar dano em área onde o player clicar) possam ser devidamente editadas para cada character (ex.: linha 102), adicionar váriaveis de duração e tick de skills overtime (ex.: olhar as variaveis do script boss.cs)

public class player : MonoBehaviour {
    [Header ("Attributes")]
    public float[] characterHealth = new float[3]; //[0]MaxHealth, [1]ActualHealth, [2]HealthRegen
    public float[] characterMana = new float[3]; //[0]MaxMana, [1]ActualMana, [2]ManaRegen
    public float[] characterAttributes = new float[3]; //[0]Strength, [1]Dexterity, [2]Intelligence
    public float[] characterExp = new float[3]; //[0]MaxExp, [1]ActualExp, [2]Level
    public float[] skillCooldown = new float[5]; //[0]QCooldown, [1]WCooldown, [2]ECooldown, [3]RCooldown, [4]PCooldown
    public float[] skillMaxCooldown = new float[5]; //[0]QMaxCooldown, [1]WMaxCooldown, [2]EMaxCooldown, [3]RMaxCooldown, [4]PMaxCooldown
    public float[, ] skillPower = new float[2, 5]; //[0]QPower, [1]WPower, [2]EPower, [3]RPower, [4]PPower
    public float[] skillManaCost = new float[4]; //[0]QManaCost, [1]WManaCost, [2]EManaCost, [3]RManaCost
    public float[] skillRange = new float[5]; //[0]QRange, [1]WRange, [2]ERange, [3]RRange, [4]PRange
    public float[] skillRadius = new float[5]; //[0]QRadius, [1]WRadius, [2]ERadius, [3]RRadius, [4]PRadius
    public float[] delayCounters = new float[3]; //[0]HealthRegenDelay, [1]ManaRegenDelay, [2]BasicAttackDelay
    public int[] skillLevel = new int[5]; //[0]QLevel, [1]WLevel, [2]ELevel, [3]RLevel, [4]PLevel
    public bool[] skillEnabled = new bool[5]; //[0]QEnabled, [1]WEnabled, [2]EEnabled, [3]REnabled, [4]PEnabled
    public bool AAMove, AA, isStuned;
    public float armor, basicDamage, attackRange, attackSpeed, gold, stunDuration, critChance, dodgeChance, lifesteal;

    [Header ("Unity Setup")]
    public Transform firePoint;
    public int charInstance, selectedChar;
    public GameObject damageText, healText, castleBox, shopBox, upgradeBox, enemyBox, target, movementParticle, attackMovementParticle;
    public NavMeshAgent agent;
    public characters characters;
    public skills skills;
    public bool janasceu;
    public Items Items;
    public spawn spawn;
    public Animator anim;

    void Start () {
        characters = GameObject.Find("Director").GetComponent<characters> ();
        //Atribui valores a variáveis necessárias no Update
        charInstance = 0;
        
        agent = GetComponent<NavMeshAgent> ();
        anim = GetComponent<Animator> ();
        target = null;

        if (!janasceu) {
            for (int i = 0; i < characters.charList.Count; i++) {
                if (characters.charList[i].IsOn) {
                    selectedChar = i;
                    break;
                }
            }
            characterHealth = characters.charList[selectedChar].CharHealth;
            characterMana = characters.charList[selectedChar].CharMana;
            characterAttributes = characters.charList[selectedChar].CharAttributes;
            characterExp = characters.charList[selectedChar].CharExp;
            skillCooldown = characters.charList[selectedChar].SkillCd;
            skillMaxCooldown = characters.charList[selectedChar].SkillMaxCd;
            skillPower = characters.charList[selectedChar].SkillPwr;
            skillManaCost = characters.charList[selectedChar].SkillCost;
            skillRange = characters.charList[selectedChar].SkillRange;
            skillRadius = characters.charList[selectedChar].SkillRadius;
            skillEnabled = characters.charList[selectedChar].SkillEnabled;
            armor = characters.charList[selectedChar].CharArmor;
            basicDamage = characters.charList[selectedChar].CharBasicDamage;
            attackRange = characters.charList[selectedChar].CharAttackRange;
            attackSpeed = characters.charList[selectedChar].CharAttackSpeed;
            critChance = characters.charList[selectedChar].CharCritChance;
            dodgeChance = characters.charList[selectedChar].CharDodgeChance;
            lifesteal = characters.charList[selectedChar].CharLifesteal;
        }

        gameObject.name = characters.charList[selectedChar].Name;
    }

    // Update is called once per frame
    void Update () {
        if (Mathf.Round(Vector3.Distance(agent.destination, transform.position)) <= 7f) anim.Play("Idle");

        if (stunDuration > 0) {
            isStuned = true;
            agent.destination = transform.position;
        } else isStuned = false;

        //Faz com que o player comece no nível um (1)
        if (characterExp[2] == 0) LevelUp ();

        //Caso o player tenha morrido, o objeto é destruído
        if (characterHealth[1] <= 0) {
            anim.Play("Death");
            spawn.Die();
        }
        
        //Regenera a vida do player periodicamente
        if (delayCounters[0] <= 0f && characterHealth[1] < characterHealth[0] && Time.timeScale == 1f) characterHealth[1] -= -characterHealth[2] / 100f;

        if (characterHealth[1] > characterHealth[0]) characterHealth[1] = characterHealth[0];

        //Regenera a mana do player periodicamente
        if (delayCounters[1] <= 0f && characterMana[1] < characterMana[0] && Time.timeScale == 1f) characterMana[1] -= -characterMana[2] / 100f;
        
        if (characterMana[1] > characterMana[0]) characterMana[1] = characterMana[0];

        //Caso o player aperte Q, ativa a primeira habilidade
        switch (characters.charList[selectedChar].SkillType[charInstance, 0]) {
            case 0:
                skills.UseSkill (characters.charList[selectedChar].NSkill[0]);
                break;
            case 1:
                if (skillCooldown[0] <= 0) skills.UseSkill (characters.charList[selectedChar].NSkill[0]);
                break;
            case 4:
                if (Input.GetKeyDown("q") && !isStuned) skills.UseSkill(characters.charList[selectedChar].NSkill[0]);
                if (Input.GetKey("left ctrl") && Input.GetKeyDown("q") && !isStuned) {
                    RaycastHit hit = new RaycastHit();
                    hit.point = transform.position;
                    skills.SkillClicked(characters.charList[selectedChar].NSkill[0], hit, true);
                } else if (Input.GetKey("left shift") && Input.GetKeyUp("q") && !isStuned) {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit)) skills.SkillClicked(characters.charList[selectedChar].NSkill[0], hit, false);
                }
                break;
            default:
                if (Input.GetKeyDown ("q") && !isStuned) skills.UseSkill (characters.charList[selectedChar].NSkill[0]);
                break;
        }

        //Caso o player aperte W, ativa a segunda habilidade
        switch (characters.charList[selectedChar].SkillType[charInstance, 1]) {
            case 0:
                skills.UseSkill (characters.charList[selectedChar].NSkill[1]);
                break;
            case 1:
                if (skillCooldown[1] <= 0) skills.UseSkill (characters.charList[selectedChar].NSkill[1]);
                break;
            case 4:
                if (Input.GetKeyDown("w") && !isStuned) skills.UseSkill(characters.charList[selectedChar].NSkill[1]);
                if (Input.GetKey("left ctrl") && Input.GetKeyDown("w") && !isStuned) {
                    RaycastHit hit = new RaycastHit();
                    hit.point = transform.position;
                    skills.SkillClicked(characters.charList[selectedChar].NSkill[1], hit, true);
                }
                else if (Input.GetKey("left shift") && Input.GetKeyUp("w") && !isStuned) {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit)) skills.SkillClicked(characters.charList[selectedChar].NSkill[1], hit, false);
                }
                break;
            default:
                if (Input.GetKeyDown ("w") && !isStuned) skills.UseSkill (characters.charList[selectedChar].NSkill[1]);
                break;
        }

        //Caso o player aperte E, ativa a terceira habilidade
        switch (characters.charList[selectedChar].SkillType[charInstance, 2]) {
            case 0:
                skills.UseSkill (characters.charList[selectedChar].NSkill[2]);
                break;
            case 1:
                if (skillCooldown[2] <= 0) skills.UseSkill (characters.charList[selectedChar].NSkill[2]);
                break;
            case 4:
                if (Input.GetKeyDown("e") && !isStuned) skills.UseSkill(characters.charList[selectedChar].NSkill[2]);
                if (Input.GetKey("left ctrl") && Input.GetKeyDown("e") && !isStuned) {
                    RaycastHit hit = new RaycastHit();
                    hit.point = transform.position;
                    skills.SkillClicked(characters.charList[selectedChar].NSkill[2], hit, true);
                }
                else if (Input.GetKey("left shift") && Input.GetKeyUp("e") && !isStuned) {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit)) skills.SkillClicked(characters.charList[selectedChar].NSkill[2], hit, false);
                }
                break;
            default:
                if (Input.GetKeyDown ("e") && !isStuned) skills.UseSkill (characters.charList[selectedChar].NSkill[2]);
                break;
        }

        //Caso o player aperte R, ativa a quarta habilidade
        switch (characters.charList[selectedChar].SkillType[charInstance, 3]) {
            case 0:
                skills.UseSkill (characters.charList[selectedChar].NSkill[3]);
                break;
            case 1:
                if (skillCooldown[3] <= 0) skills.UseSkill (characters.charList[selectedChar].NSkill[3]);
                break;
            case 4:
                if (Input.GetKeyDown("r") && !isStuned) skills.UseSkill(characters.charList[selectedChar].NSkill[3]);
                if (Input.GetKey("left ctrl") && Input.GetKeyDown("r") && !isStuned) {
                    RaycastHit hit = new RaycastHit();
                    hit.point = transform.position;
                    skills.SkillClicked(characters.charList[selectedChar].NSkill[3], hit, true);
                }
                else if (Input.GetKey("left shift") && Input.GetKeyUp("r") && !isStuned) {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit)) skills.SkillClicked(characters.charList[selectedChar].NSkill[3], hit, false);
                }
                break;
            default:
                if (Input.GetKeyDown ("r") && !isStuned) skills.UseSkill (characters.charList[selectedChar].NSkill[3]);
                break;
        }

        //Caso habilidade passiva do player esteja fora do cooldown, ela é usada
        switch (characters.charList[selectedChar].SkillType[charInstance, 4]) {
            case 0:
                skills.UseSkill (characters.charList[selectedChar].NSkill[4]);
                break;
            case 1:
                if (skillCooldown[4] <= 0) skills.UseSkill (characters.charList[selectedChar].NSkill[4]);
                break;
            default:
                break;
        }

        //Atualiza a hud do alvo
        enemyBox.SetActive (target != null);

        //Faz com que o player suba de nível caso tenha experiência o suficiente
        if (characterExp[1] >= characterExp[0]) {
            if (characterExp[2] == 99) characterExp[1] = characterExp[0];
            else {
                characterExp[1] -= characterExp[0];
                LevelUp ();
            }
        }

        //Faz com que todos cooldowns e timers diminuam
        delayCounters[0] -= Time.deltaTime;
        delayCounters[1] -= Time.deltaTime;
        delayCounters[2] -= Time.deltaTime;
        skillCooldown[0] -= Time.deltaTime;
        skillCooldown[1] -= Time.deltaTime;
        skillCooldown[2] -= Time.deltaTime;
        skillCooldown[3] -= Time.deltaTime;
        skillCooldown[4] -= Time.deltaTime;

        if (!isStuned) {

            if (Input.GetKeyDown ("s")) {
                anim.Play("Idle");
                agent.destination = transform.position;
            }

            if (Input.GetKeyDown ("a")) AAMove = true;

            if (Input.GetKeyDown ("m")) {
                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast (ray, out hit)) {
                    transform.position = hit.point;
                }
            }

            //Permite que o player utilize ou cancele suas habilidades ao clicar com o botão esquerdo do mouse
            if (Input.GetMouseButtonDown (0)) {
                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast (ray, out hit)) {
                    if (AAMove) {
                        if (hit.collider.tag == "Ground") {
                            target = null;
                            agent.destination = hit.point;
                            GameObject[] movementParticles = GameObject.FindGameObjectsWithTag("Movement Particle");
                            for(int i = 0 ; i < movementParticles.Length ; i ++) {
                                GameObject.Destroy(movementParticles[i]);
                            }
                            anim.Play("Walking");
                            Instantiate(attackMovementParticle, new Vector3(hit.point.x, hit.point.y + 2f, hit.point.z), Quaternion.identity);
                            AA = true;
                            AAMove = false;
                        }
                        if (hit.collider.tag == "Mob") {
                            target = hit.collider.gameObject;
                            agent.destination = hit.point;
                            AAMove = false;
                        }
                    }
                    if (characters.charList[selectedChar].SkillType[charInstance, 0] == 4) skills.SkillClicked (characters.charList[selectedChar].NSkill[0], hit, false);
                    if (characters.charList[selectedChar].SkillType[charInstance, 1] == 4) skills.SkillClicked (characters.charList[selectedChar].NSkill[1], hit, false);
                    if (characters.charList[selectedChar].SkillType[charInstance, 2] == 4) skills.SkillClicked (characters.charList[selectedChar].NSkill[2], hit, false);
                    if (characters.charList[selectedChar].SkillType[charInstance, 3] == 4) skills.SkillClicked (characters.charList[selectedChar].NSkill[3], hit, false);

                    if (hit.collider.tag == "Castle") {
                        for (int i = 0; i < skillEnabled.Length; i++) {
                            skillEnabled[i] = false;
                        }
                        castleBox.SetActive (true);
                        shopBox.SetActive (false);
                        upgradeBox.SetActive (false);
                    }
                    if (hit.collider.tag == "Shopkeeper") {
                        for (int i = 0; i < skillEnabled.Length; i++) {
                            skillEnabled[i] = false;
                        }
                        shopBox.SetActive (true);
                        upgradeBox.SetActive (false);
                        castleBox.SetActive (false);
                    }
                    if (hit.collider.tag == "Upgradekeeper") {
                        for (int i = 0; i < skillEnabled.Length; i++) {
                            skillEnabled[i] = false;
                        }
                        upgradeBox.SetActive (true);
                        castleBox.SetActive (false);
                        shopBox.SetActive (false);
                    }
                }
            }

            //Ativa a partícula que mostra onde o player vai ir
            if (Input.GetMouseButtonDown(1)) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) {
                    if (hit.collider.tag == "Ground") {
                        GameObject[] movementParticles = GameObject.FindGameObjectsWithTag("Movement Particle");
                        for(int i = 0 ; i < movementParticles.Length ; i ++) {
                            Destroy(movementParticles[i]);
                        }
                        anim.Play("Walking");
                        Instantiate(movementParticle, new Vector3(hit.point.x, hit.point.y + 2f, hit.point.z), Quaternion.identity);
                    }
                }
            }

            //Faz com que o player ande até o local ou alvo que ele tenha clicado com o botão direito do mouse
            if (Input.GetMouseButton (1)) {
                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast (ray, out hit)) {
                    if (hit.collider.tag == "Ground") {
                        target = null;
                        agent.destination = hit.point;
                        AAMove = false;
                        if (Input.GetMouseButtonDown(1)) {
                            GameObject[] movementParticles = GameObject.FindGameObjectsWithTag("Movement Particle");
                            for(int i = 0 ; i < movementParticles.Length ; i ++) {
                                Destroy(movementParticles[i]);
                            }
                            anim.Play("Walking");
                            Instantiate(movementParticle, new Vector3(hit.point.x, hit.point.y + 2f, hit.point.z), Quaternion.identity);
                        }
                    }
                    if (hit.collider.tag == "Mob") {
                        target = hit.collider.gameObject;
                        agent.destination = hit.point;
                    }
                    for (int i = 0; i < skillEnabled.Length; i++) {
                        skillEnabled[i] = false;
                    }
                }
            }

            if (AA) {
                Collider[] colliders = Physics.OverlapSphere (gameObject.transform.position, attackRange);
                float closerDistance;
                if (attackRange + Items.Effect[6] < 25f) closerDistance = 25f;
                else closerDistance = attackRange + Items.Effect[6];
                GameObject futureTarget = null;
                foreach (Collider nearbyObject in colliders) {
                    Rigidbody rb = nearbyObject.GetComponent<Rigidbody> ();
                    if (rb != null && rb.transform.root.tag == "Mob") {
                        if (Vector3.Distance (transform.position, rb.transform.position) < closerDistance) {
                            closerDistance = Vector3.Distance (transform.position, rb.transform.position);
                            futureTarget = rb.transform.root.gameObject;
                        }
                    }
                }
                target = futureTarget;
                if (target != null) {
                    AA = false;
                    agent.destination = target.transform.position;
                }
            }

            if (agent.destination == transform.position || target != null) AAMove = false;

            if (target != null) {
                if (Vector3.Distance (target.transform.position, transform.position) > attackRange + Items.Effect[6]) agent.destination = target.transform.position;
                else if (delayCounters[2] <= 0f && target != null && agent.isActiveAndEnabled) {
                    agent.destination = transform.position;
                    Attack();
                }
            }

        } else stunDuration -= Time.deltaTime;
    }

    //Caso o player tenha experiência o suficiente para subir de nível, ele sobe, a menos que ele esteja no nível 99 
    void LevelUp () {
        characterExp[2]++;
        characterExp[0] = ((characterExp[0] / 1.5f) * 2f) + characterExp[2] * 25f;
        characterAttributes[0] -= -characters.charList[selectedChar].CharAttributePerLvl[0];
        characterAttributes[1] -= -characters.charList[selectedChar].CharAttributePerLvl[1];
        characterAttributes[2] -= -characters.charList[selectedChar].CharAttributePerLvl[2];
        characterHealth[0] -= -characters.charList[selectedChar].CharAttributePerLvl[0] * 10f;
        characterHealth[2] -= -characters.charList[selectedChar].CharAttributePerLvl[0] * 0.1f;
        characterMana[0] -= -characters.charList[selectedChar].CharAttributePerLvl[2] * 6f;
        characterMana[2] -= -characters.charList[selectedChar].CharAttributePerLvl[2] * 0.05f;
        characterHealth[1] = characterHealth[0];
        characterMana[1] = characterMana[0];
        armor -= -characters.charList[selectedChar].CharAttributePerLvl[1] * 0.2f;
        if (armor > 99f) armor = 99f;
        basicDamage -= -(characters.charList[selectedChar].CharAttributePerLvl[1] + characters.charList[selectedChar].CharAttributePerLvl[0]) * 1.25f;
        attackSpeed -= -characters.charList[selectedChar].CharAttributePerLvl[1] * 0.01f;
        if (attackSpeed > 3.5f) attackSpeed = 3.5f;
    }

    public void TakeDamage (float damage, int i) {
        GameObject go;
        switch (i) {
            case 1:
                go = GameObject.Instantiate(damageText, transform.position, new Quaternion(1, 0, 0, 1));
                go.GetComponent<TextMesh>().text = damage.ToString("N0");
                characterHealth[1] -= damage;
                break;
            default:
                if (Random.Range(0, 100) < dodgeChance) {
                    go = GameObject.Instantiate(damageText, transform.position, new Quaternion(1, 0, 0, 1));
                    go.GetComponent<TextMesh>().text = "DODGE";
                } else {
                    go = GameObject.Instantiate (damageText, transform.position, new Quaternion (1, 0, 0, 1));
                    go.GetComponent<TextMesh> ().text = damage.ToString ("N0");
                    characterHealth[1] -= damage;
                }
                break;
        }
    }

    public void Heal (float healing) {
        GameObject go = GameObject.Instantiate (healText, transform.position, new Quaternion (1, 0, 0, 1));
        go.GetComponent<TextMesh> ().text = healing.ToString ("N0");
        characterHealth[1] -= -healing;
    }

    private void Attack() {
        if (charInstance == 0) {
            int atknum = Random.Range(0, 2);
            switch (atknum) {
                case 0:
                    anim.Play("Attack1");
                    anim.speed = attackSpeed;
                    break;
                case 1:
                    anim.Play("Attack2");
                    anim.speed = attackSpeed;
                    break;
                default:
                    break;
            }
        } else if (charInstance == 1) {
            int atknum = Random.Range(0, 2);
            switch (atknum) {
                case 0:
                    anim.Play("Attack3");
                    anim.speed = attackSpeed;
                    break;
                case 1:
                    anim.Play("Attack4");
                    anim.speed = attackSpeed;
                    break;
                default:
                    break;
            }
        }
    }

    private void SetAnimationSpeed (float speed) {
        anim.speed = speed;
    }
}