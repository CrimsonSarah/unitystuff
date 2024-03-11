using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelDisplay : MonoBehaviour
{
    public Text characterLevel;
    public player player;

    // Update is called once per frame
    void Update() {
        if (GameObject.FindGameObjectWithTag("Player")) {
            characterLevel.text = player.characterExp[2].ToString("N0");
        }
    }
}
