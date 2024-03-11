using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour {
    public Image ExpBar;
    public Text ExpText;
    public float[] hudCharacterExp = new float[2];
    public player player;

    // Update is called once per frame
    void Update () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            hudCharacterExp[0] = player.characterExp[0];
            hudCharacterExp[1] = player.characterExp[1];
            ExpText.text = hudCharacterExp[1].ToString ("N0") + "/" + hudCharacterExp[0].ToString ("N0");
            ExpBar.fillAmount = hudCharacterExp[1] / hudCharacterExp[0];
        }
    }
}