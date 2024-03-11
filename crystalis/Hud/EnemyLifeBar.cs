using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLifeBar : MonoBehaviour {
    public Image enemyLifeBar;
    public Text LifeText;
    public float[] hudEnemyLife = new float[2];
    public player player;

    // Update is called once per frame
    void Update () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            if (player.target) {
                hudEnemyLife[1] = player.target.GetComponent<mob> ().life[1];
                hudEnemyLife[0] = player.target.GetComponent<mob> ().life[0];
                LifeText.text = hudEnemyLife[1].ToString ("N0") + "/" + hudEnemyLife[0].ToString ("N0");
                enemyLifeBar.fillAmount = hudEnemyLife[1] / hudEnemyLife[0];
            }
        }
    }
}