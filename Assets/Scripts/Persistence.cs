using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Persistence : MonoBehaviour
{
    public static Persistence Instance;

    public List<HighscoreEntry> highscores = new List<HighscoreEntry>();

    public string currentName;
    public int bestScore;

    [System.Serializable]
    public class HighscoreEntry
    {
        public int score;
        public string name;

        public HighscoreEntry(string name, int score)
        {
            this.score = score;
            this.name = name;
        }
    }

    [System.Serializable]
    private class SaveDataClass
    {
        public string savedName;
        public List<HighscoreEntry> highscores;
    }

    private void Awake()
    {
        if (Instance!=null)
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
        SaveDataClass saveData = new SaveDataClass
        {
            savedName = currentName,
            highscores = highscores.GetRange(0, Mathf.Min(highscores.Count, 10))
        };

        string json = JsonUtility.ToJson(saveData, true);

        string Path = Application.persistentDataPath + "/savedData.json";

        File.WriteAllText(Path, json);
    }

    public void LoadData()
    {
        string Path = Application.persistentDataPath + "/savedData.json";
        if (File.Exists(Path))
        {
            string json = File.ReadAllText(Path);
            SaveDataClass loadedData = JsonUtility.FromJson<SaveDataClass>(json);

            currentName = loadedData.savedName;
            highscores = loadedData.highscores;
            bestScore = highscores[0].score;
        }
    }

    public void CheckHighscore(int points)
    {
        //for (int i = 0; i < highscores.Count; i++)
        //{
        //    if (points >= highscores[i].score)
        //    {
        //        highscores.Insert(i, new HighscoreEntry(currentName, points));
        //        SaveData();
        //        return;
        //    }
        //}
        highscores.Add(new HighscoreEntry(currentName, points));
        highscores.Sort(delegate (HighscoreEntry x, HighscoreEntry y)
        {
            return y.score.CompareTo(x.score);
        });
        SaveData();
    }

    public void OnPlayClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void SaveName(string name)
    {
        currentName = name;
    }

    public void Exit()
    {
        SaveData();
    }
}
