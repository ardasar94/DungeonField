using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;

    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    private void Awake()
    {
        StopAllCoroutines();
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();

    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            StartCoroutine("WaitAndTakeLife");
            //TakeLife();
        }
        else
        {
            ResetGameSesion();
        }
    }

    private void TakeLife()
    {
        playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    IEnumerator WaitAndTakeLife()
    {
        yield return new WaitForSeconds(2);
        TakeLife();
        StopAllCoroutines();
    }

    private void ResetGameSesion()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
