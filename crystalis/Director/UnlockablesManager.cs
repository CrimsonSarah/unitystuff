using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockablesManager : MonoBehaviour
{
    public UnlockableMatrix unlockableMatrix;
    private string unlockableMatrixPath;

    void Awake() {
        unlockableMatrixPath = $"{Application.persistentDataPath}/UnlockableMatrix.json";
        if (File.Exists(unlockableMatrixPath)) {
            string json = File.ReadAllText(unlockableMatrixPath);
            unlockableMatrix = JsonUtility.FromJson<UnlockableMatrix>(json);
        }
    }
    
    public void LoadJson () {
        if (File.Exists(unlockableMatrixPath)) {
            string json = File.ReadAllText(unlockableMatrixPath);
            unlockableMatrix = JsonUtility.FromJson<UnlockableMatrix>(json);
        }
    }

    public void SaveJson () {
        string json = JsonUtility.ToJson(unlockableMatrix);
        File.WriteAllText(unlockableMatrixPath, json);
    }
}
