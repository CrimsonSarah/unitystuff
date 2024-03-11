using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freeaimweaponbehaviour : MonoBehaviour
{
    public Animator animator;
    public Vector3 target;
    
    public virtual void Start () {
        animator = GetComponent<Animator>();
        UnlockAtk();
    }

    public virtual void Update() {
        if (Input.GetButtonDown("combatLock")) {
            animator.SetBool("combatLock", true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        if (Input.GetButtonUp("combatLock")) {
            animator.SetBool("combatLock", false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetButtonDown("Attack1") && animator.GetBool("combatLock") && animator.GetBool("canAttack")) {
            animator.SetTrigger("atk");
            SetTarget();
        }
    }

    public virtual void SetTarget() {
        Vector3 mousePos = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity)) {
            target = hit.point;
        }
    }

    public void ResetAtkTrigger () {
        animator.ResetTrigger("atk");
    }

    public void LockAtk () {
        animator.SetBool("canAttack", false);
    }

    public void UnlockAtk () {
        animator.SetBool("canAttack", true);
    }
}
