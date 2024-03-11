using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class map : MonoBehaviour
{
    public camera camera;
    public Vector3 clickPosition;
    public Transform playerTransform, castleTransform, cameraTransform, shopkeeperTransform, upgradekeeperTransform;
    public RectTransform mapRect, playerLocation, castleLocation, cameraLocation, shopkeeperLocation, upgradekeeperLocation;

    // Start is called before the first frame update
    void Start() {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        castleTransform = GameObject.FindGameObjectWithTag("Castle").transform;
        shopkeeperTransform = GameObject.FindGameObjectWithTag("Shopkeeper").transform;
        upgradekeeperTransform = GameObject.FindGameObjectWithTag("Upgradekeeper").transform;
        playerLocation = transform.GetChild(3).gameObject.GetComponent<RectTransform>();
        cameraLocation = transform.GetChild(4).gameObject.GetComponent<RectTransform>();
        castleLocation = transform.GetChild(0).gameObject.GetComponent<RectTransform>();
        shopkeeperLocation = transform.GetChild(1).gameObject.GetComponent<RectTransform>();
        upgradekeeperLocation = transform.GetChild(2).gameObject.GetComponent<RectTransform>();
        mapRect = gameObject.GetComponent<RectTransform>();
        camera = cameraTransform.gameObject.GetComponent<camera>();
        castleLocation.localPosition = new Vector3(-mapRect.rect.width / (-1000 / castleTransform.position.x), mapRect.rect.height / (1000 / castleTransform.position.z), 0);
        shopkeeperLocation.localPosition = new Vector3(-mapRect.rect.width / (-1000 / shopkeeperTransform.position.x), mapRect.rect.height / (1000 / shopkeeperTransform.position.z), 0);
        upgradekeeperLocation.localPosition = new Vector3(-mapRect.rect.width / (-1000 / upgradekeeperTransform.position.x), mapRect.rect.height / (1000 / upgradekeeperTransform.position.z), 0);
    }

    // Update is called once per frame
    void Update() {
        if (GameObject.FindGameObjectWithTag("Player")) {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            playerLocation.localPosition = new Vector3(-mapRect.rect.width / (-1000 / playerTransform.position.x), mapRect.rect.height / (1000 / playerTransform.position.z), 0);
            transform.GetChild(0).gameObject.SetActive(true);
        } else transform.GetChild(0).gameObject.SetActive(false);
        cameraLocation.localPosition = new Vector3(-mapRect.rect.width / (-1000 / cameraTransform.position.x), (mapRect.rect.height / (1000 / (cameraTransform.position.z - camera.posZ))) * (16 + (1.2f * (camera.posZ / camera.offset.z))) / 16, 0);
        cameraLocation.sizeDelta = new Vector2(5 + 50 * camera.posZ / camera.offset.z, (5 + 50 * camera.posZ / camera.offset.z) * 12 / 16);
    }

    public void MoveCam () {
        clickPosition = Input.mousePosition - mapRect.position;
        camera.isFixed = false;
        cameraTransform.position = new Vector3(clickPosition.x * 5, cameraTransform.position.y, clickPosition.y * 5 + camera.posZ);
    }
}
