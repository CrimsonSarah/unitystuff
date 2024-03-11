using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public int difficulty;

    // Start is called before the first frame update
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "SampleScene" && scene.name != "NewRun" && scene.name != "CharacterSelect" && scene.name != "DontDestroyOnLoad") Destroy(gameObject);
        else if (scene.name == "SampleScene") {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<GameManager>().enabled = true;
            GetComponent<wavespawner>().enabled = true;
            GetComponent<Tooltip>().enabled = true;
            GetComponent<EventManager>().enabled = true;
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
