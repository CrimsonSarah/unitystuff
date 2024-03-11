using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RProjectile : MonoBehaviour {
    private Vector3 direction, destination, path, initialPosition;
    public float speed;
    private float yValue, distanceThisFrame, temp;
    private bool goForward;
    public Rigidbody rb;
    private player Default;
    private Items Items;

    void Start () {
        yValue = 5f;
        direction.x = destination.x - transform.position.x;
        direction.z = destination.z - transform.position.z;
        direction.y = yValue;
        initialPosition = transform.position;
        path = initialPosition;

        Default = GameObject.Find("default").GetComponent<player>();
        Items = GameObject.Find("Items").GetComponent<Items>();
    }

    // Update is called once per frame
    void Update () {
        if (transform.position.x <= -1000f || transform.position.x >= 0f || transform.position.x >= 1000f || transform.position.z <= 0f) {
            Destroy(gameObject);
        }
        distanceThisFrame = speed * Time.deltaTime;
        temp = Mathf.Sqrt (Mathf.Pow (direction.x, 2f) + Mathf.Pow (direction.z, 2f)) / 1f;
        direction.x = direction.x / temp;
        direction.z = direction.z / temp;
        path.x += direction.x;
        path.z += direction.z;
        gameObject.transform.position = path;
    }

    public void GoTo (Vector3 point) {
        destination = point;
    }

    private void OnTriggerEnter (Collider other) {
        if (other.tag == "Mob") {
            Collider[] colliders = Physics.OverlapSphere (other.transform.position, Default.skillRadius[3] + Items.Effect[33]);
            foreach (Collider nearbyObject in colliders) {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody> ();
                if (rb != null && rb.transform.root.tag == "Mob") {
                    rb.transform.root.GetComponent<mob> ().TakeDamage (Default.skillPower[Default.charInstance,3] + Items.Effect[19] + (Default.characterAttributes[2] * 1.25f), 1);
                }
            }
            Destroy (gameObject);
        }
    }
}