using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] string _playerName;
    [SerializeField] string _hiScorePlayer;
    public int score;
    [SerializeField] private int _highScore;
    private void Awake()
    {
        _playerName = "Name...";
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    public void SaveData()
    {
        string savePath = $"{Application.persistentDataPath} /saveFile.json";
        
        SaveData data = new SaveData();
        data.playerName = _hiScorePlayer;
        data.highScore = _highScore;
        
        string json = JsonUtility.ToJson(data);
        
        Debug.Log(savePath);
        File.WriteAllText(savePath, json);
    }

    public void LoadData()
    {
        string savePath = $"{Application.persistentDataPath} /saveFile.json";
        
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            _hiScorePlayer = data.playerName;
            _highScore = data.highScore;
        }
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

    private void OnApplicationQuit()
    {
        SaveData();
    }
}

