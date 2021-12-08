using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopPlayers : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _topPlayersList;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
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
}
