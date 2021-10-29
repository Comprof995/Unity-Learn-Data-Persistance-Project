using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string CurPlayer, HSPlayerName;
    public int HighScore;

    public void Awake()
    {
        if(GameManager.Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
    }

    public string GetHighScoreStr()
    {
        return "High Score : " + HSPlayerName + " : " + HighScore;
    }

    public bool SubmitHighScore(int score)
    {
        if (score > HighScore)
        {
            SaveData data = new SaveData();
            data.HighScore = HighScore = score;
            data.HSPlayerName = HSPlayerName = CurPlayer;

            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/SaveData.json", json);

            return true;
        }

        return false;
    }

    public void LoadHighScore()
    {
        string filePath = Application.persistentDataPath + "/SaveData.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighScore = data.HighScore;
            HSPlayerName = data.HSPlayerName;
        }
    }

    [System.Serializable]
    private class SaveData
    {
        public int HighScore;
        public string HSPlayerName;
    }
}
