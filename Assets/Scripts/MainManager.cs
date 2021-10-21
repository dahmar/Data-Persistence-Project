using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody2D Ball;

    public Text ScoreText;
    public Text bestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private int m_BrickCount;
    
    private bool m_GameOver = false;

    private bool ScoreUpdated = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        SetBestScoreText();
        
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = i;
                brick.onDestroyed.AddListener(AddPoint);
                m_BrickCount++;
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);

                Ball.velocity = forceDir * 2.0f;
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                LoadMenu();
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point + 1;
        ScoreText.text = $"Score : {m_Points}";
        m_BrickCount--;
        if (m_BrickCount < 1)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        UpdateHighscore();
        SetBestScoreText();
    }

    private void SetBestScoreText()
    {
        bestScoreText.text = "Best Score: " + Persistence.Instance.bestScore.ToString() +
                             " Name: " + Persistence.Instance.currentName;
    }

    public void LoadMenu()
    {
        UpdateHighscore();
        SceneManager.LoadScene(0);
    }

    private void UpdateHighscore()
    {
        if (!ScoreUpdated)
        {
            Persistence.Instance.CheckHighscore(m_Points);
            ScoreUpdated = true;
        }
    }
}
