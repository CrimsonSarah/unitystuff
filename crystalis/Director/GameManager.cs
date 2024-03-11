using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Texture2D[] cursor = new Texture2D[3];
    public CanvasGroup MapBG;
    [SerializeField]
    private player player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
        MapBG = GameObject.Find("MapBG").GetComponent<CanvasGroup>();
    }

    void Update() {
        if (Input.GetKeyDown ("tab") && Time.timeScale == 1f) {
            MapBG.alpha = MapBG.alpha == 0f ? 1f : 0f;
            MapBG.blocksRaycasts = !MapBG.blocksRaycasts;
        }
        if (Input.GetKeyDown("left alt") || Input.GetKeyDown("right alt")) {
            GameObject[] ItemBGs = GameObject.FindGameObjectsWithTag("ItemBG");
            foreach (GameObject ItemBG in ItemBGs) {
                ItemBG.GetComponent<Image>().raycastTarget = true;
            }
        }
        if (Input.GetKeyUp("left alt") || Input.GetKeyUp("right alt")) {
            GameObject[] ItemBGs = GameObject.FindGameObjectsWithTag("ItemBG");
            foreach (GameObject ItemBG in ItemBGs) {
                ItemBG.GetComponent<Image>().raycastTarget = false;
            }
        }
        if (player.skillEnabled[0] || player.skillEnabled[1] || player.skillEnabled[2] || player.skillEnabled[3] || player.skillEnabled[4]) Cursor.SetCursor(cursor[1], new Vector2(16f, 16f), CursorMode.ForceSoftware);
        else if (player.AAMove) Cursor.SetCursor(cursor[2], Vector2.zero, CursorMode.ForceSoftware);
        else Cursor.SetCursor(cursor[0], Vector2.zero, CursorMode.ForceSoftware);
    }
}
