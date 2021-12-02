using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] string _playerName;
    public int score;
    private int _highScore;
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
    }

    public void SaveData()
    {
        
    }

    public void LoadData()
    {
        
    }

    public int GetHighScore()
    {
        if (score > _highScore)
        {
            _highScore = score;
        }

        return _highScore;
    }

    public string GetName()
    {
        return _playerName;
    }

    public void SetPlayerName(string name)
    {
        _playerName = name;
    }
}

