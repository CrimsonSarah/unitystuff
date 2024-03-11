using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class bossHealthBar : MonoBehaviour {
    private GameObject bossHealthObject;
    public float[] hudBossLife = new float[2];
    public Image bossLifeBar;
    public Text lifeText;
    public mob boss;

    void Awake () {
        bossHealthObject = GameObject.Find("BossHealthBG");

        if (GameObject.Find("Boss(Clone)")) {
            boss = GameObject.Find("Boss(Clone)").GetComponent<mob>();
        }
    }

    // Update is called once per frame
    void Update () {
        hudBossLife[1] = boss.life[1];
        hudBossLife[0] = boss.life[0];
        lifeText.text = hudBossLife[1].ToString ("N0") + "/" + hudBossLife[0].ToString ("N0");
        bossLifeBar.fillAmount = hudBossLife[1] / hudBossLife[0];
        
        if (boss.life[1] <= 0) {
            bossHealthObject.SetActive(false);
        }
    }
}