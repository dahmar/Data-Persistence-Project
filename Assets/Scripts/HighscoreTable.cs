using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreTable : MonoBehaviour
{
    [SerializeField]
    GameObject table;
    [SerializeField]
    GameObject[] entries;

    public void FillTable()
    {
        var highscores = Persistence.Instance.highscores;
        var length = Mathf.Min(highscores.Count, entries.Length);
        for (int i = 0; i < length; i++)
        {
            if (i > entries.Length){ return; }

            var entry = entries[i].GetComponent<HighscoreTableEntry>();
            entry.SetText(highscores[i].name, highscores[i].score);
        }
    }
}
