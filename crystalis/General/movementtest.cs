using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class movementtest : MonoBehaviour
{
    private NavMeshAgent agent;
    private Renderer renderer;
    public Animator anim;
    private Vector3 pos;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        renderer = GameObject.Find("Merged_PolySphere").GetComponent<Renderer>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButton(1)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.tag == "Ground") {
                    agent.destination = hit.point;
                    anim.Play("Moving1");
                }
            }
        }

        if (Input.GetKeyDown("1")) {
            int atknum = Random.Range(0, 2);

            switch (atknum) {
                case 0:
                    anim.Play("Attack1");
                    break;
                default:
                    anim.Play("Attack2");
                    break;
            }
        }

        if (Input.GetKeyDown("2")) anim.Play("E");
        
        if (Input.GetKeyDown("3")) StartCoroutine(R1());

        if (Input.GetKeyDown("4")) anim.Play("Death");

        if (Input.GetKeyDown("5")) anim.Play ("Critical");
    }

    private void LateUpdate() {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Moving1") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Death") && transform.position == agent.destination) anim.Play("Idle");
    }

    IEnumerator R1 () {
        renderer.material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(2f);
        renderer.material.SetColor("_Color", Color.white);
    }
}
