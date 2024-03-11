using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverMenu : MonoBehaviour {
    public string sampleScene, mainMenuScene, characterSelect;
    public GameObject GameOverMenu;
    public bool isOver;

    public void NewRun () {
        Destroy(GameObject.Find("Director"));
        SceneManager.LoadScene (characterSelect);
    }

    public void MainMenu () {
        SceneManager.LoadScene (mainMenuScene);
    }
}