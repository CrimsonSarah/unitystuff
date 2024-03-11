using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gold : MonoBehaviour {
    public Text goldStash;
    private float playerGold;
    public player player;

    // Update is called once per frame
    void Update () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            playerGold = player.gold;
            goldStash.text = playerGold.ToString ();
        }
    }
}