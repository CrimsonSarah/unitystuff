using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManaBar : MonoBehaviour {
    public Image enemyManaBar;
    public Text manaText;
    public float[] hudEnemyMana = new float[2];
    public player player;

    // Update is called once per frame
    void Update () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            if (GameObject.FindGameObjectWithTag ("Player").GetComponent<player> ().target) {
                hudEnemyMana[0] = player.target.GetComponent<mob> ().mana[0];
                hudEnemyMana[1] = player.target.GetComponent<mob> ().mana[1];
                manaText.text = hudEnemyMana[1].ToString ("N0") + "/" + hudEnemyMana[0].ToString ("N0");
                enemyManaBar.fillAmount = hudEnemyMana[1] / hudEnemyMana[0];
            }
        }
    }
}