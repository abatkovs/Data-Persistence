using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SceneLoaderSO")]
public class SceneLoader : ScriptableObject
{
    public void LoadSceneX(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
