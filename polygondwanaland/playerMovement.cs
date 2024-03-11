using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("Camera Control")]
    [SerializeField]
    private float mouseSense;
    private float[] rotation = {0,0}; //0:X, 1:Y
    private Transform camera;
    private Transform orientation;
    private Quaternion oldCamRotation;
    private Quaternion oldOrientationRotation;

    [Header("3D Movement")]
    [SerializeField]
    private LayerMask Ground;
    private bool grounded;
    private Rigidbody playerrb;
    private Vector3 moveDirection;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float groundDrag;
    [SerializeField]
    private float playerHeight;
    private float[] input = {0,0}; //0:X (horizontal), 1:Y (vertical)

    [Header("Combat")]
    [SerializeField]
    private float dashCD = 0f;
    [SerializeField]
    private float dashCDMax;
    public bool combatLock;

    // Start is called before the first frame update
    void Start() {
        camera = GameObject.Find("cameraHolder").GetComponent<Transform>();
        orientation = GameObject.Find("player").GetComponent<Transform>();
        playerrb = gameObject.GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerrb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update() {
        if (!combatLock) {
            CameraMovement();
        } else {
            oldCamRotation = camera.transform.rotation;
            oldOrientationRotation = orientation.rotation;
        }
        HandleDrag();
        HandleCombatLock();
    }

    private void FixedUpdate() {
        if (!combatLock) {
            PlayerMovement();    
        }
        HandleDash();
    }

    private void CameraMovement () {
        float cursorX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSense;
        float cursorY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSense;

        rotation[0] -= cursorY;
        rotation[0] = Mathf.Clamp(rotation[0], -90f, 90f);
        rotation[1] += cursorX;

        camera.transform.rotation = Quaternion.Euler(rotation[0], rotation[1], 0);
        orientation.rotation = Quaternion.Euler(0, rotation[1], 0);
    }

    private void PlayerMovement () {
        input[0] = Input.GetAxisRaw("Horizontal");
        input[1] = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * input[1] + orientation.right * input[0];
        playerrb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);

        Vector3 flatVel = new Vector3(playerrb.velocity.x, 0f, playerrb.velocity.z);
        if (flatVel.magnitude > moveSpeed) {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            playerrb.velocity = new Vector3(limitedVel.x, playerrb.velocity.y, limitedVel.z);
        }
    }

    private void HandleDrag () {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);

        if (grounded) {
            if (combatLock) {
                playerrb.drag = groundDrag * 2;
            } else {
                playerrb.drag = groundDrag;
            }
        } else {
            playerrb.drag = 0f;
        }
    }

    private void HandleCombatLock () {
        if (Input.GetButtonDown("combatLock")) {
            combatLock = true;
        }
        if (Input.GetButtonUp("combatLock")) {
            combatLock = false;
            camera.transform.rotation = oldCamRotation;
            orientation.rotation = oldOrientationRotation;
        }
    }

    private void HandleDash () {
        if (dashCD <= 0f) {
            if (Input.GetButtonDown("dashButton") && combatLock) {
                dashCD = dashCDMax;
                input[0] = Input.GetAxisRaw("Horizontal");
                input[1] = Input.GetAxisRaw("Vertical");

                moveDirection = orientation.forward * input[1] + orientation.right * input[0];
                playerrb.AddForce(moveDirection.normalized * moveSpeed * 200f, ForceMode.Force);
            }
        } else {
            dashCD -= Time.deltaTime;
        }
    }
}
