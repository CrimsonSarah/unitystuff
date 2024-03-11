using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castle : MonoBehaviour {
    [Header ("Atributes")]
    public float armor;
    public bool castleAura;
    public float[] health = new float[3];
    public float[] level = new float[2];
    public int[] upgradeCost = new int[3];
    private float[] delayCounters = new float[2];

    [Header ("Unity Setup")]
    [SerializeField]
    public GameObject toDestroy;
    public GameObject GameOverMenu;

    void Start () {
        health[1] = health[0];
        level[0] = 10f;
        delayCounters[0] = 0.1f;
        castleAura = false;
    }

    // Update is called once per frame
    void Update () {
        if (health[1] <= 0) {
            toDestroy.SetActive(false);
            GameOverMenu.transform.GetChild(0).gameObject.SetActive(true);
            GameOverMenu.GetComponent<gameOverMenu>().isOver = true;
            GameObject.Find("CastleHealthBG").SetActive(false);
            Time.timeScale = 0f;
        }
        if (delayCounters[0] <= 0f && health[1] < health[0] && Time.timeScale == 1f) {
            health[1] -= -health[2] / 100f;
            if (health[1] > health[0]) {
                health[1] = health[0];
            }
        }
        delayCounters[0] -= Time.deltaTime;
    }
}