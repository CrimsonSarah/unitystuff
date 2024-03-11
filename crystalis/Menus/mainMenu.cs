using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour {
    public string StartScene;
    [SerializeField]
    private Texture2D cursor;

    void Awake () {
        Cursor.SetCursor (cursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void StartGame () {
        SceneManager.LoadScene (StartScene);
    }

    public void QuitGame () {
        Application.Quit ();
    }
}