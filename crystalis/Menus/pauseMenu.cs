using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour {
    public string mainMenuScene, currentScene;
    public GameObject PauseMenu, CastleBox, UpgradeBox, ShopBox;
    public Tooltip tooltip;
    public gameOverMenu gameOverMenu;
    public bool isPaused;
    private bool optionsEnabled;

    void Update () {
        if (Input.GetKeyDown (KeyCode.Escape) && !gameOverMenu.isOver) {
            if (CastleBox.activeSelf){
                tooltip.HideTooltip();
                CastleBox.SetActive(false);
            }
            else if (UpgradeBox.activeSelf){
                tooltip.HideTooltip();
                UpgradeBox.SetActive(false);
            }
            else if (ShopBox.activeSelf){
                tooltip.HideTooltip();
                ShopBox.SetActive(false);
            }
            else TogglePause ();
        }
    }

    public void TogglePause () {
        isPaused = !isPaused;
        PauseMenu.SetActive (isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void MainMenu () {
        SceneManager.LoadScene (mainMenuScene);
    }
}