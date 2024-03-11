using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class castleUpgrades : MonoBehaviour {
    public castle castle;
    public player player;
    public GameObject character;
    public GameObject castleObject;
    public Text[] upgradeCostText = new Text[3];
    public string[,,] tooltipText = new string[2,2,3];
    [SerializeField]
    private Image[] upgradeIcons = new Image[3];
    [SerializeField]
    private Sprite[] upgradeSprite = new Sprite[6];
    private spawn spawn;
    private int selectedCastle;

    void Start () {
        spawn = GameObject.Find("Spawn").GetComponent<spawn>();
        selectedCastle = spawn.selectedCastle;

        switch (selectedCastle) {
            case 1:
                upgradeIcons[0].sprite = upgradeSprite[3];
                upgradeIcons[1].sprite = upgradeSprite[4];
                upgradeIcons[2].sprite = upgradeSprite[5];
                break;
            default:
                upgradeIcons[0].sprite = upgradeSprite[0];
                upgradeIcons[1].sprite = upgradeSprite[1];
                upgradeIcons[2].sprite = upgradeSprite[2];
                break;
        }

        tooltipText[0,0,0] = "Health Upgrade";
        tooltipText[0,1,0] = "Aumenta a vida máxima do Crystalis em 90 e cura toda vida perdida." + "\n" + "\n" + "Transformação: triplica a vida máxima do Crystalis e cura toda vida perdida.";
        tooltipText[0,0,1] = "Armor Upgrade";
        tooltipText[0,1,1] = "Aumenta a armadura do Crystalis em 2,5." + "\n" + "\n" + "Transformação: ganha 25 de armadura.";
        tooltipText[0,0,2] = "Health Regen Upgrade";
        tooltipText[0,1,2] = "Aumenta a regeneração de vida do Crystalis em 3,5." + "\n" + "\n" + "Transformação: duplica a regeneração de vida do Crystalis e concede uma aura de cura. A cura aumenta de acordo com a regeneração de vida do Crystalis";
        tooltipText[1,0,0] = "Damage Upgrade";
        tooltipText[1,1,0] = "Aumenta o dano do Crystalis em 20." + "\n" + "\n" + "Transformação: aumenta o limite de alvos em 3.";
        tooltipText[1,0,1] = "Attack Speed Upgrade";
        tooltipText[1,1,1] = "Aumenta a velocidade de ataque do Crystalis em 25%." + "\n" + "\n" + "Transformação: ataca duas vezes cada alvo.";
        tooltipText[1,0,2] = "Lifesteal Upgrade";
        tooltipText[1,1,2] = "Aumenta o roubo de vida do Crystalis em 7,5%." + "\n" + "\n" + "Transformação: o Crystalis ganha 100% de roubo de vida extra e uma aura de roubo de vida. A aura concede 50% de roubo de vida aos heróis dentro da área.";

        upgradeCostText[0].text = castle.upgradeCost[0].ToString();
        upgradeCostText[1].text = castle.upgradeCost[1].ToString();
        upgradeCostText[2].text = castle.upgradeCost[2].ToString();
    }

    void Update () {
        upgradeCostText[0].transform.parent.gameObject.GetComponent<Button>().interactable = player.gold >= castle.upgradeCost[0] && castle.level[1] < castle.level[0];
        upgradeCostText[1].transform.parent.gameObject.GetComponent<Button>().interactable = player.gold >= castle.upgradeCost[1] && castle.level[1] < castle.level[0];
        upgradeCostText[2].transform.parent.gameObject.GetComponent<Button>().interactable = player.gold >= castle.upgradeCost[2] && castle.level[1] < castle.level[0];
    }

    public void Upgrade1 () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            if (player.gold >= castle.upgradeCost[0] && castle.level[1] < castle.level[0]) {
                player.gold -= castle.upgradeCost[0];
                castle.upgradeCost[0] -=- 100;
                upgradeCostText[0].text = castle.upgradeCost[0].ToString();
                castle.level[1]++;

                switch (selectedCastle) {
                    case 1:
                        offensiveCastle offCastle;
                        offCastle = castle.gameObject.GetComponent<offensiveCastle>();

                        offCastle.damage -=- 20f;

                        if (castle.level[1] == castle.level[0]) {
                            if (castle.upgradeCost[0] > castle.upgradeCost[1] && castle.upgradeCost[0] > castle.upgradeCost[2]) offCastle.maxTargets -=- 3;
                            if (castle.upgradeCost[1] > castle.upgradeCost[0] && castle.upgradeCost[1] > castle.upgradeCost[2]) {
                                offCastle.lifesteal -=- 1f;
                                castle.castleAura = true;
                            }
                            if (castle.upgradeCost[2] > castle.upgradeCost[0] && castle.upgradeCost[2] > castle.upgradeCost[1]) offCastle.doubleAttack = true;
                        }
                        break;
                    default:
                        castle.health[0] -= -90f;
                        castle.health[1] = castle.health[0];

                        if (castle.level[1] == castle.level[0]) {
                            if (castle.upgradeCost[0] > castle.upgradeCost[1] && castle.upgradeCost[0] > castle.upgradeCost[2]) {
                                castle.health[0] = castle.health[0] * 3;
                                castle.health[1] = castle.health[0];
                            }
                            if (castle.upgradeCost[1] > castle.upgradeCost[0] && castle.upgradeCost[1] > castle.upgradeCost[2]) {
                                castle.health[2] = castle.health[2] * 2;
                                castle.castleAura = true;
                            }
                            if (castle.upgradeCost[2] > castle.upgradeCost[0] && castle.upgradeCost[2] > castle.upgradeCost[1]) castle.armor *= 2;
                        }
                        break;
                }

                if (castle.level[1] == castle.level[0]) {
                    foreach (Text text in upgradeCostText) {
                        text.text = "MAX";
                    }
                    GameObject.Find("Director").GetComponent<UnlockablesManager>().unlockableMatrix.OffensiveCastleUnlocked = true;
                    GameObject.Find("Director").GetComponent<UnlockablesManager>().SaveJson();
                }
            }
        }
    }

    public void Upgrade2 () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            if (player.gold >= castle.upgradeCost[2] && castle.level[1] < castle.level[0]) {
                player.gold -= castle.upgradeCost[2];
                castle.upgradeCost[2] -=- 100;
                upgradeCostText[2].text = castle.upgradeCost[2].ToString();
                castle.level[1]++;

                switch (selectedCastle) {
                    case 1:
                        offensiveCastle offCastle;
                        offCastle = castle.gameObject.GetComponent<offensiveCastle>();

                        offCastle.attackSpeed -=- (offCastle.attackSpeed * 0.25f);

                        if (castle.level[1] == castle.level[0]) {
                            if (castle.upgradeCost[0] > castle.upgradeCost[1] && castle.upgradeCost[0] > castle.upgradeCost[2]) offCastle.maxTargets -=- 3;
                            if (castle.upgradeCost[1] > castle.upgradeCost[0] && castle.upgradeCost[1] > castle.upgradeCost[2]) {
                                offCastle.lifesteal -=- 1f;
                                castle.castleAura = true;
                            }
                            if (castle.upgradeCost[2] > castle.upgradeCost[0] && castle.upgradeCost[2] > castle.upgradeCost[1]) offCastle.doubleAttack = true;
                        }
                        break;
                    default:
                        castle.armor -= -2.5f;

                        if (castle.level[1] == castle.level[0]) {
                            if (castle.upgradeCost[0] > castle.upgradeCost[1] && castle.upgradeCost[0] > castle.upgradeCost[2]) {
                                castle.health[0] = castle.health[0] * 3;
                                castle.health[1] = castle.health[0];
                            }
                            if (castle.upgradeCost[1] > castle.upgradeCost[0] && castle.upgradeCost[1] > castle.upgradeCost[2]) {
                                castle.health[2] = castle.health[2] * 2;
                                castle.castleAura = true;
                            }
                            if (castle.upgradeCost[2] > castle.upgradeCost[0] && castle.upgradeCost[2] > castle.upgradeCost[1]) castle.armor *= 2;
                        }
                        break;
                }

                if (castle.level[1] == castle.level[0]) {
                    foreach (Text text in upgradeCostText) {
                        text.text = "MAX";
                    }
                    GameObject.Find("Director").GetComponent<UnlockablesManager>().unlockableMatrix.OffensiveCastleUnlocked = true;
                    GameObject.Find("Director").GetComponent<UnlockablesManager>().SaveJson();
                }
            }
        }
    }

    public void Upgrade3 () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            if (player.gold >= castle.upgradeCost[1] && castle.level[1] < castle.level[0]) {
                player.gold -= castle.upgradeCost[1];
                castle.upgradeCost[1] -=- 100;
                upgradeCostText[1].text = castle.upgradeCost[1].ToString();
                castle.level[1]++;

                switch (selectedCastle) {
                    case 1:
                        offensiveCastle offCastle;
                        offCastle = castle.gameObject.GetComponent<offensiveCastle>();

                        offCastle.lifesteal -=- 0.075f;

                        if (castle.level[1] == castle.level[0]) {
                            if (castle.upgradeCost[0] > castle.upgradeCost[1] && castle.upgradeCost[0] > castle.upgradeCost[2]) offCastle.maxTargets -=- 3;
                            if (castle.upgradeCost[1] > castle.upgradeCost[0] && castle.upgradeCost[1] > castle.upgradeCost[2]) {
                                offCastle.lifesteal -=- 1f;
                                castle.castleAura = true;
                            }
                            if (castle.upgradeCost[2] > castle.upgradeCost[0] && castle.upgradeCost[2] > castle.upgradeCost[1]) offCastle.doubleAttack = true;
                        }
                        break;
                    default:
                        castle.health[2] -= -3.5f;

                        if (castle.level[1] == castle.level[0]) {
                            if (castle.upgradeCost[0] > castle.upgradeCost[1] && castle.upgradeCost[0] > castle.upgradeCost[2]) {
                                castle.health[0] = castle.health[0] * 3;
                                castle.health[1] = castle.health[0];
                            }
                            if (castle.upgradeCost[1] > castle.upgradeCost[0] && castle.upgradeCost[1] > castle.upgradeCost[2]) {
                                castle.health[2] = castle.health[2] * 2;
                                castle.castleAura = true;
                            }
                            if (castle.upgradeCost[2] > castle.upgradeCost[0] && castle.upgradeCost[2] > castle.upgradeCost[1]) castle.armor *= 2;
                        }
                        break;
                }

                if (castle.level[1] == castle.level[0]) {
                    foreach (Text text in upgradeCostText) {
                        text.text = "MAX";
                    }
                    GameObject.Find("Director").GetComponent<UnlockablesManager>().unlockableMatrix.OffensiveCastleUnlocked = true;
                    GameObject.Find("Director").GetComponent<UnlockablesManager>().SaveJson();
                }
            }
        }
    }
}