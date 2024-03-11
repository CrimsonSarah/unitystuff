using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class skills : MonoBehaviour {
    private GameObject SirenaNearestSphere;
    public List<GameObject> SirenaSpheres = new List<GameObject>();
    private float bonusArmor;
    public player player;
    public Items Items;
    public Image[] skillButton = new Image[5];
    [SerializeField]
    private Sprite[] HogIcons = new Sprite[6];
    public GameObject HogProjectile, DefaultRProjectile, SirenaSphere, SirenaBasicAttack;
    public string[,,] tooltiptext = new string[2,3,10];

    void LateUpdate () {
        if (Input.GetKeyDown("right alt") || Input.GetKeyDown("left alt") || Input.GetKeyUp("right alt") || Input.GetKeyUp("left alt")) {
            skillButton[0].transform.GetChild(0).gameObject.GetComponent<Image>().raycastTarget = false;
            skillButton[1].transform.GetChild(0).gameObject.GetComponent<Image>().raycastTarget = false;
            skillButton[2].transform.GetChild(0).gameObject.GetComponent<Image>().raycastTarget = false;
            skillButton[3].transform.GetChild(0).gameObject.GetComponent<Image>().raycastTarget = false;
            skillButton[4].GetComponent<Image>().raycastTarget = false;
            StartCoroutine(updateTooltip());
            IEnumerator updateTooltip() {
                yield return 0;
                skillButton[0].transform.GetChild(0).gameObject.GetComponent<Image>().raycastTarget = true;
                skillButton[1].transform.GetChild(0).gameObject.GetComponent<Image>().raycastTarget = true;
                skillButton[2].transform.GetChild(0).gameObject.GetComponent<Image>().raycastTarget = true;
                skillButton[3].transform.GetChild(0).gameObject.GetComponent<Image>().raycastTarget = true;
                skillButton[4].GetComponent<Image>().raycastTarget = true;
            }
        }
        if (Input.GetKey("right alt") || Input.GetKey("left alt")) {
            tooltiptext[0,1,0] = "?";
            tooltiptext[1,1,0] = "Hog arremessa seu martelo em uma área alvo causando atordoamento por 1/2/3/4/5s e 5/10/25/75/150 + (Str + Dex) de dano aos inimigos atingidos." + "\n" + "O seu martelo irá ficar no chão até ser recolhido.";
            tooltiptext[0,1,1] = "?";
            tooltiptext[1,1,1] = "Hog ganha 1 de armadura para cada 1% de vida perdido até um máximo de 4/6/10/12/16 de armadura adicional.";
            tooltiptext[0,1,2] = "?";
            tooltiptext[1,1,2] = "Hog solta um grito de guerra aumentando o dano de ataque de seus aliados em 10/15/20/25/30% por 2/3/4/8/10s.";
            tooltiptext[0,1,3] = "?";
            tooltiptext[1,1,3] = "Hog dá uma batida no chão extremamente forte com seu martelo, inimigos pegos pela onde de choque sofrem 135/310/585 + (Str + Dex) de dano." + "\n" + "O martelo de Hog irá ficar preso no chão por 6/4/2s.";
            tooltiptext[0,1,4] = "?";
            tooltiptext[1,1,4] = "Hog usa seu martelo para atacar, causando dano em área e tendo seu dano diminuido em 45/42/39/36/33%.";
            tooltiptext[0,1,5] = "?";
            tooltiptext[1,1,5] = "Hog arremessa seu martelo em uma área alvo causando atordoamento por 1/2/3/4/5s e 5/10/25/75/150 + (Str + Dex) de dano aos inimigos atingidos." + "\n" + "O seu martelo irá ficar no chão até ser recolhido.";
            tooltiptext[0,1,6] = "?";
            tooltiptext[1,1,6] = "Hog ganha 1 de armadura para cada 1% de vida perdido até um máximo de 4/6/10/12/16 de armadura adicional.";
            tooltiptext[0,1,7] = "?";
            tooltiptext[1,1,7] = "Hog solta um grito de guerra aumentando a regeneração de vida dos seus aliados em 20/30/40/50/60 durante 4/5/6/7/10s.";
            tooltiptext[0,1,8] = "?";
            tooltiptext[1,1,8] = "Hog entra em estado de fúria ganhando 25/50/75% de dano de ataque e 25/50/75% de roubo de vida por 4/8/12s.";
            tooltiptext[0,1,9] = "?";
            tooltiptext[1,1,9] = "Hog usa suas mãos para atacar, ganhando 10/20/30/40/60% de velocidade de ataque e causando seu dano completo.";
            tooltiptext[0,2,0] = "?";
            tooltiptext[1,2,0] = "Sirena cria uma esfera no local alvo, causando 10/20/30/40/50 + (20% Int) de dano aos inimigos na área. Sirena pode manter 3/5/7/9/11 esferas em campo. Criar uma esfera além do limite fará com que a primeira desapareça.";
            tooltiptext[0,2,1] = "?";
            tooltiptext[1,2,1] = "Sirena usa sua esfera ativa para puxar inimigos e causar 12.5/25/50/100/150 + (80% Int) de dano a eles.";
            tooltiptext[0,2,2] = "?";
            tooltiptext[1,2,2] = "Sirena usa sua esfera ativa para repelir inimigos e curar aliados na área em 20/40/60/80/100 + (250% Int).";
            tooltiptext[0,2,3] = "?";
            tooltiptext[1,2,3] = "Sirena sobrecarrega todas esferas em campo, fazendo com que elas puxem inimigos e causem 12.5/25/50/100/150 + (80% Int) de dano a eles, e repele os mesmos logo após. Aliados na área são curados em 20/40/60/80/100 + (250% Int).";
            tooltiptext[0,2,4] = "?";
            tooltiptext[1,2,4] = "Sirena solta raios em seus ataques básicos, os raios desencadeiam 3/7/11/15/19 vezes entre seus inimigos (é possível o raio desencadear em um mesmo inimigo). Raios causam metade do dano de ataque.";
        } else {
            tooltiptext[0,1,0] = "?";
            tooltiptext[1,1,0] = "Hog arremessa seu martelo em uma área alvo causando atordoamento por " + player.skillPower[0, 2].ToString("N0") + " segundos e " + player.skillPower[0, 1].ToString("N0") + " + " + (player.characterAttributes[0] + player.characterAttributes[1]).ToString("N0") + " de dano aos inimigos atingidos." + "\n" + "O seu martelo irá ficar no chão até ser recolhido.";
            tooltiptext[0,1,1] = "?";
            tooltiptext[1,1,1] = "Hog ganha 1 de armadura para cada 1% de vida perdido até um máximo de " + player.skillPower[0, 3].ToString("N0") + " de armadura adicional.";
            tooltiptext[0,1,2] = "?";
            tooltiptext[1,1,2] = "Hog solta um grito de guerra aumentando o dano de ataque de seus aliados em " + player.skillPower[0, 4].ToString("N0") + "% por " + player.skillPower[0, 5].ToString("N0") + " segundos.";
            tooltiptext[0,1,3] = "?";
            tooltiptext[1,1,3] = "Hog dá uma batida no chão extremamente forte com seu martelo, inimigos pegos pela onde de choque sofrem " + player.skillPower[0,6] + " + " + (player.characterAttributes[0] + player.characterAttributes[1]).ToString("N0") + " de dano." + "\n" + "O martelo de Hog irá ficar preso no chão por " + player.skillPower[0,7].ToString("N0") + " segundos.";
            tooltiptext[0,1,4] = "?";
            tooltiptext[1,1,4] = "Hog usa seu martelo para atacar, causando dano em área e tendo seu dano diminuido em " + player.skillPower[0, 0].ToString("N0") + "%.";
            tooltiptext[0,1,5] = "?";
            tooltiptext[1,1,5] = "Hog arremessa seu martelo em uma área alvo causando atordoamento por " + player.skillPower[0, 2].ToString("N0") + " segundos e " + player.skillPower[0, 1].ToString("N0") + " + " + (player.characterAttributes[0] + player.characterAttributes[1]).ToString("N0") + " de dano aos inimigos atingidos." + "\n" + "O seu martelo irá ficar no chão até ser recolhido.";
            tooltiptext[0,1,6] = "?";
            tooltiptext[1,1,6] = "Hog ganha 1 de armadura para cada 1% de vida perdido até um máximo de " + player.skillPower[1, 3].ToString("N0") + " de armadura adicional.";
            tooltiptext[0,1,7] = "?";
            tooltiptext[1,1,7] = "Hog solta um grito de guerra aumentando a regeneração de vida dos seus aliados em " + player.skillPower[1, 4].ToString("N0") + " durante " + player.skillPower[1, 5].ToString("N0") + " segundos.";
            tooltiptext[0,1,8] = "?";
            tooltiptext[1,1,8] = "Hog entra em estado de fúria ganhando " + player.skillPower[1,6].ToString("N0") + " de dano de ataque e " + player.skillPower[1, 6].ToString("N0") + " de roubo de vida por " + player.skillPower[1,7].ToString("N0") + " segundos.";
            tooltiptext[0,1,9] = "?";
            tooltiptext[1,1,9] = "Hog usa suas mãos para atacar, ganhando " + player.skillPower[1,0].ToString("N0") + "% de velocidade de ataque e causando seu dano completo.";
            tooltiptext[0,2,0] = "?";
            tooltiptext[1,2,0] = "Sirena cria uma esfera no local alvo, causando " + player.skillPower[0,1].ToString("N0") + " + " + (player.characterAttributes[2] * 0.2f).ToString("N0") + " de dano aos inimigos na área." + "\n" + "Sirena pode manter " + player.skillPower[0,2].ToString("N0") + " esferas em campo. Criar uma esfera além do limite fará com que a primeira desapareça.";
            tooltiptext[0,2,1] = "?";
            tooltiptext[1,2,1] = "Sirena usa sua esfera ativa para puxar inimigos e causar " + player.skillPower[0,3].ToString("N0") + " + " + (player.characterAttributes[2] * 0.8f).ToString("N0") + " de dano a eles.";
            tooltiptext[0,2,2] = "?";
            tooltiptext[1,2,2] = "Sirena usa sua esfera ativa para repelir inimigos e curar aliados na área em " + player.skillPower[0,4].ToString("N0") + " + " + (player.characterAttributes[2] * 2.5f).ToString("N0") + ".";
            tooltiptext[0,2,3] = "?";
            tooltiptext[1,2,3] = "Sirena sobrecarrega todas esferas em campo, fazendo com que elas puxem inimigos e causem " + player.skillPower[0,3].ToString("N0") + " + " + (player.characterAttributes[2] * 0.8f).ToString("N0") + " de dano a eles, e repele os mesmos logo após. Aliados na área são curados em " + player.skillPower[0,4].ToString("N0") + " + " + (player.characterAttributes[2] * 2.5f).ToString("N0") + ".";
            tooltiptext[0,2,4] = "?";
            tooltiptext[1,2,4] = "Sirena solta raios em seus ataques básicos, os raios desencadeiam " + player.skillPower[0,0].ToString("N0") + " vezes entre seus inimigos (é possível o raio desencadear em um mesmo inimigo). Raios causam metade do dano de ataque.";
        }

        if (player.characterMana[1] >= player.skillManaCost[0] - Items.Effect[21]) skillButton[0].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        else skillButton[0].GetComponent<Image>().color = new Color32(127, 127, 255, 255);
        if (player.characterMana[1] >= player.skillManaCost[1] - Items.Effect[22]) skillButton[1].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        else skillButton[1].GetComponent<Image>().color = new Color32(127, 127, 255, 255);
        if (player.characterMana[1] >= player.skillManaCost[2] - Items.Effect[23]) skillButton[2].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        else skillButton[2].GetComponent<Image>().color = new Color32(127, 127, 255, 255);
        if (player.characterMana[1] >= player.skillManaCost[3] - Items.Effect[24]) skillButton[3].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        else skillButton[3].GetComponent<Image>().color = new Color32(127, 127, 255, 255);
    }

    public void UseSkill (int index) {
        switch (index) {
            case 1:
                for (int i = 0; i < player.skillEnabled.Length; i++) {
                    player.skillEnabled[i] = false;
                }
                if (player.skillCooldown[0] <= 0f && player.characterMana[1] >= Mathf.Max (player.skillManaCost[0] - Items.Effect[21], 0)) player.skillEnabled[0] = true;
                break;
            case 2:
                for (int i = 0; i < player.skillEnabled.Length; i++) {
                    player.skillEnabled[i] = false;
                }
                if (player.skillCooldown[1] <= 0f && player.characterMana[1] >= Mathf.Max (player.skillManaCost[1] - Items.Effect[22], 0)) player.skillEnabled[1] = true;
                break;
            case 3:
                if (player.skillCooldown[2] <= 0f && player.characterMana[1] >= Mathf.Max (player.skillManaCost[2] - Items.Effect[23], 0)) {
                    Collider[] colliders = Physics.OverlapSphere (transform.position, player.skillRadius[2] + Items.Effect[32]);
                    foreach (Collider nearbyObject in colliders) {
                        Rigidbody rb = nearbyObject.GetComponent<Rigidbody> ();
                        if (rb != null && rb.transform.root.tag == "Mob") {
                            rb.transform.root.GetComponent<mob> ().TakeDamage (player.skillPower[player.charInstance, 2] + Items.Effect[18] + (player.characterAttributes[1] * 0.65f), 1);
                        }
                    }
                    player.characterMana[1] -= Mathf.Max (player.skillManaCost[2] - Items.Effect[23], 0);
                    player.skillCooldown[2] = player.skillMaxCooldown[2] - (player.skillMaxCooldown[2] * Items.Effect[13]);
                }
                break;
            case 4:
                for (int i = 0; i < player.skillEnabled.Length; i++) {
                    player.skillEnabled[i] = false;
                }
                if (player.skillCooldown[3] <= 0f && player.characterMana[1] >= Mathf.Max (player.skillManaCost[3] - Items.Effect[24], 0)) player.skillEnabled[3] = true;
                break;
            case 5:
                if (player.delayCounters[2] > 0) {
                    player.delayCounters[2] = 0;
                    player.skillCooldown[4] = player.skillMaxCooldown[4] - (player.skillMaxCooldown[4] * Items.Effect[15]);
                }
                break;
            case 6:
                if (player.charInstance == 0) {
                    for (int i = 0; i < player.skillEnabled.Length; i++) {
                        player.skillEnabled[i] = false;
                    }
                    if (player.skillCooldown[0] <= 0f && player.characterMana[1] >= Mathf.Max (player.skillManaCost[0] - Items.Effect[24], 0)) player.skillEnabled[0] = true;
                }
                break;
            case 7:
                player.armor -= bonusArmor;
                bonusArmor = 0;
                for (float i = 0; i < player.skillPower[player.charInstance, 3] + 1; i++) {
                    if ((player.characterHealth[0] - (player.characterHealth[0] * i / 100)) >= player.characterHealth[1]) {
                        bonusArmor = i;
                    }
                }
                player.armor -= -bonusArmor;
                if (player.armor > 99f) player.armor = 99f;
                break;
            case 8:
                //animação
                player.anim.Play("E");
                //código
                if (player.skillCooldown[2] <= 0 && player.characterMana[1] >= Mathf.Max (player.skillManaCost[2] - Items.Effect[24], 0)) {
                    Collider[] colliders = Physics.OverlapSphere (transform.position, player.skillRadius[2] + Items.Effect[32]);
                    foreach (Collider nearbyObject in colliders) {
                        Rigidbody rb = nearbyObject.GetComponent<Rigidbody> ();
                        if (rb != null && rb.transform.root.tag == "Player") StartCoroutine(HogE(player, rb.transform.root.GetComponent<player>(), player.charInstance));
                    }
                    player.characterMana[1] -= Mathf.Max (player.skillManaCost[2] - Items.Effect[23], 0);
                    player.skillCooldown[2] = player.skillMaxCooldown[2] - (player.skillMaxCooldown[2] * Items.Effect[13]);
                }
                break;
            case 9:
                if (player.skillCooldown[3] <= 0 && player.characterMana[1] >= Mathf.Max (player.skillManaCost[3] - Items.Effect[24], 0)) {
                    if (player.charInstance == 0) {
                        Collider[] colliders = Physics.OverlapSphere (transform.position, player.skillRadius[3] + Items.Effect[32]);
                        foreach (Collider nearbyObject in colliders) {
                            Rigidbody rb = nearbyObject.GetComponent<Rigidbody> ();
                            if (rb != null && rb.transform.root.tag == "Mob") {
                                rb.transform.root.GetComponent<mob> ().TakeDamage ((player.skillPower[player.charInstance, 6] + Items.Effect[18]) + player.characterAttributes[0] + player.characterAttributes[1], 1);
                            }
                        }
                        GameObject RProjectileGO = Instantiate (HogProjectile, player.firePoint.position, player.firePoint.rotation);
                        HogProjectile RProjectile = RProjectileGO.GetComponent<HogProjectile> ();
                        if (RProjectile != null) {
                            RProjectile.GoTo (RProjectileGO.transform.position);
                            RProjectile.stuck = true;
                            RProjectile.stuckDuration = player.skillPower[player.charInstance, 7];
                        }
                        player.charInstance = 1;
                        player.characterMana[1] -= Mathf.Max (player.skillManaCost[3] - Items.Effect[23], 0);
                        player.skillCooldown[3] = player.skillMaxCooldown[3] - (player.skillMaxCooldown[3] * Items.Effect[14]);
                    } else {
                        StartCoroutine (HogR2 (player));
                        player.characterMana[1] -= Mathf.Max (player.skillManaCost[3] - Items.Effect[23], 0);
                        player.skillCooldown[3] = player.skillMaxCooldown[3] - (player.skillMaxCooldown[3] * Items.Effect[14]);
                    }
                }
                break;
            case 10:
                if (GameObject.Find ("HogProjectile(Clone)")) {
                    player.charInstance = 1;
                    skillButton[2].sprite = HogIcons[3];
                    skillButton[3].sprite = HogIcons[5];
                    skillButton[4].sprite = HogIcons[1];
                } else {
                    skillButton[2].sprite = HogIcons[2];
                    skillButton[3].sprite = HogIcons[4];
                    skillButton[4].sprite = HogIcons[0];
                }
                break;
            case 11:
                for (int i = 0; i < player.skillEnabled.Length; i++) {
                    player.skillEnabled[i] = false;
                }
                if (player.skillCooldown[0] <= 0f && player.characterMana[1] >= Mathf.Max(player.skillManaCost[0] - Items.Effect[21], 0)) player.skillEnabled[0] = true;
                break;
            case 12:
                if (player.skillCooldown[1] <= 0f && player.characterMana[1] >= Mathf.Max(player.skillManaCost[1] - Items.Effect[22], 0)) {
                    SirenaNearestSphere.GetComponent<SirenaSphere>().CreateParticle(0);
                    Collider[] colliders = Physics.OverlapSphere(SirenaNearestSphere.transform.position, player.skillRadius[1] + Items.Effect[27]);
                    foreach (Collider nearbyObject in colliders) {
                        Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                        if (rb != null && rb.transform.root.tag == "Mob") {
                            rb.transform.root.GetComponent<mob>().TakeDamage(player.skillPower[player.charInstance, 3] + Items.Effect[17] + (player.characterAttributes[2] * 0.8f), 1);
                            rb.transform.root.GetComponent<mob>().Dislocate(1.25f, (SirenaNearestSphere.transform.position - rb.transform.position));
                        }
                    }
                    player.characterMana[1] -= Mathf.Max(player.skillManaCost[2] - Items.Effect[22], 0);
                    player.skillCooldown[1] = player.skillMaxCooldown[1] - (player.skillMaxCooldown[1] * Items.Effect[12]);
                }
                break;
            case 13:
                if (player.skillCooldown[2] <= 0 && player.characterMana[1] >= Mathf.Max(player.skillManaCost[2] - Items.Effect[24], 0)) {
                    SirenaNearestSphere.GetComponent<SirenaSphere>().CreateParticle(1);
                    Collider[] colliders = Physics.OverlapSphere(SirenaNearestSphere.transform.position, player.skillRadius[2] + Items.Effect[28]);
                    foreach (Collider nearbyObject in colliders) {
                        Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                        if (rb != null && rb.transform.root.tag == "Mob") {
                            rb.transform.root.GetComponent<mob>().Dislocate(0.5f, (-SirenaNearestSphere.transform.position + rb.transform.position));
                        } else if (rb != null && rb.transform.root.tag == "Player") rb.transform.root.GetComponent<player>().Heal(player.skillPower[player.charInstance, 4] + Items.Effect[18] + (player.characterAttributes[2] * 2.5f));
                    }
                    player.characterMana[1] -= Mathf.Max(player.skillManaCost[2] - Items.Effect[23], 0);
                    player.skillCooldown[2] = player.skillMaxCooldown[2] - (player.skillMaxCooldown[2] * Items.Effect[13]);
                }
                break;
            case 14:
                if (player.skillCooldown[3] <= 0 && player.characterMana[1] >= Mathf.Max(player.skillManaCost[3] - Items.Effect[25], 0)) {
                    for (int i = 0; i < SirenaSpheres.Count; i++) {
                        StartCoroutine(SirenaR(SirenaSpheres[i]));
                    }
                    player.characterMana[1] -= Mathf.Max(player.skillManaCost[3] - Items.Effect[24], 0);
                    player.skillCooldown[3] = player.skillMaxCooldown[3] - (player.skillMaxCooldown[3] * Items.Effect[14]);
                }                
                break;
            case 15:
                if (SirenaSpheres.Count <= 0 && GameObject.FindGameObjectWithTag("Sirena Sphere")) {
                    foreach(GameObject sphere in GameObject.FindGameObjectsWithTag("Sirena Sphere")) SirenaSpheres.Insert(0, sphere);
                }

                if (SirenaSpheres.Count > 0) {
                    SirenaNearestSphere = SirenaSpheres[0];
                    for (int i = 1; i < SirenaSpheres.Count; i++) {
                        if (Vector3.Distance(transform.position, SirenaSpheres[i].transform.position) < Vector3.Distance(transform.position, SirenaNearestSphere.transform.position)) {
                            SirenaNearestSphere = SirenaSpheres[i];
                        }
                    }
                    for (int i = 0; i < SirenaSpheres.Count; i++) {
                        if (SirenaNearestSphere == SirenaSpheres[i]) {
                            SirenaSpheres[i].GetComponent<SirenaSphere>().nearest = true;
                        } else {
                            SirenaSpheres[i].GetComponent<SirenaSphere>().nearest = false;
                        }
                    }
                }
                break;
            default:
                break;
        }
    }

    public void SkillClicked (int index, RaycastHit hit, bool isAutoCast) {
        switch (index) {
            case 1:
                if (player.skillEnabled[0] && player.skillCooldown[0] <= 0 && player.characterMana[1] >= Mathf.Max (player.skillManaCost[0] - Items.Effect[21], 0) && Vector3.Distance (hit.point, transform.position) <= player.skillRange[0] + Items.Effect[26]) {
                    Collider[] colliders = Physics.OverlapSphere (hit.point, player.skillRadius[0] + Items.Effect[30]);
                    foreach (Collider nearbyObject in colliders) {
                        Rigidbody rb = nearbyObject.GetComponent<Rigidbody> ();
                        if (rb != null && rb.transform.root.tag == "Mob") {
                            rb.transform.root.GetComponent<mob> ().TakeDamage (player.skillPower[player.charInstance, 0] + Items.Effect[16] + (player.characterAttributes[2] * 0.35f), 1);
                        }
                    }
                    player.characterMana[1] -= Mathf.Max (player.skillManaCost[0] - Items.Effect[21], 0);
                    player.skillCooldown[0] = player.skillMaxCooldown[0] - (player.skillMaxCooldown[0] * Items.Effect[11]);
                    player.skillEnabled[0] = false;
                }
                break;
            case 2:
                if (hit.collider ? hit.collider.tag == "Player" : false || isAutoCast) {
                    if (player.skillEnabled[1] == true && Vector3.Distance (hit.point, transform.position) <= player.skillRange[1] + Items.Effect[26]) {
                        player.Heal (player.skillPower[player.charInstance, 1] + Items.Effect[17] + (player.characterAttributes[2] * 0.5f));
                        if (player.characterHealth[1] > player.characterHealth[0]) {
                            player.characterHealth[1] = player.characterHealth[0];
                        }
                        player.characterMana[1] -= Mathf.Max (player.skillManaCost[1] - Items.Effect[22], 0);
                        player.skillEnabled[1] = false;
                        player.skillCooldown[1] = player.skillMaxCooldown[1] - (player.skillMaxCooldown[1] * Items.Effect[12]);
                    }
                } else player.skillEnabled[1] = false;
                break;
            case 4:
                if (player.skillEnabled[3] == true && player.skillCooldown[3] <= 0 && player.characterMana[1] >= Mathf.Max (player.skillManaCost[3] - Items.Effect[24], 0)) {
                    player.agent.isStopped = true;
                    transform.LookAt (hit.point);
                    float oldRotationx = transform.rotation.x;
                    float oldRotationz = transform.rotation.z;
                    transform.rotation.Set (oldRotationx, 0, oldRotationz, 1);
                    GameObject RProjectileGO = Instantiate (DefaultRProjectile, player.firePoint.position, player.firePoint.rotation);
                    RProjectile RProjectile = RProjectileGO.GetComponent<RProjectile> ();
                    if (RProjectile != null) {
                        RProjectile.GoTo (hit.point);
                    }
                    player.characterMana[1] -= Mathf.Max (player.skillManaCost[3] - Items.Effect[24], 0);
                    player.skillCooldown[3] = player.skillMaxCooldown[3] - (player.skillMaxCooldown[3] * Items.Effect[14]);
                    player.skillEnabled[3] = false;
                    player.agent.isStopped = false;
                }
                break;
            case 6:
                if (player.skillEnabled[0] == true && player.skillCooldown[0] <= 0 && player.characterMana[1] >= Mathf.Max (player.skillManaCost[0] - Items.Effect[24], 0) && Vector3.Distance (hit.point, transform.position) <= player.skillRange[0] + Items.Effect[26]) {
                    gameObject.GetComponent<NavMeshAgent> ().isStopped = true;
                    transform.LookAt (hit.point);
                    float oldRotationx = transform.rotation.x;
                    float oldRotationz = transform.rotation.z;
                    transform.rotation.Set (oldRotationx, 0, oldRotationz, 1);
                    GameObject QProjectileGO = Instantiate (HogProjectile, player.firePoint.position, player.firePoint.rotation);
                    HogProjectile QProjectile = QProjectileGO.GetComponent<HogProjectile> ();
                    if (QProjectile != null) {
                        QProjectile.GoTo (hit.point);
                    }
                    player.characterMana[1] -= Mathf.Max (player.skillManaCost[0] - Items.Effect[24], 0);
                    player.skillCooldown[0] = player.skillMaxCooldown[0] - (player.skillMaxCooldown[0] * Items.Effect[11]);
                    player.skillEnabled[0] = false;
                    transform.GetChild(0).gameObject.SetActive(false);
                    player.charInstance = 1;
                    player.anim.SetInteger("CharInstance", 1);
                    gameObject.GetComponent<NavMeshAgent> ().isStopped = false;
                }
                break;
            case 11:
                if (player.skillEnabled[0] && player.skillCooldown[0] <= 0 && player.characterMana[1] >= Mathf.Max(player.skillManaCost[0] - Items.Effect[21], 0) && Vector3.Distance(hit.point, transform.position) <= player.skillRange[0] + Items.Effect[26]) {
                    GameObject SirenaSphereGO = Instantiate (SirenaSphere, hit.point, Quaternion.identity);
                    SirenaSpheres.Insert(0, SirenaSphereGO);
                    if (SirenaSpheres.Count > player.skillPower[player.charInstance, 2]) {
                        Destroy(SirenaSpheres[SirenaSpheres.Count -1]);
                        SirenaSpheres.RemoveAt(SirenaSpheres.Count -1);
                    }
                    player.characterMana[1] -= Mathf.Max(player.skillManaCost[0] - Items.Effect[21], 0);
                    player.skillCooldown[0] = player.skillMaxCooldown[0] - (player.skillMaxCooldown[0] * Items.Effect[11]);
                    player.skillEnabled[0] = false;
                }
                break;
            default:
                break;
        }
    }

    public void BasicAttack (int index) {
        if (player.target.tag == "Mob") {
            if (Vector3.Distance (player.target.transform.position, transform.position) <= player.attackRange + Items.Effect[6]) {
                switch (index) {
                    case 1:
                        transform.LookAt(player.target.transform);
                        if (player.charInstance == 0) {
                            Collider[] colliders = Physics.OverlapSphere (player.target.transform.position, player.skillRadius[4]);
                            foreach (Collider nearbyObject in colliders) {
                                Rigidbody rb = nearbyObject.GetComponent<Rigidbody> ();
                                if (rb != null && rb.transform.root.tag == "Mob") {
                                    if (Random.Range(0, 100) < player.critChance) { 
                                        rb.transform.root.GetComponent<mob> ().TakeDamage (((player.basicDamage - (player.basicDamage * player.skillPower[player.charInstance, 0] / 100)) - player.basicDamage * player.target.GetComponent<mob> ().armor / 100) * 2.5f, 0);
                                        if (player.lifesteal > 0f) player.Heal((((player.basicDamage - (player.basicDamage * player.skillPower[player.charInstance, 0] / 100)) - player.basicDamage * player.target.GetComponent<mob> ().armor / 100) * 2.5f) * player.lifesteal);
                                    }
                                    else {
                                        rb.transform.root.GetComponent<mob>().TakeDamage((player.basicDamage - (player.basicDamage * player.skillPower[player.charInstance, 0] / 100)) - player.basicDamage * player.target.GetComponent<mob>().armor / 100, 0);
                                        if (player.lifesteal > 0f) player.Heal(((player.basicDamage - (player.basicDamage * player.skillPower[player.charInstance, 0] / 100)) - player.basicDamage * player.target.GetComponent<mob>().armor / 100) * player.lifesteal);
                                    }
                                }
                            }
                            player.delayCounters[2] = 1 / player.attackSpeed;
                            player.agent.destination = transform.position;
                        } else {
                            if (Random.Range(0, 100) < player.critChance) {
                                player.target.GetComponent<mob> ().TakeDamage ((player.basicDamage - (player.basicDamage * player.target.GetComponent<mob> ().armor / 100)) * 2.5f, 0);
                                if (player.lifesteal > 0f) player.Heal(((player.basicDamage - (player.basicDamage * player.target.GetComponent<mob>().armor / 100)) * 2.5f) * player.lifesteal);
                            } else {
                                player.target.GetComponent<mob>().TakeDamage(player.basicDamage - (player.basicDamage * player.target.GetComponent<mob>().armor / 100), 0);
                                if (player.lifesteal > 0f) player.Heal((player.basicDamage - (player.basicDamage * player.target.GetComponent<mob>().armor / 100)) * player.lifesteal);
                            }
                            player.delayCounters[2] = 1 / (player.attackSpeed + (player.attackSpeed * player.skillPower[player.charInstance, 1] / 100));
                            player.agent.destination = transform.position;
                        }
                        break;
                    case 2:
                        transform.LookAt(player.target.transform);
                        Instantiate(SirenaBasicAttack, player.firePoint.position, Quaternion.identity);
                        player.delayCounters[2] = 1 / player.attackSpeed;
                        player.agent.destination = transform.position;
                        break;
                    default:
                        transform.LookAt(player.target.transform);
                        if (Random.Range(0, 100) < player.critChance) {
                            player.target.GetComponent<mob> ().TakeDamage ((player.basicDamage - (player.basicDamage * player.target.GetComponent<mob> ().armor / 100)) * 2.5f, 0);
                            if (player.lifesteal > 0f) player.Heal(((player.basicDamage - (player.basicDamage * player.target.GetComponent<mob> ().armor / 100)) * 2.5f) * player.lifesteal);
                        }
                        else {
                            player.target.GetComponent<mob>().TakeDamage(player.basicDamage - (player.basicDamage * player.target.GetComponent<mob>().armor / 100), 0);
                            if (player.lifesteal > 0f) player.Heal((player.basicDamage - (player.basicDamage * player.target.GetComponent<mob>().armor / 100)) * player.lifesteal);
                        }
                        player.delayCounters[2] = 1 / player.attackSpeed;
                        player.agent.destination = transform.position;
                        break;
                }
            }
        }
    }

    IEnumerator HogR2 (player player) {
        float hogRbonusDamage;
        hogRbonusDamage = player.basicDamage * player.skillPower[player.charInstance, 6] / 100;
        player.basicDamage -= -hogRbonusDamage;
        player.lifesteal -= -player.skillPower[player.charInstance, 6] / 100;
        yield return new WaitForSeconds (player.skillPower[player.charInstance, 7]);
        player.basicDamage -= hogRbonusDamage;
        player.lifesteal -= player.skillPower[player.charInstance, 6] / 100;
    }

    IEnumerator HogE (player caster, player target, int instance) {
        float hogEbonusDamage;
        float hogEbonusHealthRegen;
        if (instance == 0) {
            hogEbonusDamage = (target.basicDamage) * caster.skillPower[caster.charInstance, 4] / 100;
            target.basicDamage -= -hogEbonusDamage;
            yield return new WaitForSeconds (caster.skillPower[caster.charInstance, 5]);
            target.basicDamage -= hogEbonusDamage;
        } else {
            hogEbonusHealthRegen = caster.skillPower[caster.charInstance, 4];
            target.characterHealth[2] -= -hogEbonusHealthRegen;
            yield return new WaitForSeconds (caster.skillPower[caster.charInstance, 5]);
            target.characterHealth[2] -= hogEbonusHealthRegen;
        }
    }

    IEnumerator SirenaR (GameObject sphere) {
        sphere.GetComponent<SirenaSphere>().CreateParticle(0);
        Collider[] colliders = Physics.OverlapSphere(sphere.transform.position, player.skillRadius[1] + Items.Effect[27]);
        foreach (Collider nearbyObject in colliders) {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null && rb.transform.root.tag == "Mob") {
                rb.transform.root.GetComponent<mob>().TakeDamage(player.skillPower[player.charInstance, 5] + Items.Effect[17] + (player.characterAttributes[2] * 0.8f), 1);
                if (rb.transform.root.GetComponent<mob>()) rb.transform.root.GetComponent<mob>().Dislocate(1.25f, (sphere.transform.position - rb.transform.position));
            }
        }
        yield return new WaitForSeconds (1.25f);
        sphere.GetComponent<SirenaSphere>().CreateParticle(1);
        colliders = Physics.OverlapSphere(sphere.transform.position, player.skillRadius[2] + Items.Effect[28]);
        foreach (Collider nearbyObject in colliders) {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null && rb.transform.root.tag == "Mob") rb.transform.root.GetComponent<mob>().Dislocate(0.5f, (-sphere.transform.position + rb.transform.position));
            else if (rb != null && rb.transform.root.tag == "Player") rb.transform.root.GetComponent<player>().Heal(player.skillPower[player.charInstance, 6] + Items.Effect[18] + (player.characterAttributes[2] * 2.5f));
        }
    }
}