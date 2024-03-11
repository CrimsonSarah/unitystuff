using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characters : MonoBehaviour
{
    public class Character {
        public string Name { get; set; }
        public string Description { get; set; }
        public float[] CharHealth { get; set; }
        public float[] CharMana { get; set; }
        public float[] CharAttributes { get; set; }
        public float[] CharExp { get; set; }
        public float[] SkillCd { get; set; }
        public float[] SkillMaxCd { get; set; }
        public float[,] SkillPwr { get; set; }
        public float[] SkillCost { get; set; }
        public float[] SkillRange { get; set; }
        public float[] SkillRadius { get; set; }
        public float CharArmor { get; set; }
        public float CharBasicDamage { get; set; }
        public float CharAttackRange { get; set; }
        public float CharAttackSpeed { get; set; }
        public float CharCritChance { get; set; }
        public float CharDodgeChance { get; set; }
        public float CharLifesteal { get; set; }
        public bool[] SkillEnabled { get; set; }
        public bool IsOn { get; set; }
        public int[] NSkill { get; set; }
        public int[,] SkillType { get; set; }
        public int[] CharAttributePerLvl { get; set; }
        public GameObject[] Projectiles { get; set; }
        public GameObject CharFirePoint { get; set; }
        public Character( 
            string name,
            string description,
            float[] charHealth,
            float[] charMana,
            float[] charAttributes,
            float[] charExp,
            float[] skillCd,
            float[] skillMaxCd,
            float[,] skillPwr,
            float[] skillCost,
            float[] skillRange,
            float[] skillRadius,
            float charArmor,
            float charBasicDamage,
            float charAttackRange,
            float charAttackSpeed,
            float charCritChance,
            float charDodgeChance,
            float charLifesteal,
            bool[] skillEnabled,
            bool isOn,
            int[] nSkill,
            int[,] skillType, //[0]Buff/Debuff, [1]Passiva, [2]Ativa, [3]Toggle, [4]Clicável
            int[] charAttributePerLvl){

            Name = name;
            Description = description;
            CharHealth = charHealth;
            CharMana = charMana;
            CharAttributes = charAttributes;
            CharExp = charExp;
            SkillCd = skillCd;
            SkillMaxCd = skillMaxCd;
            SkillPwr = skillPwr;
            SkillCost = skillCost;
            SkillRange = skillRange;
            SkillRadius = skillRadius;
            CharArmor = charArmor;
            CharBasicDamage = charBasicDamage;
            CharAttackRange = charAttackRange;
            CharAttackSpeed = charAttackSpeed;
            CharCritChance = charCritChance;
            CharDodgeChance = charDodgeChance;
            CharLifesteal = charLifesteal;
            SkillEnabled = skillEnabled;
            IsOn = isOn;
            NSkill = nSkill;
            SkillType = skillType;
            CharAttributePerLvl = charAttributePerLvl;
        }
    }

    public List<Character> charList = new List<Character>();
    public int selectedCastle;

    void Awake() {
        charList.Add(new Character("default", "default", new float[3] { 115f, 0f, 1f }, new float[3] { 65f, 0f, 0.5f }, new float[3] { 0f, 0f, 0f }, new float[3] { 0f, 0f, 0f }, new float[5] { 0f, 0f, 0f, 0f, 0f }, new float[5] { 9f, 7f, 14f, 76f, 4f }, new float[2, 8] { { 15f, 15f, 30f, 70f, 0f, 0f, 0f, 0f }, { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f } }, new float[4] { 13f, 37f, 18f, 90f }, new float[5] { 40f, 70f, 0f, 1f, 1f }, new float[5] { 25f, 0f, 15f, 50f, 0f }, 0f, 0f, 12f, 0.9f, 0f, 0f, 0f, new bool[5] { false, false, false, false, false }, false, new int[5] { 1, 2, 3, 4, 5 }, new int[2, 5] { { 4, 4, 2, 4, 1 }, { 0, 0, 0, 0, 0 } }, new int[3] { 3, 3, 3 }));

        charList.Add(new Character("Hog", "He isn't a hog, he is THE hog.", new float[3] { 90f, 0f, 2.5f }, new float[3] { 65f, 0f, 0.5f }, new float[3] { 0f, 0f, 0f }, new float[3] { 0f, 0f, 0f }, new float[5] { 0f, 0f, 0f, 0f, 0f }, new float[5] { 12f, 0f, 18f, 112f, 0f }, new float[2, 8] { { 45f, 5f, 1f, 4f, 10f, 2f, 135f, 6f }, { 10f, 0f, 0f, 4f, 20f, 4f, 25f, 4f } }, new float[4] { 39f, 0f, 27f, 98f }, new float[5] { 90f, 1f, 60f, 0f, 1f }, new float[5] { 20f, 0f, 25f, 45f, 10f }, 0f, 0f, 12f, 0.6f, 0f, 0f, 0f, new bool[5] { false, false, false, false, false }, false, new int[5] { 6, 7, 8, 9, 10 }, new int[2, 5] { { 4, 0, 3, 3, 0 }, { 4, 0, 3, 3, 0 } }, new int[3] { 5, 3, 1 }));

        charList.Add(new Character("Sirena", "placeholder", new float[3] { 150f, 0f, 1f }, new float[3] { 45f, 0f, 0.9f }, new float[3] { 0f, 0f, 0f }, new float[3] { 0f, 0f, 0f }, new float[5] { 0f, 0f, 0f, 0f, 0f }, new float[5] { 11f, 6f, 3.5f, 14.5f, 0f }, new float[2, 8] { { 3f, 10f, 3f, 12.5f, 20f, 12.5f, 20f, 0f }, { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f } }, new float[4] { 19f, 24f, 41f, 59f }, new float[5] { 55f, 1f, 1f, 1f, 1f }, new float[5] { 8.5f, 26f, 26f, 0f, 10f }, 0f, 0f, 52f, 0.9f, 0f, 0f, 0f, new bool[5] { false, false, false, false, false }, false, new int[5] { 11, 12, 13, 14, 15 }, new int[2, 5] { { 4, 2, 2, 2, 0 }, { 0, 0, 0, 0, 0 } }, new int[3] { 2, 1, 6 }));
    }
}
