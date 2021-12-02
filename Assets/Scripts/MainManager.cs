using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class MainManager : MonoBehaviour
{
    private GameManager gameManager;
    
    public Brick brickPrefab;
    public int lineCount = 6;
    public Rigidbody ball;

    public Text scoreText;
    public GameObject gameOverText;
    public Text highScoreText;
    
    private bool _started = false;
    private int _points;
    public string playerName;
    public int highScore;
    
    private bool _gameOver = false;

    private void Awake()
    {
        GenerateBricks();
        gameManager = GameManager.Instance;
        UpdateHighScore();
    }
    
    private void GenerateBricks()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] {1, 1, 2, 2, 5, 5};
        for (int i = 0; i < lineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(brickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!_started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                ball.transform.SetParent(null);
                ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (_gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        _points += point;
        scoreText.text = $"Score : {_points}";
    }

    public void GameOver()
    {
        _gameOver = true;
        gameOverText.SetActive(true);
        UpdateHighScore();
        gameManager.score = _points;
    }

    private void UpdateHighScore()
    {
        highScoreText.text = $"HiScore: {gameManager.GetName()} : {gameManager.GetHighScore()}";
    }

}
