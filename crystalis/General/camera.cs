using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {
    public Vector3 offset;
    public float scrollSpeed = 5f;
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public float minY = 10f;
    public float maxY = 75f;
    public float posZ;
    public bool isFixed;
    public Transform player;

    void Start() {
        posZ = offset.z * (transform.position.y / maxY);
        isFixed = false;
    }

    // Update is called once per frame
    void Update () {
        Vector3 pos = transform.position;

        if (Input.mousePosition.y >= Screen.height - panBorderThickness || Input.GetKey("up")) {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= panBorderThickness || Input.GetKey("down")) {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x >= Screen.width - panBorderThickness || Input.GetKey("right")) {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x <= panBorderThickness || Input.GetKey("left")) {
            pos.x -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown ("f")) {
            isFixed = !isFixed;
        }
        if (Input.GetKey ("space")) {
            if (GameObject.FindGameObjectWithTag("Player")) pos = new Vector3(player.position.x + offset.x, pos.y, player.position.z + offset.z * (pos.y / maxY));
        }

        float scroll = Input.GetAxis ("Mouse ScrollWheel");
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp (pos.y, minY, maxY);

        pos.z -= posZ;
        posZ = offset.z * (pos.y / maxY);
        pos.z -=- posZ;

        transform.position = isFixed && GameObject.FindGameObjectWithTag("Player") ? new Vector3(player.position.x + offset.x, pos.y, player.position.z + posZ) : new Vector3(Mathf.Clamp(pos.x, -1000 + 250 * (pos.y / maxY) / 2, -250 * (pos.y / maxY) / 2), pos.y, Mathf.Clamp(pos.z, 0 - posZ, 1000 + posZ * 3));
    }
}