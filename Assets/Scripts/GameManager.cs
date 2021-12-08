using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] string _playerName;
    [SerializeField] string _hiScorePlayer;
    public int score;
    [SerializeField] private int _highScore;
    public List<PlayerData> topPlayers;
    private string _lastPlayerName;
    public PlayerData playerData;
    public List<Brick> remainingBricks;

    private void Awake()
    {
        LoadData();
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        playerData = new PlayerData();
        _playerName = _lastPlayerName;
    }

    [ContextMenu("Save")]
    public void SaveData()
    {
        
        string savePath = $"{Application.persistentDataPath} /saveFile.json";
        
        SaveData allPlayers = new SaveData();
        allPlayers.topPlayers = topPlayers;
        allPlayers.lastPlayer = _playerName;
        
        string json = JsonUtility.ToJson(allPlayers);
        
        File.WriteAllText(savePath, json);
        Debug.Log($"Game saved to {savePath} json: {json}");
        
    }

    public void LoadData()
    {
        SaveData data = new SaveData();
        string savePath = $"{Application.persistentDataPath} /saveFile.json";

        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            
            data = JsonUtility.FromJson<SaveData>(json);
            topPlayers = data.topPlayers;
            //Sort top players by score
            SortPlayers();
            _lastPlayerName = data.lastPlayer;
            _hiScorePlayer = topPlayers[0].playerName;
            _highScore = topPlayers[0].highScore;
        }
        else if (!File.Exists(savePath))
        {
            string lp = "Name...";
            PlayerData pd = new PlayerData{ highScore = 0,playerName = "Best Player"};
            data.lastPlayer = lp;
            data.topPlayers = new List<PlayerData> {pd};
            topPlayers = data.topPlayers;
            _lastPlayerName = data.lastPlayer;
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(savePath, json);
        }
    }

    private void SortPlayers()
    {
        topPlayers = topPlayers.OrderByDescending(i => i.highScore).ToList();
    }

    public int GetHighScore()
    {
        if (score > _highScore)
        {
            _highScore = score;
            _hiScorePlayer = _playerName;
        }

        return _highScore;
    }

    public string GetPlayerName()
    {
        return _playerName;
    }

    public string GetHiScorePlayer()
    {
        return _hiScorePlayer;
    }

    public void SetPlayerName(string playerName)
    {
        _playerName = playerName;
    }
    
/*
 * p1 : 100
 * p2 : 50
 * p3 : 25
 * p4 : 0
 */
    public string UpdateTopPlayers()
    {
        int topPlayerIndex = 10;
        string playerString = $"Top Players\n";

        foreach (var player in topPlayers)
        {
            Debug.Log($"Top player: {player.playerName}");
            if (score > player.highScore){
                topPlayers.Add(new PlayerData{ highScore = score,playerName = _playerName});
                break;
            }

        }

        SortPlayers();
        if (topPlayers.Count > 10)
        {
            topPlayers.RemoveRange(topPlayerIndex,topPlayers.Count - topPlayerIndex);
        }
        
        
        foreach (var player in topPlayers)
        {
            playerString += $"{player.playerName} : {player.highScore}\n";
        }
        ResetScore();
        return playerString;
    }

    private void ResetScore()
    {
        score = 0;
    }

    /*
     * 0 - MainMenu
     * 1 - Game
     * 2 - Highscores
     */
    public void LoadSceneX(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    
    private void OnApplicationQuit()
    {
        SaveData();
    }
}

