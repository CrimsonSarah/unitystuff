using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBar : MonoBehaviour {
    public Image castleLevelBar;
    public Text LevelText;
    public float[] hudCastleLevel = new float[2];
    public castle castle;

    // Update is called once per frame
    void Update () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            hudCastleLevel[0] = castle.level[0];
            hudCastleLevel[1] = castle.level[1];
            LevelText.text = hudCastleLevel[1].ToString ("N0") + "/" + hudCastleLevel[0].ToString ("N0");
            castleLevelBar.fillAmount = hudCastleLevel[1] / hudCastleLevel[0];
        }
    }
}