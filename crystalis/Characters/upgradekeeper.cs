using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradekeeper : MonoBehaviour {
    private bool enableBuy = false;
    public player player;
    public characters characters;
    public Text[] upgradeCostText = new Text[5];
    public Button[] upgradeButton = new Button[5];
    private int[] upgradeCost = new int[5];
    [SerializeField]
    private Sprite[] defaultSprite = new Sprite [5];
    [SerializeField]
    private Sprite[] hogSprite = new Sprite [5];
    [SerializeField]
    private Sprite[] sirenaSprite = new Sprite [5];

    void Start () {
        upgradeCost[0] = 300;
        upgradeCost[1] = 300;
        upgradeCost[2] = 300;
        upgradeCost[3] = 1370;
        upgradeCost[4] = 300;

        upgradeButton[0].onClick.AddListener(QUpgrade);
        upgradeButton[1].onClick.AddListener(WUpgrade);
        upgradeButton[2].onClick.AddListener(EUpgrade);
        upgradeButton[3].onClick.AddListener(RUpgrade);
        upgradeButton[4].onClick.AddListener(PUpgrade);

        switch (player.selectedChar) {
            case 1:
                for (int i = 0; i < upgradeButton.Length; i++) {
                    upgradeButton[i].gameObject.GetComponent<Image>().sprite = hogSprite[i];
                }
                break;
            case 2:
                for (int i = 0; i < upgradeButton.Length; i++) {
                    upgradeButton[i].gameObject.GetComponent<Image>().sprite = sirenaSprite[i];
                }
                break;
            default:
                for (int i = 0; i < upgradeButton.Length; i++) {
                    upgradeButton[i].gameObject.GetComponent<Image>().sprite = defaultSprite[i];
                }
                break;
        }

        for (int i = 0; i < upgradeCost.Length; i++)
        {
            upgradeCostText[i].text = upgradeCost[i].ToString();
        }
    }

    void Update () {
        upgradeButton[0].GetComponent<Button>().interactable = player.skillLevel[0] < 4 && player.gold >= upgradeCost[0] && enableBuy;
        upgradeButton[1].GetComponent<Button>().interactable = player.skillLevel[1] < 4 && player.gold >= upgradeCost[1] && enableBuy;
        upgradeButton[2].GetComponent<Button>().interactable = player.skillLevel[2] < 4 && player.gold >= upgradeCost[2] && enableBuy;
        upgradeButton[3].GetComponent<Button>().interactable = player.skillLevel[3] < 3 && player.gold >= upgradeCost[3] && enableBuy;
        upgradeButton[4].GetComponent<Button>().interactable = player.skillLevel[4] < 4 && player.gold >= upgradeCost[4] && enableBuy;
    }

    public void QUpgrade () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            if (player.skillLevel[0] < 4 && player.gold >= upgradeCost[0] && enableBuy) {
                player.skillLevel[0]++;
                player.gold -= upgradeCost[0];
                upgradeCost[0] -= -upgradeCost[0];
                upgradeCostText[0].text = upgradeCost[0].ToString ();
                if (player.skillLevel[0] == 4) upgradeCostText[0].text = "MAX";
                switch (characters.charList[player.selectedChar].NSkill[0])
                {
                    case 1:
                        if (player.skillLevel[0] % 2 == 0)
                        {
                            player.skillRadius[0] -= -9f;
                        }
                        else
                        {
                            player.skillPower[player.charInstance, 0] -= -10f;
                            player.skillManaCost[0] -= -13f;
                        }
                        break;
                    case 6:
                        switch (player.skillLevel[0])
                        {
                            case 1:
                                player.skillPower[0, 1] -= -5f;
                                player.skillPower[0, 2] -= -1f;
                                break;
                            case 2:
                                player.skillPower[0, 1] -= -15f;
                                player.skillPower[0, 2] -= -1f;
                                break;
                            case 3:
                                player.skillPower[0, 1] -= -50f;
                                player.skillPower[0, 2] -= -1f;
                                break;
                            case 4:
                                player.skillPower[0, 1] -= -75f;
                                player.skillPower[0, 2] -= -1f;
                                break;
                            default:
                                break;
                        }
                        break;
                    case 11:
                        player.skillPower[0, 1] -= -10f;
                        player.skillPower[0, 2] -= -2f;
                        player.skillManaCost[0] -= -13f;
                        player.skillMaxCooldown[0] -= 2f;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void WUpgrade () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            if (player.skillLevel[1] < 4 && player.gold >= upgradeCost[1] && enableBuy) {
                player.skillLevel[1]++;
                player.gold -= upgradeCost[1];
                upgradeCost[1] -= -upgradeCost[1];
                upgradeCostText[1].text = upgradeCost[1].ToString ();
                if (player.skillLevel[1] == 4) upgradeCostText[1].text = "MAX";
                switch (characters.charList[player.selectedChar].NSkill[1])
                {
                    case 2:
                        switch (player.skillLevel[1])
                        {
                            case 1:
                                player.skillPower[player.charInstance, 1] -= -15f;
                                player.skillManaCost[1] -= -11f;
                                break;
                            case 2:
                                player.skillMaxCooldown[1] -= 0.5f;
                                break;
                            case 3:
                                player.skillPower[player.charInstance, 1] -= -25f;
                                player.skillManaCost[1] -= -21f;
                                break;
                            case 4:
                                player.skillPower[player.charInstance, 1] -= 1.5f;
                                break;
                            default:
                                break;
                        }
                        break;
                    case 7:
                        switch (player.skillLevel[1])
                        {
                            case 1:
                                player.skillPower[0, 3] -= -2f;
                                player.skillPower[1, 3] -= -2f;
                                break;
                            case 2:
                                player.skillPower[0, 3] -= -4f;
                                player.skillPower[1, 3] -= -4f;
                                break;
                            case 3:
                                player.skillPower[0, 3] -= -2f;
                                player.skillPower[1, 3] -= -2f;
                                break;
                            case 4:
                                player.skillPower[0, 3] -= -4f;
                                player.skillPower[1, 3] -= -4f;
                                break;
                            default:
                                break;
                        }
                        break;
                    case 12:
                        switch (player.skillLevel[1])
                        {
                            case 4:
                                player.skillPower[0, 3] -= -50;
                                break;
                            default:
                                player.skillPower[0, 3] -= -player.skillPower[0, 3];
                                player.skillManaCost[1] -= -19f;
                                player.skillMaxCooldown[1] -= 0.5f;
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void EUpgrade () {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            if (player.skillLevel[2] < 4 && player.gold >= upgradeCost[2] && enableBuy) {
                player.skillLevel[2]++;
                player.gold -= upgradeCost[2];
                upgradeCost[2] -= -upgradeCost[2];
                upgradeCostText[2].text = upgradeCost[2].ToString ();
                if (player.skillLevel[2] == 4) upgradeCostText[2].text = "MAX";
                switch (characters.charList[player.selectedChar].NSkill[2])
                {
                    case 3:
                        switch (player.skillLevel[2])
                        {
                            case 1:
                                player.skillPower[player.charInstance, 2] -= -30f;
                                player.skillManaCost[2] -= -19f;
                                break;
                            case 2:
                                player.skillMaxCooldown[2] -= 2.5f;
                                break;
                            case 3:
                                player.skillPower[player.charInstance, 2] -= -60f;
                                player.skillManaCost[2] -= -29f;
                                break;
                            case 4:
                                player.skillMaxCooldown[2] -= 3f;
                                break;
                            default:
                                break;
                        }
                        break;
                    case 8:
                        switch (player.skillLevel[2])
                        {
                            case 3:
                                player.skillPower[0, 4] -= -5f;
                                player.skillPower[1, 4] -= -10f;
                                player.skillPower[0, 5] -= -2f;
                                player.skillPower[1, 5] -= -1f;
                                break;
                            case 4:
                                player.skillPower[0, 4] -= -5f;
                                player.skillPower[1, 4] -= -10f;
                                player.skillPower[0, 5] -= -2f;
                                player.skillPower[1, 5] -= -3f;
                                break;
                            default:
                                player.skillPower[0, 4] -= -5f;
                                player.skillPower[1, 4] -= -10f;
                                player.skillPower[0, 5] -= -1f;
                                player.skillPower[1, 5] -= -1f;
                                break;
                        }
                        break;
                    case 13:
                        player.skillPower[0, 4] -= -20f;
                        player.skillManaCost[2] -= -33f;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void RUpgrade () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            if (player.skillLevel[3] < 3 && player.gold >= upgradeCost[3] && enableBuy) {
                player.skillLevel[3]++;
                player.gold -= upgradeCost[3];
                upgradeCost[3] -= -upgradeCost[3];
                upgradeCostText[3].text = upgradeCost[3].ToString ();
                if (player.skillLevel[3] == 3) upgradeCostText[3].text = "MAX";
                switch (characters.charList[player.selectedChar].NSkill[3])
                {
                    case 4:
                        switch (player.skillLevel[3])
                        {
                            case 1:
                                player.skillRadius[3] -= -25f;
                                break;
                            case 2:
                                player.skillPower[player.charInstance, 3] -= -120f;
                                break;
                            default:
                                break;
                        }
                        break;
                    case 9:
                        switch (player.skillLevel[3])
                        {
                            case 1:
                                player.skillPower[0, 6] -=- 175f;
                                player.skillPower[1, 6] -=- 25f;
                                player.skillPower[0, 7] -= 2f;
                                player.skillPower[1, 7] -=- 4f;
                                break;
                            case 2:
                                player.skillPower[0, 6] -= -275f;
                                player.skillPower[1, 6] -= -25f;
                                player.skillPower[0, 7] -= 2f;
                                player.skillPower[1, 7] -= -4f;
                                break;
                            default:
                                break;
                        }
                        break;
                    case 14:
                        switch (player.skillLevel[3])
                        {
                            case 4:
                                player.skillPower[0, 5] -= -50;
                                break;
                            default:
                                player.skillPower[0, 5] -= -player.skillPower[0, 5];
                                break;
                        }
                        player.skillPower[0, 6] -= -20f;
                        player.skillManaCost[3] -= -14f;
                        player.skillMaxCooldown[3] -= 1.25f;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void PUpgrade () {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            if (player.skillLevel[4] < 4 && player.gold >= upgradeCost[4] && enableBuy) {
                player.skillLevel[4]++;
                player.gold -= upgradeCost[4];
                upgradeCost[4] -= -upgradeCost[4];
                upgradeCostText[4].text = upgradeCost[4].ToString ();
                if (player.skillLevel[4] == 4) upgradeCostText[4].text = "MAX";
                switch (characters.charList[player.selectedChar].NSkill[4])
                {
                    case 5:
                        player.skillMaxCooldown[4] -= 0.25f;
                        break;
                    case 10:
                        player.skillPower[0, 0] -= 3f;
                        player.skillPower[1, 0] -=- 10f;
                        break;
                    case 15:
                        player.skillPower[0, 0] -=- 4f;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            enableBuy = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            enableBuy = false;
        }
    }
}