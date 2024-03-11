using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveCounter : MonoBehaviour
{
    public Text counterText;

    // Update is called once per frame
    void Update()
    {
        counterText.text = "Wave: " + GameObject.Find("Director").GetComponent<wavespawner>().waveindex.ToString();
    }
}
