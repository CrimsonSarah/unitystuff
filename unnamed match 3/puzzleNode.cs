using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleNode : MonoBehaviour
{
    private main handler;
    private Rigidbody2D rigidbody;
    public SpriteRenderer renderer;

    private GameObject targetNode;
    [SerializeField] private List<GameObject> neighbourNodes = new List<GameObject>();
    private Vector3 targetLocation;
    private bool dragging;

    void Start() {
        handler = GameObject.Find("Handler").GetComponent<main>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();

        renderer.color = handler.availableColors[Random.Range(0, handler.availableColors.Length)];
    }

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.collider) {
                if (hit.collider.gameObject == gameObject) {
                    dragging = true;
                }
            }
        }

        if (dragging) {
            if(Input.GetMouseButton(0)) {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if(hit.collider) {
                    if (hit.collider.gameObject.tag == "Puzzle Node" && hit.collider.gameObject != gameObject) {
                        targetNode = hit.collider.gameObject;
                        targetLocation = targetNode.transform.position;
                    } else targetNode = null;
                }
            }

            if(Input.GetMouseButtonUp(0)) {
                dragging = false;
                if(targetNode != null) {
                    if(targetNode.tag == "Puzzle Node") {
                        targetNode.transform.position = transform.position;
                        transform.position = targetLocation;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Puzzle Node")  neighbourNodes.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Puzzle Node") neighbourNodes.Remove(other.gameObject);
    }
}
