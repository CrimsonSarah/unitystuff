using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject node;
    [SerializeField] private List<GameObject> childNodes = new List<GameObject>();
    private Transform nodePanel;

    void Start() {
        nodePanel = GameObject.Find("puzzle panel").GetComponent<Transform>();
        childNodes.Add(Instantiate(node, transform.position, Quaternion.identity, nodePanel));
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (childNodes.Count < 8 && other.gameObject.tag == "Puzzle Node") childNodes.Add(Instantiate(node, transform.position, Quaternion.identity, nodePanel));
    }
}
