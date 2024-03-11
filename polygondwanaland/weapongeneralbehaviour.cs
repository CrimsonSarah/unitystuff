using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapongeneralbehaviour : MonoBehaviour
{
    [SerializeField]
    private float mouseSense;
    private Animator animator;
    private bool rotationIsLocked;
    private float[] rotation = {0, 0}; //0:X, 1:Y
    private float[] storedRotation = {0, 0}; //0:X, 1:Y
    [SerializeField]
    private Collider hitbox;
    
    void Start () {
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (!rotationIsLocked) {
            rotation[0] += Input.GetAxis("Mouse X") * mouseSense;
            rotation[1] += Input.GetAxis("Mouse Y") * mouseSense;

            rotation[0] = Mathf.Clamp(rotation[0], -1f, 1f);
            rotation[1] = Mathf.Clamp(rotation[1], -1f, 1f); 
            
            animator.SetFloat("X", rotation[0]);
            animator.SetFloat("Y", rotation[1]);
        }

        if (Input.GetButtonDown("combatLock")) {
            animator.SetBool("combatLock", true);
        }
        if (Input.GetButtonUp("combatLock")) {
            animator.SetBool("combatLock", false);
        }
        if (Input.GetButtonDown("Attack1") && animator.GetBool("combatLock") && !animator.GetBool("lockedRotation")) {
            animator.SetTrigger("atk");
            animator.SetFloat("X", -rotation[0]);
            animator.SetFloat("Y", -rotation[1]);
        }
    }

    public void ResetAtkTrigger () {
        animator.ResetTrigger("atk");
    }

    public void StoreRotation () {
        rotationIsLocked = true;
        animator.SetBool("lockedRotation", true);
        storedRotation[0] = rotation[0];
        storedRotation[1] = rotation[1];
    }

    public void MaintainRotation () {
        rotation[0] = -storedRotation[0];
        rotation[1] = -storedRotation[1];
        animator.SetFloat("X", rotation[0]);
        animator.SetFloat("Y", rotation[1]);
    }

    public void InvertRotation () {
        rotation[0] = -storedRotation[0];
        rotation[1] = -storedRotation[1];
        animator.SetFloat("X", rotation[0]);
        animator.SetFloat("Y", rotation[1]);
        rotationIsLocked = false;
        animator.SetBool("lockedRotation", false);
    }

    public void ResetInput () {
        rotation[0] = 0f;
        rotation[1] = 0f;
    }

    public void ActivateHitbox () {
        hitbox.enabled = true;
    }

    public void DeactivateHitbox () {
        hitbox.enabled = false;
    }
}
