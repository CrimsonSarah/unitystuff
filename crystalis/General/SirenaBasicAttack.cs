using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirenaBasicAttack : MonoBehaviour
{
    private player player;
    private skills skills;
    private Items Items;
    private float attackbounces;
    private float bounces;
    private GameObject target;
    private GameObject newtarget;
    private List<GameObject> possibleTargets = new List<GameObject>();
    [SerializeField]
    private GameObject shockParticle;

    void Start() {
        Items = GameObject.Find("Items").GetComponent<Items>();
        skills = GameObject.Find("Sirena").GetComponent<skills>();
        player = GameObject.Find("Sirena").GetComponent<player>();
        target = player.target;
        transform.position = target.transform.position;
        shockParticle.transform.position = player.firePoint.position;
        attackbounces =  player.skillPower[player.charInstance, 0];
        StartCoroutine(AttackBounce(target, attackbounces));
    }

    void Update() {
        if (attackbounces <= 0 || !target) {
            GameObject.Destroy(gameObject);
        } else {
            transform.position = target.transform.position;
            if (shockParticle.transform.position != target.transform.position) shockParticle.transform.position = target.transform.position;
        }
    }

    IEnumerator AttackBounce (GameObject newtarget, float bounces) {
        if (bounces > 0) {
            if (Random.Range(0, 100) < player.critChance) {
                if (newtarget) newtarget.GetComponent<mob> ().TakeDamage (((player.basicDamage * 0.5f) - (player.basicDamage * target.GetComponent<mob> ().armor / 100)) * 2.5f, 0);
                if (player.lifesteal > 0f && newtarget) player.Heal((((player.basicDamage * 0.5f) - (player.basicDamage * target.GetComponent<mob> ().armor / 100)) * 2.5f) * player.lifesteal);
            }
            else {
                if (newtarget) newtarget.GetComponent<mob>().TakeDamage((player.basicDamage * 0.5f) - (player.basicDamage * target.GetComponent<mob>().armor / 100), 0);
                if (player.lifesteal > 0f && newtarget) player.Heal((player.basicDamage * 0.5f) - (player.basicDamage * target.GetComponent<mob>().armor / 100) * player.lifesteal);
            }
            yield return new WaitForSeconds (0.5f);
            this.attackbounces--;
            Collider[] colliders = Physics.OverlapSphere (transform.position, player.skillRadius[4]);
            for (int i = 0; i < colliders.Length; i++) {
                if (colliders[i] != null && colliders[i].transform.root.tag == "Mob") {
                    possibleTargets.Insert(0, colliders[i].transform.root.gameObject); 
                }
            }
            if (possibleTargets.Count > 1) newtarget = possibleTargets[Random.Range(0, possibleTargets.Count -1)];
            else this.attackbounces = 0;
            this.target = newtarget;
            StartCoroutine(AttackBounce(newtarget, this.attackbounces));
        } else {
            this.attackbounces = 0;
        }
    }
}
