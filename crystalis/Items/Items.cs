using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {
    public float[] Effect = new float[37]; //[0]MaxHealth, [1]HealthRegen, [2]MaxMana, [3]ManaRegen, [4]Armor, [5]BasicDamage, [6]AttackRange, [7]AttackSpeed, [8]Strength, [9]Dexterity, [10]Intelligence, [11]QMaxCooldown, [12]WMaxCooldown, [13]EMaxCooldown, [14]RMaxCooldown, [15]PMaxCooldown, [16]QPower, [17]WPower, [18]EPower, [19]RPower, [20]PPower, [21]QManaCost, [22]WManaCost, [23]EManaCost, [24]RManaCost, [25]QRange, [26]WRange, [27]ERange, [28]RRange, [29]PRange, [30]QRadius, [31]WRadius, [32]ERadius, [33]RRadius, [34]PRadius, [35]CritChance, [36]DodgeChance
    public player Player;
    public List<Item> itemList = new List<Item> ();
    private float atkspdbonus = 0f;

    void Awake () {
        //Itens comuns
        itemList.Add (new Item ("Life Ring", "Vida máxima: +65" + "\n" + "Regeneração de vida: +0.75", "C", 675, 0, new float[37], false));
        itemList[0].Effect[0] = 65f;
        itemList[0].Effect[1] = 0.75f;
        itemList.Add (new Item ("Mana Ring", "Mana máxima: +40" + "\n" + "Regeneração de mana: +0.5", "C", 675, 0, new float[37], false));
        itemList[1].Effect[2] = 40f;
        itemList[1].Effect[3] = 0.5f;
        itemList.Add (new Item ("Generic Sword", "Dano: +3", "C", 100, 0, new float[37], false));
        itemList[2].Effect[5] = 3f;
        itemList.Add (new Item ("Steroids", "Força: +3", "C", 885, 0, new float[37], false));
        itemList[3].Effect[8] = 3f;
        itemList.Add (new Item ("Classy Boots", "Destreza: +3", "C", 885, 0, new float[37], false));
        itemList[4].Effect[9] = 3f;
        itemList.Add (new Item ("Dictionary", "Inteligência: +3", "C", 885, 0, new float[37], false));
        itemList[5].Effect[10] = 3f;

        //Itens incomuns 
        itemList.Add (new Item ("Spiked Gauntlet", "Armadura: +5" + "\n" + "Dano: +35", "B", 1125, 0, new float[37], false));
        itemList[6].Effect[4] = 5f;
        itemList[6].Effect[5] = 35f;
        itemList.Add (new Item ("Turtoise Armor", "Armadura: +13", "B", 2650, 0, new float[37], false));
        itemList[7].Effect[4] = 13f;
        itemList.Add (new Item ("Haste Gauntlet", "Velocidade de ataque: +15%", "B", 625, 0, new float[37], false));
        itemList[8].Effect[7] = 15f;

        //Itens raros
        itemList.Add (new Item ("Magic Pendulum", "Diminui o cooldown de todas skills em 10%.", "A", 15150, 0, new float[37], false));
        itemList[9].Effect[11] = 0.1f;
        itemList[9].Effect[12] = 0.1f;
        itemList[9].Effect[13] = 0.1f;
        itemList[9].Effect[14] = 0.1f;
        itemList[9].Effect[15] = 0.1f;
        itemList.Add (new Item ("Pendulum", "Diminui o cooldown da ult em 20%.", "A", 7750, 0, new float[37], false));
        itemList[10].Effect[14] = 0.2f;
        itemList.Add (new Item ("Simple Bow", "Alcance de ataque: +1", "A", 70, 0, new float[37], false));
        itemList[11].Effect[6] = 1f;

        //Itens especiais
        itemList.Add (new Item ("Macaco", "Como assim \"Macaco\"???", "S", 50000, 0, new float[37], false));
        itemList[12].Effect[0] = 1000f;
        itemList[12].Effect[1] = 30f;
        itemList[12].Effect[2] = 1000f;
        itemList[12].Effect[3] = 30f;
        itemList[12].Effect[4] = 100f;
        itemList[12].Effect[5] = 1000f;
        itemList[12].Effect[6] = 1000f;
        itemList[12].Effect[7] = 1000f;
        itemList[12].Effect[8] = 10f;
        itemList[12].Effect[9] = 10f;
        itemList[12].Effect[10] = 10f;
        itemList[12].Effect[11] = 10f;
        itemList[12].Effect[12] = 10f;
        itemList[12].Effect[13] = 10f;
        itemList[12].Effect[14] = 10f;
        itemList[12].Effect[15] = 10f;
        itemList[12].Effect[16] = 30f;
        itemList[12].Effect[17] = 30f;
        itemList[12].Effect[18] = 30f;
        itemList[12].Effect[19] = 30f;
        itemList[12].Effect[20] = 30f;
        itemList[12].Effect[21] = 100f;
        itemList[12].Effect[22] = 100f;
        itemList[12].Effect[23] = 100f;
        itemList[12].Effect[24] = 100f;
        itemList[12].Effect[25] = 50f;
        itemList[12].Effect[26] = 50f;
        itemList[12].Effect[27] = 50f;
        itemList[12].Effect[28] = 50f;
        itemList[12].Effect[29] = 50f;
        itemList[12].Effect[30] = 30f;
        itemList[12].Effect[31] = 30f;
        itemList[12].Effect[32] = 30f;
        itemList[12].Effect[33] = 30f;
        itemList[12].Effect[34] = 30f;

        Debug.Log(itemList.Count);
    }

    public void ItemUpdate () {
        Player.armor -= Effect[4];
        Player.basicDamage -= Effect[5];
        if (Player.attackSpeed >= 3.5f) Player.attackSpeed = 3.5f;
        else Player.attackSpeed -= atkspdbonus;
        Player.characterHealth[0] -= Effect[0];
        Player.characterHealth[2] -= Effect[1];
        Player.characterMana[0] -= Effect[2];
        Player.characterMana[2] -= Effect[3];
        Player.characterAttributes[0] -= Effect[8];
        Player.characterAttributes[1] -= Effect[9];
        Player.characterAttributes[2] -= Effect[10];
        Player.critChance -= Effect[35];
        Player.dodgeChance -= Effect[36];
        for (int c = 0; c < Effect.Length; c++) {
            Effect[c] = 0f;
        }
        for (int i = 0; i < itemList.Count; i++) {
            if (itemList[i].IsOn) {
                for (int c = 0; c < Effect.Length; c++) {
                    Effect[c] += itemList[i].Effect[c] * itemList[i].Quantity;
                }
            }
        }
        atkspdbonus = Player.attackSpeed * Effect[7] / 100f;
        Player.characterHealth[0] -= -Effect[0];
        Player.characterHealth[2] -= -Effect[1];
        Player.characterMana[0] -= -Effect[2];
        Player.characterMana[2] -= -Effect[3];
        Player.characterAttributes[0] -= -Effect[8];
        Player.characterAttributes[1] -= -Effect[9];
        Player.characterAttributes[2] -= -Effect[10];
        Player.armor -= -Effect[4];
        Player.basicDamage -= -Effect[5];
        Player.attackSpeed -= -atkspdbonus;
        if (Player.attackSpeed >= 3.5f) Player.attackSpeed = 3.5f;
        Player.critChance -= -Effect[35];
        Player.dodgeChance -= -Effect[36];
    }
}

public class Item {
    public string Name { get; set; }
    public string Description { get; set; }
    public string Rank { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    public float[] Effect { get; set; }
    public bool IsOn { get; set; }
    public Item (string name, string description, string rank, int price, int quantity, float[] effect, bool isOn) {
        Name = name;
        Description = description;
        Rank = rank;
        Price = price;
        Quantity = quantity;
        Effect = effect;
        IsOn = isOn;
    }
}