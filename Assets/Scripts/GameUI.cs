using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject newScorePrefab;
    [SerializeField] private Text scoreText;

    private int score = 0;
    
    void Start()
    {
        UpdateScore();
    }

    void Update()
    {
        
    }

    void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    public void AddScore(int _score)
    {
        score += _score;
        StartCoroutine(ShowPopupScore(_score));
        UpdateScore();
    }

    IEnumerator ShowPopupScore(int score)
    {
        GameObject scoreText = Instantiate(newScorePrefab, transform);
        Text text = scoreText.GetComponent<Text>();
        text.text = (score > 0 ? '+' : ' ') + score.ToString();
        text.color = score > 0 ? Color.green : Color.red;
        yield return new WaitForSeconds(1f);
        Destroy(scoreText);
    }
}
