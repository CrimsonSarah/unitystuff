using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;

public class allDirAim : MonoBehaviour
{
    private playerMovement player;
    private Image img;
    private RectTransform rect;

    void Start() {
        player = GameObject.Find("player").GetComponent<playerMovement>();
        img = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }

    void Update() {
        if (player.combatLock) {
            img.enabled = true;
            // rect.eulerAngles += new Vector3(0f, 0f, -Input.GetAxisRaw("Mouse X") * 2);
            Vector3 mousePos = Input.mousePosition;
            // mousePos.z = 5.23f;
 
            Vector3 objectPos = Camera.main.WorldToScreenPoint (rect.position);
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;
 
            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            rect.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        } else {
            img.enabled = false;
            rect.eulerAngles = new Vector3(0f, 0f, 0f);
        }
    }

    //  criar parente para a reticula do retangulo (pode ser a mira livre), dps fazer o retangulo girar para onde está a reticula
}
