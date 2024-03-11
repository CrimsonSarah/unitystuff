using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rapier : freeaimweaponbehaviour
{
    [SerializeField]
    private Transform pivot;
    private bool thrusting;
    private Vector3 origin;
    [SerializeField]
    private float range;
    [SerializeField]
    private float speed;

    public override void Start () {
        origin = pivot.localPosition;
        base.Start();
    }

    public override void Update() {
        if (Input.GetKeyDown("h")) {
            UnlockAtk();
        }

        if (Input.GetKeyDown("u")) {
            Debug.Log(thrusting);
        }

        if (thrusting) {
            pivot.localPosition += transform.forward * Time.deltaTime * speed;
        }

        if (Vector3.Distance(pivot.localPosition, origin) >= range) {
            thrusting = false;
        }

        if (!thrusting && pivot.localPosition != origin) {
            pivot.localPosition = Vector3.MoveTowards(pivot.localPosition, origin, speed * Time.deltaTime);
        }

        if(!thrusting && pivot.localPosition == origin && !animator.GetBool("canAttack")) {
            animator.SetBool("canAttack", true);
        }

        base.Update();
    }

    public void Thrust () {
        gameObject.transform.LookAt(target);
        thrusting = true;
    }
}
