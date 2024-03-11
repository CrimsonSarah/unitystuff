using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class mob : MonoBehaviour {
    [Header ("Atributes")]
    public float[] life = new float[3];
    public float[] mana = new float[3];
    public float damage, armor;
    public float attackRange = 8f;
    public float attackSpeed = 0.6f;
    private float attackDelay = 0.1f;
    public float expValue, goldValue, spawnCost, stunDuration, dislocateDuration, dodgeChance, critChance;
    public bool isStuned, dislocating, wasCalled;

    [Header ("Unity Setup")]
    public GameObject damageText;
    public GameObject healText;
    public GameObject aggroed;
    public Image healthBar;
    public wavespawner wavespawner;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private Vector3 destination, dislocateDestination;
    public float secondsPassed;

    // Start is called before the first frame update
    void Start () {
        int difficulty = GameObject.Find("Director").GetComponent<Manager>().difficulty;
        wavespawner = GameObject.Find("Director").GetComponent<wavespawner>();
        healthBar.enabled = false;
        switch (difficulty) {
            case 1:
                if (wavespawner.waveindex >= 10) {
                    life[0] = life[0] * wavespawner.waveindex;
                    mana[0] = mana[0] * wavespawner.waveindex;
                    damage = damage * wavespawner.waveindex;
                    expValue = expValue * (wavespawner.waveindex * 1.35f);
                    goldValue = goldValue * wavespawner.waveindex;
                }
                else if (wavespawner.waveindex == 0) {
                    life[0] = life[0] * 0.55f;
                    mana[0] = mana[0] * 0.55f;
                    damage = damage * 0.55f;
                    expValue = expValue * 0.55f;
                    goldValue = goldValue / 2;
                }
                else {
                    life[0] = life[0] * (wavespawner.waveindex * 0.75f);
                    mana[0] = mana[0] * (wavespawner.waveindex * 0.75f);
                    damage = damage * (wavespawner.waveindex * 0.75f);
                    expValue = expValue * (wavespawner.waveindex * 1.05f);
                    goldValue = goldValue * wavespawner.waveindex;
                }
                break;
            case 3:
                if (wavespawner.waveindex >= 10) {
                    life[0] = life[0] * (wavespawner.waveindex * 1.65f);
                    mana[0] = mana[0] * (wavespawner.waveindex * 1.65f);
                    damage = damage * (wavespawner.waveindex * 1.65f);
                    expValue = expValue * (wavespawner.waveindex * 1.35f);
                    goldValue = goldValue * wavespawner.waveindex;
                }
                else if (wavespawner.waveindex == 0) {
                    expValue = expValue * 0.55f;
                    goldValue = goldValue / 2;
                }
                else {
                    life[0] = life[0] * (wavespawner.waveindex * 1.35f);
                    mana[0] = mana[0] * (wavespawner.waveindex * 1.35f);
                    damage = damage * (wavespawner.waveindex * 1.35f);
                    expValue = expValue * (wavespawner.waveindex * 1.05f);
                    goldValue = goldValue * wavespawner.waveindex;
                }
                break;
            default:
                if (wavespawner.waveindex >= 10) {
                    life[0] = life[0] * (wavespawner.waveindex * 1.25f);
                    mana[0] = mana[0] * (wavespawner.waveindex * 1.25f);
                    damage = damage * (wavespawner.waveindex * 1.25f);
                    expValue = expValue * (wavespawner.waveindex * 1.25f);
                    goldValue = goldValue * wavespawner.waveindex;
                } else if (wavespawner.waveindex == 0) {
                    life[0] = life[0] * 0.65f;
                    mana[0] = mana[0] * 0.65f;
                    damage = damage * 0.65f;
                    expValue = expValue * 0.65f;
                    goldValue = goldValue / 2;
                } else {
                    life[0] = life[0] * (wavespawner.waveindex * 1.15f);
                    mana[0] = mana[0] * (wavespawner.waveindex * 1.15f);
                    damage = damage * (wavespawner.waveindex * 1.15f);
                    expValue = expValue * (wavespawner.waveindex * 1.15f);
                    goldValue = goldValue * wavespawner.waveindex;
                }
                break;
        }
        life[1] = life[0];
        mana[1] = mana[0];
        agent = GetComponent<NavMeshAgent> ();
        rb = GetComponent<Rigidbody> ();
        destination = Vector3.zero;
    }

    // Update is called once per frame
    void Update () {
        if (stunDuration <= 0) {
            isStuned = false;
            agent.isStopped = false;
        }

        if (dislocateDuration <= 0) {
            dislocating = false;
            agent.isStopped = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        //Caso a vida do inimigo esteja em ou abaixo de zero, o objeto é destruído
        if (life[1] <= 0)
        {
            if (GameObject.FindGameObjectWithTag("Player")) {
                GameObject.FindGameObjectWithTag("Player").GetComponent<player>().characterExp[1] -= -expValue;
                GameObject.FindGameObjectWithTag("Player").GetComponent<player>().gold -= -goldValue;
            }
            GameObject.Destroy(gameObject);
        }

        //Regenera mana
        if (mana[1] < mana[0] && Time.timeScale == 1f) {
            mana[1] -= -mana[2] / 100f;
            if (mana[1] > mana[0]) mana[1] = mana[0];
        }

        if (life[1] < life[0] && Time.timeScale == 1f) {
            life[1] -= -life[2] / 100f;
            if (life[1] > life[0]) life[1] = life[0];
        }

        //Se a vida do inimigo não estiver no máximo habilita a barra de vida
        if (life[1] < life[0]) healthBar.enabled = true;


        //Atualiza a barra de vida
        healthBar.fillAmount = life[1] / life[0];

        //Garante que não vai continuar aggrando algo morto
        if (aggroed == null && wasCalled) aggroed = GameObject.Find("castleCore");
        
        if (!isStuned && !dislocating){
            UpdateTarget();

            //Ataca o alvo
            if (attackDelay <= 0f) {
                EnemyBasicAttack ();
                attackDelay = 1 / attackSpeed;
            }

            //Diminui o delay para o próximo ataque
            attackDelay -= Time.deltaTime;
        } else if (life[1] > 0) {
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(transform.position, transform.position, NavMesh.AllAreas, path);
            if (agent.isActiveAndEnabled) agent.SetPath(path);
            agent.isStopped = true;
            stunDuration -= Time.deltaTime;
            dislocateDuration -= Time.deltaTime;
        }

        secondsPassed -=- Time.deltaTime;
    }

    //Atualiza o alvo e a rota até o alvo do mob
    void UpdateTarget () {
        float secondsNeeded = Random.Range(0.75f, 2.5f);

        if (aggroed != null) {
            if (aggroed.name == "castleCore") {
                if (Vector3.Distance (aggroed.transform.position, transform.position) > attackRange + 17.5f) {
                    NavMeshPath path = new NavMeshPath ();
                    NavMesh.CalculatePath (transform.position, aggroed.transform.position, NavMesh.AllAreas, path);
                    if (agent.isActiveAndEnabled) agent.SetPath (path);
                }
            } else if (Vector3.Distance (aggroed.transform.position, transform.position) > attackRange + 4.5f) {
                NavMeshPath path = new NavMeshPath ();
                NavMesh.CalculatePath (transform.position, aggroed.transform.position, NavMesh.AllAreas, path);
                if (agent.isActiveAndEnabled) agent.SetPath (path);
            }
        } else if (secondsPassed >= secondsNeeded) {
            destination = new Vector3((transform.position.x + Random.Range(-10f, 10f)), transform.position.y, (transform.position.z + Random.Range(-10f, 10f)));
            NavMeshPath path = new NavMeshPath ();
            NavMesh.CalculatePath (transform.position, destination, NavMesh.AllAreas, path);
            if (agent.isActiveAndEnabled) agent.SetPath(path);
            secondsPassed = 0f;
        }
    }

    //Verifica se o player está no range do aggro, caso sim, o player se torna o alvo do mob
    void OnTriggerEnter (Collider other) {
        if (other.tag == "Player") aggroed = other.transform.root.gameObject;
    }
    void OnTriggerExit (Collider other) {
        if (other.tag == "Player" && wasCalled) aggroed = GameObject.Find ("castleCore");
        else if (other.tag == "Player") aggroed = null;
    }

    //Causa dano ao alvo caso ele esteja dentro do range de ataque
    void EnemyBasicAttack () {
        if (aggroed != null) {
            transform.LookAt(aggroed.transform);
            if (aggroed.tag == "Player") {
                if (Vector3.Distance (aggroed.transform.position, transform.position) <= attackRange + 4.5f) {
                    if (Random.Range(0, 100) < critChance) aggroed.GetComponent<player>().TakeDamage((damage - (damage * aggroed.GetComponent<player>().armor / 100)) * 2.5f, 0);
                    else aggroed.GetComponent<player> ().TakeDamage (damage - (damage * aggroed.GetComponent<player> ().armor / 100), 0);
                    if (agent.isActiveAndEnabled) agent.destination = transform.position;
                }
            }
            if (aggroed.tag == "Castle Core") {
                if (Vector3.Distance (aggroed.transform.position, transform.position) <= attackRange + 17.5f) {
                    if (Random.Range(0, 100) < critChance) aggroed.GetComponent<castle> ().health[1] -= ((damage - (damage * aggroed.GetComponent<castle> ().armor / 100)) * 2.5f);
                    aggroed.GetComponent<castle> ().health[1] -= damage - (damage * aggroed.GetComponent<castle> ().armor / 100);
                    if (agent.isActiveAndEnabled) agent.destination = transform.position;
                }
            }
        }
    }

    public void TakeDamage(float damage, int i) {
        GameObject go;
        switch (i) {
            case 1:
                go = GameObject.Instantiate(damageText, transform.position, new Quaternion(1, 0, 0, 1));
                go.GetComponent<TextMesh>().text = damage.ToString("N0");
                life[1] -= damage;
                break;
            default:
                if (Random.Range(0, 100) < dodgeChance) {
                    go = GameObject.Instantiate(damageText, transform.position, new Quaternion(1, 0, 0, 1));
                    go.GetComponent<TextMesh>().text = "DODGE";
                }
                else {
                    go = GameObject.Instantiate(damageText, transform.position, new Quaternion(1, 0, 0, 1));
                    go.GetComponent<TextMesh>().text = damage.ToString("N0");
                    life[1] -= damage;
                }
                break;
        }
    }

    public void Heal (float healing) {
        GameObject go = GameObject.Instantiate (healText, transform.position, new Quaternion (1, 0, 0, 1));
        go.GetComponent<TextMesh> ().text = healing.ToString ("N0");
        life[1] -= -healing;
    }

    public void Dislocate (float duration, Vector3 target) {
        if (life[0] > 0) {
            agent.isStopped = true;
            dislocating = true;
            dislocateDuration = duration;
            dislocateDestination = target;
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY;
            rb.AddForce(target.normalized * 75, ForceMode.Impulse);
        }
    }
}