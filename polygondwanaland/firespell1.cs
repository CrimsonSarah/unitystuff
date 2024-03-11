using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firespell1 : freeaimweaponbehaviour
{
    [SerializeField]
    private GameObject fireball;
    private Transform firepoint;

    public override void Start() {
        firepoint = GameObject.Find("firepoint").GetComponent<Transform>();
        base.Start();
    }

    private void SpawnProjectile () {
        GameObject projectile = Instantiate(fireball, firepoint.position, Quaternion.identity);
        projectile.transform.LookAt(target);
    }
}
