using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreTableEntry : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI placeText;
    [SerializeField]
    TextMeshProUGUI nameText;
    [SerializeField]
    TextMeshProUGUI scoreText;

    public void SetText(string name, int score)
    {
        nameText.text = name;
        scoreText.text = score.ToString();
    }
}
