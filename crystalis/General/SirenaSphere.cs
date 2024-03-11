using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirenaSphere : MonoBehaviour
{
    private player player;
    private skills skills;
    private Items Items;
    [SerializeField]
    private GameObject[] particles;
    public bool nearest;

    void Start() {
        Items = GameObject.Find("Items").GetComponent<Items>();
        skills = GameObject.Find("Sirena").GetComponent<skills>();
        player = GameObject.Find("Sirena").GetComponent<player>();

        if (!GameObject.Find("SirenaSphere 1")) gameObject.name = "SirenaSphere 1";
        else if (!GameObject.Find("SirenaSphere 2")) gameObject.name = "SirenaSphere 2";
        else if (!GameObject.Find("SirenaSphere 3")) gameObject.name = "SirenaSphere 3";

        transform.position = new Vector3 (transform.position.x, 7f, transform.position.z);

        Collider[] colliders = Physics.OverlapSphere(transform.position, player.skillRadius[0]);
        foreach (Collider nearbyObject in colliders) {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null && rb.transform.root.tag == "Mob") {
                rb.transform.root.GetComponent<mob>().TakeDamage(player.skillPower[player.charInstance, 1] + Items.Effect[16] + (player.characterAttributes[2] * 0.2f), 1);
            }
        }
    }
    
    void Update() {
        if (nearest) {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        } else {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
    }

    public void CreateParticle (int i) {
        if(transform.childCount > 0) foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
        Instantiate(particles[i], transform.position, Quaternion.Euler(90,0,0), transform);
        foreach (Transform child in transform) {
            ParticleSystem particles = child.gameObject.GetComponent<ParticleSystem>();
            particles.startSize = (player.skillRadius[i + 1] + Items.Effect[i + 26] + 30);
        }
    }
}
