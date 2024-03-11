using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemImage : MonoBehaviour
{
    private Item item;
    private Items items;
    private Image image;
    private shopkeeper shopkeeper;
    private string itemName;
    
    void Start() {
        shopkeeper = GameObject.FindWithTag("Shop Core").GetComponent<shopkeeper>();
        image = gameObject.GetComponent<Image>();
        items = GameObject.Find ("Items").GetComponent<Items> ();
        item = items.itemList[shopkeeper.shopSlot[(int) gameObject.name[4] - 49]];
        itemName = item.Name;
        switch (itemName) {
            case "Life Ring":
                image.sprite = shopkeeper.sprites[1];
                break;
            case "Mana Ring":
                image.sprite = shopkeeper.sprites[2];
                break;
            case "Generic Sword":
                image.sprite = shopkeeper.sprites[3];
                break;
            case "Steroids":
                image.sprite = shopkeeper.sprites[4];
                break;
            case "Classy Boots":
                image.sprite = shopkeeper.sprites[5];
                break;
            case "Dictionary":
                image.sprite = shopkeeper.sprites[6];
                break;
            case "Turtoise Armor":
                image.sprite = shopkeeper.sprites[8];
                break;
            default:
                image.sprite = shopkeeper.sprites[0];
                break;
        }
    }

    public void ImageUpdate () {
        item = items.itemList[shopkeeper.shopSlot[(int) gameObject.name[4] - 49]];
        itemName = item.Name;
        switch (itemName) {
            case "Life Ring":
                image.sprite = shopkeeper.sprites[1];
                break;
            case "Mana Ring":
                image.sprite = shopkeeper.sprites[2];
                break;
            case "Generic Sword":
                image.sprite = shopkeeper.sprites[3];
                break;
            case "Steroids":
                image.sprite = shopkeeper.sprites[4];
                break;
            case "Classy Boots":
                image.sprite = shopkeeper.sprites[5];
                break;
            case "Dictionary":
                image.sprite = shopkeeper.sprites[6];
                break;
            case "Turtoise Armor":
                image.sprite = shopkeeper.sprites[8];
                break;
            default:
                image.sprite = shopkeeper.sprites[0];
                break;
        }
    }
}
