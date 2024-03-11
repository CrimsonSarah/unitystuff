using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private int[] eventIndex = new int[1];
    private wavespawner wavespawner;
    private float timePassed;
    private int eventNumber;
    private bool rolled;
    [SerializeField]
    private Quaternion[] npcRotation = new Quaternion[1];
    [SerializeField]
    private GameObject[] npc = new GameObject[1];
    [SerializeField]
    private Vector3[] npcSpawn = new Vector3[1];

    // Start is called before the first frame update
    void Start() {
        wavespawner = GetComponent<wavespawner>();
        timePassed = 0f;
    }

    // Update is called once per frame
    void Update() {
        if (wavespawner.countdown <= wavespawner.timer / 2 && !rolled && wavespawner.waveindex > 0) {
            Debug.Log("aaa chama evetno");
            eventNumber = Random.Range(0, eventIndex.Length - 1);
            rolled = true;
            SpawnEvent(eventNumber);
        }

        if (wavespawner.countdown <= 0f) {
            Debug.Log("aaaa sai evetno");
            DespawnEvent(eventNumber);
            rolled = false;
        }
    }

    public void SpawnEvent(int i) {
        switch (i) {
            case 1:
                Instantiate(npc[0], npcSpawn[0], npcRotation[0]);
                break;
            default:
                break;
        } 
    }

    public void DespawnEvent(int i) {
        switch (i) {
            case 1:
                Destroy(GameObject.Find(npc[0].name));
                break;
            default:
                break;
        } 
    }
}
