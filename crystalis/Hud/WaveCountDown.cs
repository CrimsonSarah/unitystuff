using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveCountDown : MonoBehaviour {
    public Text countDownText;
    private float time;
    private string minute;

    // Update is called once per frame
    void Update () {
        time = GameObject.Find ("Director").GetComponent<wavespawner> ().countdown;
        if ((time / 60f) > 1f) {
            minute = (time / 60f).ToString ("N0");
            if (Mathf.Round (time % 60f) > 9f) minute += ":";
            else minute += ":0";
        } else minute = "";
        countDownText.text = "Next Wave In: " + minute + (time % 60f).ToString ("N0");
    }
}