using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    private Transform cameraPosition;

    // Start is called before the first frame update
    void Start() {
        cameraPosition = GameObject.Find("orientation").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {
        gameObject.GetComponent<Transform>().position = cameraPosition.position;
    }
}
