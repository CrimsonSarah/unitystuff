using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {
    public GameObject tooltipGO;
    public Text[] text = new Text[3];

    void Update () {
        if (tooltipGO) {
            if (tooltipGO.activeSelf) {
                Vector3 tooltipposition = Input.mousePosition;
                tooltipGO.transform.position = new Vector3(Mathf.Clamp(tooltipposition.x, tooltipGO.GetComponent<RectTransform>().rect.width / 2f, Screen.width - tooltipGO.GetComponent<RectTransform>().rect.width / 2f), (tooltipposition.y + (tooltipGO.GetComponent<RectTransform>().rect.height / 2f) + 15f), 0f);
            }
        }
    }

    // Update is called once per frame
    public void ShowSkillTooltip (int i) {
        if (GameObject.FindGameObjectWithTag("Player")) {
            skills skills;
            Tooltip tooltip;
            player player;
            Items items;

            tooltip = GameObject.Find ("Director").GetComponent<Tooltip> ();
            skills = GameObject.FindGameObjectWithTag ("Player").GetComponent<skills> ();
            player = GameObject.FindGameObjectWithTag ("Player").GetComponent<player> ();
            items = GameObject.Find ("Items").GetComponent<Items> ();

            tooltip.tooltipGO.SetActive (true);
            tooltip.text[1].gameObject.SetActive(true);
            tooltip.text[2].gameObject.SetActive(true);

            tooltip.text[0].text = skills.tooltiptext[0, player.selectedChar, i] + "\n" + "\n" + skills.tooltiptext[1, player.selectedChar, player.charInstance * 5 + i];
            if (i == 4) tooltip.text[1].text = "0";
            else tooltip.text[1].text = (player.skillManaCost[i] - items.Effect[21 + i]).ToString("N0");
            tooltip.text[2].text = (player.skillMaxCooldown[i] - (player.skillMaxCooldown[i] * items.Effect[11 + i])).ToString("N0");
        }
    }

    public void ShowShopTooltip (Button button) {
        shopkeeper shopkeeper;
        Tooltip tooltip;
        Items items;
        Item item;

        items = GameObject.Find ("Items").GetComponent<Items> ();
        shopkeeper = GameObject.FindGameObjectWithTag("Shop Core").GetComponent<shopkeeper> ();
        tooltip = GameObject.Find ("Director").GetComponent<Tooltip> ();
        item = items.itemList[shopkeeper.shopSlot[(int) button.name[4] - 49]];

        tooltip.tooltipGO.SetActive (true);
        tooltip.text[1].gameObject.SetActive(false);
        tooltip.text[2].gameObject.SetActive(false);

        tooltip.text[0].text = item.Name + "\n" + "\n" + item.Description;
    }

    public void ShowCastleTooltip (int i) {
        castleUpgrades castle;
        Tooltip tooltip;
        characters characters;

        tooltip = GameObject.Find("Director").GetComponent<Tooltip>();
        characters = GameObject.Find("Director").GetComponent<characters>();
        castle = GameObject.Find("CastleUpgradesBox").GetComponent<castleUpgrades>();

        tooltip.tooltipGO.SetActive(true);
        tooltip.text[1].gameObject.SetActive(false);
        tooltip.text[2].gameObject.SetActive(false);

        tooltip.text[0].text = castle.tooltipText[characters.selectedCastle,0,i] + "\n" + "\n" + castle.tooltipText[characters.selectedCastle,1,i];
    }

    public void ShowAttributesTooltip () {
        Items items;
        player player;
        Tooltip tooltip;

        items = GameObject.Find("Items").GetComponent<Items>();
        tooltip = GameObject.Find("Director").GetComponent<Tooltip>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();

        tooltip.tooltipGO.SetActive(true);
        tooltip.text[1].gameObject.SetActive(false);
        tooltip.text[2].gameObject.SetActive(false);

        tooltip.text[0].text = "Strength" + "\n" + "Max health: " + (player.characterAttributes[0] * 10f).ToString("N0") + " + " + (items.Effect[0]).ToString("N0") + " = " + ((player.characterAttributes[0] * 10f) + items.Effect[0]).ToString("N0") + "\n" + "Health regen: " + (player.characterAttributes[0] * 0.1f).ToString("N2") + " + " + (items.Effect[1] / 2.5f).ToString("N2") + " = " + ((player.characterAttributes[0] * 0.1f) + (items.Effect[1] / 2.5f)).ToString("N2") + "\n" + "\n";  

        tooltip.text[0].text += "Dexterity" + "\n" + "Armor: " + (player.characterAttributes[1] * 0.2f).ToString("N1") + " + " + (items.Effect[4] / 5f).ToString("N1") + " = " + ((player.characterAttributes[1] * 0.2f) + (items.Effect[4] / 5f)).ToString("N1") + "\n" + "Attack speed: " + (player.characterAttributes[1] * 0.01f).ToString("N2") + " + " + ((player.characterAttributes[1] * 0.01f) * items.Effect[7] / 100f).ToString("N2") + " = " + ((player.characterAttributes[1] * 0.01f) + ((player.characterAttributes[1] * 0.01f) * items.Effect[7] / 100f)).ToString("N2") + "\n" + "\n";

        tooltip.text[0].text += "Intelligence" + "\n" + "Max mana: " + (player.characterAttributes[2] * 6f).ToString("N0") + " + " + (items.Effect[2]).ToString("N0") + " = " + ((player.characterAttributes[2] * 6f) + items.Effect[2]).ToString("N0") + "\n" + "Mana regen: " + (player.characterAttributes[0] * 0.05f).ToString("N2") + " + " + (items.Effect[3] / 2.5f).ToString("N2") + " = " + ((player.characterAttributes[0] * 0.05f) + (items.Effect[3] / 2.5f)).ToString("N2") + "\n" + "\n";

        tooltip.text[0].text += "Damage: " + ((player.characterAttributes[0] + player.characterAttributes[1]) * 1.25f).ToString("N0") + " + " + items.Effect[5].ToString("N0") + " = " + (((player.characterAttributes[0] + player.characterAttributes[1]) * 1.25f) + items.Effect[5]).ToString("N0");
    }

    public void ShowItemTooltip (itemData item) {
        Tooltip tooltip;

        tooltip = GameObject.Find("Director").GetComponent<Tooltip>();

        tooltip.tooltipGO.SetActive(true);
        tooltip.text[1].gameObject.SetActive(false);
        tooltip.text[2].gameObject.SetActive(false);

        tooltip.text[0].text = item.itemName + "\n" + "\n" + item.description;
    }

    public void HideTooltip () {
        Tooltip tooltip;

        tooltip = GameObject.Find ("Director").GetComponent<Tooltip> ();

        tooltip.tooltipGO.transform.localPosition = new Vector3 (0f, 0f, 0f);
        tooltip.text[0].text = ".";
        GameObject.Find ("Director").GetComponent<Tooltip> ().tooltipGO.SetActive (false);
    }
}