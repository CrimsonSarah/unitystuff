using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
    public Color[] availableColors;

    private void Awake() {
        availableColors = new Color[] {Color.grey, Color.red, Color.blue, Color.yellow};
    }
}
