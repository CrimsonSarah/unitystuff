using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wavespawner : MonoBehaviour {
    public Transform[] spawnpoints = new Transform[6];
    public Transform placeholderenemy, placeholderboss;
    public float countdown, waveindex, timer;
    public float maxCredits, waveCredits, spawnCredits;
    private int difficulty;

    void Start () {
        difficulty = gameObject.GetComponent<Manager>().difficulty;

        switch (difficulty) {
            case 1:
                maxCredits = 40;
                break;
            case 3:
                maxCredits = 90;
                break;
            default:
                maxCredits = 60;
                break;
        }
        spawnCredits = maxCredits;
        waveCredits = maxCredits / 2f;
        countdown = 0;
        waveindex = -1;
    }

    // Update is called once per frame
    void LateUpdate () {
        if (countdown <= 0f) {
            StartCoroutine (spawnwave ());
            if (waveindex >= 0) StartCoroutine (StartWave ());
            waveindex++;
            if (waveindex % 10 == 0 && waveindex != 0) {
                switch (difficulty) {
                    case 1:
                        maxCredits -=- 4.5f;
                        break;
                    case 3:
                        maxCredits -=- 9f;
                        break;
                    default:
                        maxCredits -=- 6f;
                        break;
                }
            }
            countdown = timer;
        }

        countdown -= Time.deltaTime;

        if (waveindex == 10) {
            gameObject.GetComponent<UnlockablesManager>().unlockableMatrix.SirenaUnlocked = true;
            gameObject.GetComponent<UnlockablesManager>().SaveJson();
        }
    }

    IEnumerator spawnwave () {
        while (spawnCredits > 0) {
            spawnenemy ();
            spawnCredits -= placeholderenemy.GetComponent<mob> ().spawnCost;
            if (placeholderenemy.GetComponent<mob>().spawnCost > spawnCredits) break;
            yield return new WaitForSeconds (1f);
        }
    }

    IEnumerator StartWave () {
        if ((waveindex + 1) % 10 == 0 && waveindex != 0) spawnboss ();

        while (waveCredits > 0) {
            spawnenemy ();
            waveCredits -= placeholderenemy.GetComponent<mob> ().spawnCost;
            if (placeholderenemy.GetComponent<mob>().spawnCost > waveCredits) break;
            yield return new WaitForSeconds (0.25f);
        }

        GameObject[] mobs = GameObject.FindGameObjectsWithTag ("Mob");
        foreach (GameObject mob in mobs) {
            mob.GetComponent<mob> ().aggroed = GameObject.Find ("castleCore");
            mob.GetComponent<mob> ().wasCalled = true;
        }

        spawnCredits = maxCredits;
        waveCredits = maxCredits / 2f;
    }

    void spawnenemy () {
        spawnCredits -= placeholderenemy.GetComponent<mob> ().spawnCost;
        Transform selectedspawn = spawnpoints[Random.Range (0, spawnpoints.Length - 1)];
        Instantiate (placeholderenemy, selectedspawn.position, selectedspawn.rotation);
    }

    void spawnboss () {
        Transform selectedspawn = spawnpoints[Random.Range (0, spawnpoints.Length - 1)];
        Instantiate (placeholderboss, selectedspawn.position, selectedspawn.rotation);
    }
}