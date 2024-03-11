using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offensiveCastle : MonoBehaviour {
    [Header ("Atributes")]
    public float damage;
    public float attackSpeed;
    public float lifesteal;
    public int maxTargets;
    public bool lifestealAura, doubleAttack;
    private float delayCounters;
    private GameObject[] targets = new GameObject[5];

    [Header ("Unity Setup")]
    [SerializeField]
    private GameObject castleAuraParticle;
    private castle castle;

    void Start () {
        castle = gameObject.GetComponent<castle>();
        damage = 30f;
        attackSpeed = 0.6f;
        lifesteal = 0.25f;
        maxTargets = 2;
        delayCounters = 0.1f;
        lifestealAura = false;
        doubleAttack = false;
        for (int i = 0; i < targets.Length; i++) {
            targets[i] = null;
        }
    }

    // Update is called once per frame
    public virtual void Update () {
        if (delayCounters <= 0f) {
            for (int i = 0; i < maxTargets; i++) {
                if (targets[i] != null) {
                    if (targets[i].GetComponent<mob>().life[1] <= (damage - (damage * targets[i].GetComponent<mob>().armor / 100))) {
                        targets[i].GetComponent<mob>().TakeDamage(damage - (damage * targets[i].GetComponent<mob>().armor / 100), 0);
                        castle.health[1] -=- (damage - (damage * targets[i].GetComponent<mob>().armor / 100)) * lifesteal;
                        if (castle.health[1] > castle.health[0]) castle.health[1] = castle.health[0];
                        targets[i] = null;
                    } else {
                        targets[i].GetComponent<mob>().TakeDamage(damage - (damage * targets[i].GetComponent<mob>().armor / 100), 0);
                        castle.health[1] -=- (damage - (damage * targets[i].GetComponent<mob>().armor / 100)) * lifesteal;
                        if (castle.health[1] > castle.health[0]) castle.health[1] = castle.health[0];
                    }
                    if (doubleAttack) {
                        if (targets[i].GetComponent<mob>().life[1] <= (damage - (damage * targets[i].GetComponent<mob>().armor / 100))) {
                            targets[i].GetComponent<mob>().TakeDamage(damage - (damage * targets[i].GetComponent<mob>().armor / 100), 0);
                            castle.health[1] -=- (damage - (damage * targets[i].GetComponent<mob>().armor / 100)) * lifesteal;
                            if (castle.health[1] > castle.health[0]) castle.health[1] = castle.health[0];
                            targets[i] = null;
                        } else {
                            targets[i].GetComponent<mob>().TakeDamage(damage - (damage * targets[i].GetComponent<mob>().armor / 100), 0);
                            castle.health[1] -=- (damage - (damage * targets[i].GetComponent<mob>().armor / 100)) * lifesteal;
                            if (castle.health[1] > castle.health[0]) castle.health[1] = castle.health[0];
                        }
                    }
                }
                delayCounters = 1 / attackSpeed;
            }
        }

        if (castle.castleAura) lifestealAura = true;
        if (lifestealAura && !castleAuraParticle.activeSelf) castleAuraParticle.SetActive(true);

        delayCounters -= Time.deltaTime;
    }

    private void OnTriggerEnter (Collider other) {
        if (other.tag == "Mob") {
            for (int i = 0; i < maxTargets; i++) {
                if (targets[i] == null) {
                    targets[i] = other.gameObject;
                    break;
                }
            }
        }
    }

    private void OnTriggerExit (Collider other) {
        if (other.tag == "Mob") {
            for (int i = 0; i < maxTargets; i++) {
                if (targets[i] == other.gameObject) {
                    targets[i] = null;
                    break;
                }
            }
        }

        if (other.tag == "Player" && lifestealAura) {
            if(other.GetComponent<player>().lifesteal > 0f) other.GetComponent<player> ().lifesteal -= 0.5f;
        }
    }

    private void OnTriggerStay (Collider other) {
        if (other.tag == "Mob") {
            for (int i = 0; i < maxTargets; i++) {
                if (targets[i] == null) {
                    targets[i] = other.gameObject;
                    break;
                }
            }
        }

        if (other.tag == "Player" && lifestealAura) {
            if(other.GetComponent<player>().lifesteal <= 0f) other.GetComponent<player> ().lifesteal -=- 0.5f;
        }
    }
}