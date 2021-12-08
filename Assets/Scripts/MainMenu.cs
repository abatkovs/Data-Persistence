using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private TextMeshProUGUI _highScore;
    [SerializeField] private TextMeshProUGUI _topPlayersList;
    [SerializeField] private TMP_InputField _playerNameInputField;

    private void Start()
    {
        gameManager = GameManager.Instance;
        _highScore.text = $"HiScore: {gameManager.GetHiScorePlayer()} : {gameManager.GetHighScore()}";
        _playerNameInputField.text = gameManager.GetPlayerName();
        UpdateTopPlayerList();
    }

    private void UpdateTopPlayerList()
    {
        _topPlayersList.text = "Top Players\n";
        foreach (var player in gameManager.topPlayers)
        {
            _topPlayersList.text += $"{player.playerName} : {player.highScore}\n";
        }
    }
    public void StartGame()
    {
        GameManager.Instance.SetPlayerName(_playerName.text);
        SceneManager.LoadScene(1);
    }

    public void LoadHighscores()
    {
        SceneManager.LoadScene(2);
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
