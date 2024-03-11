using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeBar : MonoBehaviour {
    public Image playerHealthBar;
    public Text playerHealthText;
    public float[] hudCharacterHealth = new float[2];
    public player player;

    // Update is called once per frame
    void Update () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            hudCharacterHealth[0] = player.characterHealth[0];
            hudCharacterHealth[1] = player.characterHealth[1];
            playerHealthText.text = hudCharacterHealth[1].ToString ("N0") + "/" + hudCharacterHealth[0].ToString ("N0");
            playerHealthBar.fillAmount = hudCharacterHealth[1] / hudCharacterHealth[0];
        }
    }
}