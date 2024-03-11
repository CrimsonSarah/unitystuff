using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleLifeBar : MonoBehaviour {
    public Image castleLifeBar;
    public Text lifeText;
    public float hudCastleLife;
    public float hudCastleMaxLife;
    public castle castle;

    // Update is called once per frame
    void Update () {
        hudCastleLife = castle.health[1];
        hudCastleMaxLife = castle.health[0];
        lifeText.text = hudCastleLife.ToString("N0") + "/" + hudCastleMaxLife.ToString("N0");
        castleLifeBar.fillAmount = hudCastleLife / hudCastleMaxLife;
    }
}