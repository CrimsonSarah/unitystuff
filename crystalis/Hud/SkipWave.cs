using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipWave : MonoBehaviour {
    public Button skipButton;

    public void Skip () {
        if (!GameObject.FindGameObjectWithTag("Mob")){
            GameObject.Find("Director").GetComponent<wavespawner>().countdown = 0;
        }
    }
}
