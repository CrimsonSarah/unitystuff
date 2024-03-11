using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HogProjectile : MonoBehaviour
{
    [SerializeField]
    private Vector3 destination;
    private float yValue;
    private player player;
    private Items items;
    private bool moving, damageDealt;
    public bool stuck;
    public float speed, stuckDuration;
    public Rigidbody rb;
    private Animator anim;

    void Awake() {
        anim = transform.GetChild(0).gameObject.GetComponent<Animator>();
        player = GameObject.Find("Hog").GetComponent<player>();
        items = GameObject.Find("Items").GetComponent<Items>();
        yValue = transform.position.y;
        moving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Hog") && Time.timeScale == 1f) {
            if (!stuck) {
                if (Mathf.Round(transform.position.x) != Mathf.Round(destination.x) && Mathf.Round(transform.position.z) != Mathf.Round(destination.z)) {
                    transform.position = Vector3.MoveTowards(transform.position, destination, speed);
                    transform.position = new Vector3(transform.position.x, yValue, transform.position.z);
                }
                else if (!damageDealt) {
                    Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, player.skillRadius[0] + items.Effect[33]);
                    foreach (Collider nearbyObject in colliders)
                    {
                        Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                        if (rb != null && rb.transform.root.tag == "Mob")
                        {
                            rb.transform.root.GetComponent<mob>().TakeDamage((player.skillPower[0, 1] + items.Effect[19]) + player.characterAttributes[0] + player.characterAttributes[1], 1);
                            rb.transform.root.GetComponent<mob>().isStuned = true;
                            rb.transform.root.GetComponent<mob>().stunDuration -=- player.skillPower[0, 2];
                        }
                    }
                    damageDealt = true;
                    moving = false;
                    anim.SetBool("Moving", false);
                }
            } else {
                stuckDuration -= Time.deltaTime;
                damageDealt = true;
                moving = false;
                anim.SetBool("Moving", false);
                if (stuckDuration <= 0) stuck = false;
            }
        } else if (!GameObject.Find("Hog")) Destroy(gameObject);
    }

    public void GoTo(Vector3 point) {
        destination = point;
        anim.speed = (Vector3.Distance(transform.position, destination) / 60);
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player" && !moving && !stuck) {
            other.transform.GetChild(0).gameObject.SetActive(true);
            other.transform.gameObject.GetComponent<player>().charInstance = 0;
            other.transform.gameObject.GetComponent<player>().anim.SetInteger("CharInstance", 0);
            Destroy(gameObject);
        }
    }
}
