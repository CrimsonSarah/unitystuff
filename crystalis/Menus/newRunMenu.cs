using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class newRunMenu : MonoBehaviour {
    public string SingleplayerScene, MultiplayerScene, MainMenuScene;
    [SerializeField]
    private Texture2D cursor;

    void Awake () {
        Cursor.SetCursor (cursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void Singleplayer () {
        SceneManager.LoadScene (SingleplayerScene);
    }

    public void Multiplayer () {
        SceneManager.LoadScene (MultiplayerScene);
    }

    public void MainMenu () {
        SceneManager.LoadScene (MainMenuScene);
    }
}