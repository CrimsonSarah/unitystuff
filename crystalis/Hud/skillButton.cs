using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillButton : MonoBehaviour {
    public ShowSkillsRange rangeScript;
    public player player;
    public Sprite[] defaultSprite = new Sprite [5];
    public Sprite[] hogSprite = new Sprite [5];
    public Sprite[] sirenaSprite = new Sprite [5];
    public Image[] skillImage = new Image[5];

    void Start () {
        skillImage[0] = GameObject.Find("SkillQIcon").GetComponent<Image>();
        skillImage[1] = GameObject.Find("SkillWIcon").GetComponent<Image>();
        skillImage[2] = GameObject.Find("SkillEIcon").GetComponent<Image>();
        skillImage[3] = GameObject.Find("UltIcon").GetComponent<Image>();
        skillImage[4] = GameObject.Find("SkillPIcon").GetComponent<Image>();

        switch (player.selectedChar)
        {
            case 1:
                for (int i = 0; i < skillImage.Length; i++) {
                    skillImage[i].sprite = hogSprite[i];
                }
                break;
            case 2:
                for (int i = 0; i < skillImage.Length; i++) {
                    skillImage[i].sprite = sirenaSprite[i];
                }
                break;
            default:
                for (int i = 0; i < skillImage.Length; i++) {
                    skillImage[i].sprite = defaultSprite[i];
                }
                break;
        }
    }

    public void QClick () {
        if (GameObject.FindGameObjectWithTag("Player")) player.skills.UseSkill(player.characters.charList[player.selectedChar].NSkill[0]);
    }

    public void WClick () {
        if (GameObject.FindGameObjectWithTag("Player")) player.skills.UseSkill(player.characters.charList[player.selectedChar].NSkill[1]);
    }

    public void EClick () {
        if (GameObject.FindGameObjectWithTag("Player")) player.skills.UseSkill(player.characters.charList[player.selectedChar].NSkill[2]);
    }

    public void RClick () {
        if (GameObject.FindGameObjectWithTag("Player")) player.skills.UseSkill(player.characters.charList[player.selectedChar].NSkill[3]);
    }

    public void ShowQRange () {
        if (GameObject.FindGameObjectWithTag("Player")) rangeScript.ShowQRange ();
    }

    public void ShowWRange () {
        if (GameObject.FindGameObjectWithTag("Player")) rangeScript.ShowWRange ();
    }

    public void ShowERange () {
        if (GameObject.FindGameObjectWithTag("Player")) rangeScript.ShowERange ();
    }

    public void ShowRRange () {
        if (GameObject.FindGameObjectWithTag("Player")) rangeScript.ShowRRange ();
    }

    public void ShowPRange () {
        if (GameObject.FindGameObjectWithTag("Player")) rangeScript.ShowPRange ();
    }

    public void HideQRange () {
        if (GameObject.FindGameObjectWithTag("Player")) rangeScript.HideQRange ();
    }

    public void HideWRange () {
        if (GameObject.FindGameObjectWithTag("Player")) rangeScript.HideWRange ();
    }

    public void HideERange () {
        if (GameObject.FindGameObjectWithTag("Player")) rangeScript.HideERange ();
    }

    public void HideRRange () {
        if (GameObject.FindGameObjectWithTag("Player")) rangeScript.HideRRange ();
    }

    public void HidePRange () {
        if (GameObject.FindGameObjectWithTag("Player")) rangeScript.HidePRange ();
    }
}