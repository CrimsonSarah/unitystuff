using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldown : MonoBehaviour {
    public Image[] CooldownArray = new Image[5]; // [0]QSkillCooldown, [1]WSkillCooldown, [2]ESkillCooldown, [3]UltCooldown, [4]PSkillCooldown
    public Button[] CooldownButtonArrray = new Button[4]; // [0]QSkillCooldown, [1]WSkillCooldown, [2]ESkillCooldown, [3]UltCooldown, [4]PSkillCooldown
    public player player;
    public Items items;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
        items = GameObject.Find("Items").GetComponent<Items>();
    }

    void Update () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            for (int i = 0; i < 5; i++) {
                if (player.skillMaxCooldown[i] - GameObject.FindGameObjectWithTag ("Items").GetComponent<Items> ().Effect[i + 11] > 0) CooldownArray[i].fillAmount = player.skillCooldown[i] / (player.skillMaxCooldown[i] - items.Effect[i + 11]);
                else CooldownArray[i].fillAmount = 0f;
            }
        }
    }
}