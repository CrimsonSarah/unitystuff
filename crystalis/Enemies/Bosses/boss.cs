using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour {
    [Header ("Attributes")]
    public float[] skillMaxCooldown = new float[3];
    public float[] skillMaxDuration = new float[3];
    public float[] skillTickTime = new float[3];
    public float[] skillCooldown = new float[3];
    public float[] skillDuration = new float[3];
    public float[] skillManaCost = new float[3];
    public float[] skillRadius = new float[3];
    public float[] skillPower = new float[3];
    public float[] skillRange = new float[3];
    public bool[] skillEnabled = new bool[3];

    [Header ("Unity Setup")]
    public mob mobBase;
    public wavespawner director;

    void Start() {
        mobBase = GetComponent<mob>();
        director = GameObject.Find("Director").GetComponent<wavespawner>();
    }

    // Update is called once per frame
    void Update () {
        if (Vector3.Distance (transform.position, GameObject.FindGameObjectWithTag ("Player").transform.position) <= skillRange[0]) {
            BossSkill1();
        }
        skillDuration[0] -= Time.deltaTime;
        skillTickTime[0] -= Time.deltaTime;
        skillCooldown[0] -= Time.deltaTime;
    }

    //damage aura
    void BossSkill1 () {
        if (skillEnabled[0]) {
            if (Vector3.Distance (transform.position, GameObject.FindGameObjectWithTag ("Player").transform.position) <= skillRange[0] && skillTickTime[0] <= 0) {
                GameObject.FindGameObjectWithTag ("Player").GetComponent<player> ().TakeDamage ((skillPower[0] * director.waveindex) / 4, 1);
                skillTickTime[0] = 0.25f;
            }
            if (skillDuration[0] <= 0f) {
                skillEnabled[0] = false;
            }
        } else if (skillCooldown[0] <= 0 && mobBase.mana[1] >= skillManaCost[0]) {
            skillEnabled[0] = true;
            skillDuration[0] = skillMaxDuration[0];
            mobBase.mana[1] -= skillManaCost[0];
            skillCooldown[0] = skillMaxCooldown[0];
        }
    }
}