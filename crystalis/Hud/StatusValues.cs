using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusValues : MonoBehaviour {
    public Text[] attributes = new Text[3];
    public player player;
    public Sprite[] sprites = new Sprite[2];
    public Image playerIcon;

    void Start () {
        playerIcon = GameObject.Find("CharModel").GetComponent<Image>();
        switch (player.selectedChar)
        {
            case 1:
                playerIcon.sprite = sprites[1];
                break;
            default:
                playerIcon.sprite = sprites[0];
                break;
        }
    }

    // Update is called once per frame
    void Update () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            attributes[0].text = player.characterAttributes[0].ToString ();
            attributes[1].text = player.characterAttributes[1].ToString ();
            attributes[2].text = player.characterAttributes[2].ToString ();
        }
    }
}