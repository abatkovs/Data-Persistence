using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public string lastPlayer;
    public List<PlayerData> topPlayers = new List<PlayerData>();
}

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int highScore;
}