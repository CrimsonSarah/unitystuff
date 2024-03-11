using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManaBar : MonoBehaviour {
    public Image playerManaBar;
    public Text playerManaText;
    public float[] hudCharacterMana = new float[2];
    public player player;

    // Update is called once per frame
    void Update () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            hudCharacterMana[0] = player.characterMana[0];
            hudCharacterMana[1] = player.characterMana[1];
            playerManaText.text = hudCharacterMana[1].ToString ("N0") + "/" + hudCharacterMana[0].ToString ("N0");
            playerManaBar.fillAmount = hudCharacterMana[1] / hudCharacterMana[0];
        }
    }
}