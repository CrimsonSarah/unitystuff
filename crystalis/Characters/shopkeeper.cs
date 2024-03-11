using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopkeeper : MonoBehaviour {
    public GameObject ItemBG, ItemBGInstance, ItemContainer;
    public bool enableBuy;
    public player Player;
    public Items Items;
    public Sprite[] sprites = new Sprite[7];
    public Text[] itemPrices;
    public Button[] buttons;
    public int[] shopSlot;
    public int[] positionData;
    public int[] rankIndex;

    void Start () {
        shopSlot = new int[3];
        ItemContainer = GameObject.Find ("ItemContainer");
        positionData = new int[Items.itemList.Count];
        rankIndex = new int[5] { 0, -1, -1, -1, Items.itemList.Count };

        buttons[0].onClick.AddListener (delegate { buyItem (buttons[0]); });
        buttons[1].onClick.AddListener (delegate { buyItem (buttons[1]); });
        buttons[2].onClick.AddListener (delegate { buyItem (buttons[2]); });

        enableBuy = false;
        for (int i = 0; i < Items.itemList.Count-1; i++) {
            if (Items.itemList[i].Rank != Items.itemList[i + 1].Rank) {
                rankIndex[System.Array.IndexOf (rankIndex, -1)] = i;
            }
        }

        for (int i = 0; i < 3; i++) {
            shopSlot[i] = CreateRandom (i);
        }
    }

    void Update () {
        buttons[0].GetComponent<Button> ().interactable = Player.gold >= Items.itemList[shopSlot[0]].Price && enableBuy;
        buttons[1].GetComponent<Button> ().interactable = Player.gold >= Items.itemList[shopSlot[1]].Price && enableBuy;
        buttons[2].GetComponent<Button> ().interactable = Player.gold >= Items.itemList[shopSlot[2]].Price && enableBuy;
    }

    public int CreateRandom (int slot) {
        int itemrarity = Random.Range (1, 100);
        int randomint;
        if (itemrarity <= 60) randomint = Random.Range (rankIndex[0], rankIndex[1]);
        else if (itemrarity <= 95) randomint = Random.Range (rankIndex[1], rankIndex[2]);
        else randomint = Random.Range (rankIndex[2], rankIndex[3]);
        itemPrices[slot].text = Items.itemList[randomint].Price.ToString ();
        return randomint;
    }

    public void buyItem (Button button) {
        int slot = (int) button.name[4] - 49;
        if (GameObject.FindGameObjectWithTag ("Player")) {
            if (Player.gold >= Items.itemList[shopSlot[slot]].Price && enableBuy) {
                button.GetComponent<Image> ().raycastTarget = false;
                StartCoroutine (updateTooltip ());
                IEnumerator updateTooltip () {
                    yield return 0;
                    button.GetComponent<Image> ().raycastTarget = true;
                }
                Player.gold -= Items.itemList[shopSlot[slot]].Price;
                Items.itemList[shopSlot[slot]].Quantity++;
                if (Items.itemList[shopSlot[slot]].IsOn) {
                    for (int i = 0; i < 35; i++) {
                        if (Items.itemList[shopSlot[slot]].Effect[i] != 0f) {
                            ItemContainer.transform.GetChild (System.Array.IndexOf (positionData, shopSlot[slot])).transform.GetChild (1).gameObject.GetComponent<Text> ().text = Items.itemList[shopSlot[slot]].Quantity.ToString ("N0");
                        }
                    }
                } else {
                    positionData[ItemContainer.transform.childCount] = shopSlot[slot];
                    Items.itemList[shopSlot[slot]].IsOn = true;
                    ItemBGInstance = Instantiate (ItemBG, ItemContainer.transform, false);
                    ItemBGInstance.name = Items.itemList[shopSlot[slot]].Name;
                    ItemBGInstance.GetComponent<itemData> ().itemName = Items.itemList[shopSlot[slot]].Name;
                    ItemBGInstance.GetComponent<itemData> ().description = Items.itemList[shopSlot[slot]].Description;
                    ItemBGInstance.transform.GetChild (0).gameObject.GetComponent<Image> ().sprite = button.gameObject.GetComponent<Image> ().sprite;
                    ItemBGInstance.transform.GetChild (0).gameObject.GetComponent<Image> ().color = button.gameObject.GetComponent<Image> ().color;
                    ItemBGInstance.transform.GetChild (1).gameObject.GetComponent<Text> ().text = "1";
                }
                Items.ItemUpdate ();
                shopSlot[slot] = CreateRandom (slot);
                button.gameObject.GetComponent<ItemImage>().ImageUpdate();
            }
        }
    }

    private void OnTriggerEnter (Collider other) {
        if (other.tag == "Player") {
            enableBuy = true;
        }
    }

    private void OnTriggerExit (Collider other) {
        if (other.tag == "Player") {
            enableBuy = false;
        }
    }
}