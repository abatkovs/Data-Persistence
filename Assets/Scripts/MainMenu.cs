using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerName;

    public void StartGame()
    {
        GameManager.Instance.SetPlayerName(_playerName.text);
        SceneManager.LoadScene(1);
    }
    
    public void ExitGame(){
        GameManager.Instance.SaveData(); 
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
