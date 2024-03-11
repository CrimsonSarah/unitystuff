using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defaultCastle : MonoBehaviour {
    [Header ("Atributes")]
    public bool healthRegenAura;

    [Header ("Unity Setup")]
    [SerializeField]
    private GameObject castleAuraParticle;
    private castle castle;

    void Start () {
        castle = gameObject.GetComponent<castle>();
        healthRegenAura = false;
    }

    // Update is called once per frame
    void Update () {
        if (castle.castleAura) healthRegenAura = true; 
        if (healthRegenAura && !castleAuraParticle.activeSelf) castleAuraParticle.SetActive(true);
    }

    private void OnTriggerStay (Collider other) {
        if (other.tag == "Player" && healthRegenAura) other.GetComponent<player> ().characterHealth[1] -= -(castle.health[2] / 8) / 100;
    }
}