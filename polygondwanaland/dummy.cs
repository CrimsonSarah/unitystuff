using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummy : MonoBehaviour
{
    private int frames;
    private bool iFrames;
    [SerializeField]
    private int maxIFrames;
    private Animator animator;

    [Header("Stats")]
    public float health;

    void Start () {
        animator = GetComponent<Animator>();
        iFrames = false;
        health = 10;
    }

    void Update () {
        if (health <= 0f) {
            Destroy(gameObject);
        }
        HandleIFrames();
    }

    private void OnTriggerEnter (Collider other) {
        if (iFrames) {
            return;
        } else if (other.tag == "Weapon" || other.tag == "Projectile") {
            TakeDamage(other.gameObject.GetComponent<stats>().damage);
        }
    }

    private void TakeDamage (float dmg) {
        animator.SetTrigger("tookDmg");
        iFrames = true;
        health -= dmg;
    }

    private void HandleIFrames () {
        if (iFrames) {
            frames ++;
        }

        if (frames >= maxIFrames) {
            iFrames = false;
            frames = 0;
        }
    }
}
