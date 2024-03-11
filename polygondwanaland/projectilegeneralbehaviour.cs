using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectilegeneralbehaviour : MonoBehaviour
{
    [SerializeField]
    private float projectilespeed;
    private Transform transform;

    void Start() {
        transform = GetComponent<Transform>();
    }

    void Update() {
        transform.position += transform.forward * Time.deltaTime * projectilespeed;
    }

    void OnTriggerEnter() {
        Destroy(gameObject);
    }
}
