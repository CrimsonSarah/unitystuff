using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveWeapon : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;
    private Vector3 weaponPosition;

    // Start is called before the first frame update
    void Start() {
        weaponPosition = GameObject.Find("orientation").GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update() {
        gameObject.GetComponent<Transform>().position = new Vector3 (offset.x + weaponPosition.x, weaponPosition.y, offset.z + weaponPosition.z);
    }
}
