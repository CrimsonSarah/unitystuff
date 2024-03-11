using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSkillsRange : MonoBehaviour {
    public bool[] showingRange = new bool[6];
    public player character;
    public Items Items;
    public RectTransform[] skillRanges;
    public RectTransform[] skillRadiuses;

    // Update is called once per frame
    void Update () {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            for (int i = 0; i < skillRanges.Length - 1; i++) {
                if (character.skillEnabled[i] && i != 4) {
                    skillRanges[i].sizeDelta = new Vector2 ((character.skillRange[i] + Items.Effect[25 + i]) * 69f / 100f, (character.skillRange[i] + Items.Effect[25 + i]) * 69f / 100f);
                } else if (showingRange[i] == false) skillRanges[i].sizeDelta = Vector2.zero;

                if (character.skillEnabled[i] && showingRange[i] == false && i != 4) {
                    skillRadiuses[i].sizeDelta = new Vector2 ((character.skillRadius[i] + Items.Effect[30 + i]) * 69f / 100f, (character.skillRadius[i] + Items.Effect[30 + i]) * 69f / 100f);
                    Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                    RaycastHit hit;
                    Physics.Raycast (ray, out hit);
                    skillRadiuses[i].transform.position = hit.point;
                    skillRadiuses[i].transform.position = new Vector3 (skillRadiuses[i].transform.position.x, 1, skillRadiuses[i].transform.position.z);
                } else skillRadiuses[i].sizeDelta = Vector2.zero;
            }
        }
    }

    public void ShowQRange () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            skillRanges[0].sizeDelta = new Vector2 ((character.skillRange[0] + Items.Effect[25]) * 69f / 100f, (character.skillRange[0] + Items.Effect[25]) * 69f / 100f);
            if (skillRanges[0].sizeDelta == Vector2.zero) {
                skillRanges[0].sizeDelta = new Vector2 ((character.skillRadius[0] + Items.Effect[30]) * 69f / 100f, (character.skillRadius[0] + Items.Effect[30]) * 69f / 100f);
            }
            showingRange[0] = true;
        }
    }

    public void ShowWRange () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            skillRanges[1].sizeDelta = new Vector2 ((character.skillRange[1] + Items.Effect[26]) * 69f / 100f, (character.skillRange[1] + Items.Effect[26]) * 69f / 100f);
            if (skillRanges[1].sizeDelta == Vector2.zero) {
                skillRanges[1].sizeDelta = new Vector2 ((character.skillRadius[1] + Items.Effect[31]) * 69f / 100f, (character.skillRadius[1] + Items.Effect[31]) * 69f / 100f);
            }
            showingRange[1] = true;
        }
    }

    public void ShowERange () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            skillRanges[2].sizeDelta = new Vector2 ((character.skillRange[2] + Items.Effect[27]) * 69f / 100f, (character.skillRange[2] + Items.Effect[27]) * 69f / 100f);
            if (skillRanges[2].sizeDelta == Vector2.zero) {
                skillRanges[2].sizeDelta = new Vector2 ((character.skillRadius[2] + Items.Effect[32]) * 69f / 100f, (character.skillRadius[2] + Items.Effect[32]) * 69f / 100f);
            }
            showingRange[2] = true;
        }
    }

    public void ShowRRange () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            skillRanges[3].sizeDelta = new Vector2 ((character.skillRange[3] + Items.Effect[28]) * 69f / 100f, (character.skillRange[3] + Items.Effect[28]) * 69f / 100f);
            if (skillRanges[3].sizeDelta == Vector2.zero) {
                skillRanges[3].sizeDelta = new Vector2 ((character.skillRadius[3] + Items.Effect[33]) * 69f / 100f, (character.skillRadius[3] + Items.Effect[33]) * 69f / 100f);
            }
            showingRange[3] = true;
        }
    }

    public void ShowPRange () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            skillRanges[4].sizeDelta = new Vector2 ((character.skillRange[4] + Items.Effect[29]) * 69f / 100f, (character.skillRange[4] + Items.Effect[29]) * 69f / 100f);
            if (skillRanges[4].sizeDelta == Vector2.zero) {
                skillRanges[4].sizeDelta = new Vector2 ((character.skillRadius[4] + Items.Effect[34]) * 69f / 100f, (character.skillRadius[4] + Items.Effect[34]) * 69f / 100f);
            }
            showingRange[4] = true;
        }
    }

    public void HideQRange () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            skillRanges[0].sizeDelta = Vector2.zero;
            showingRange[0] = false;
        }
    }

    public void HideWRange () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            skillRanges[1].sizeDelta = Vector2.zero;
            showingRange[1] = false;
        }
    }

    public void HideERange () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            skillRanges[2].sizeDelta = Vector2.zero;
            showingRange[2] = false;
        }
    }

    public void HideRRange () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            skillRanges[3].sizeDelta = Vector2.zero;
            showingRange[3] = false;
        }
    }

    public void HidePRange () {
        if (GameObject.FindGameObjectWithTag("Player")) {
            skillRanges[4].sizeDelta = Vector2.zero;
            showingRange[4] = false; 
        }
    }
}