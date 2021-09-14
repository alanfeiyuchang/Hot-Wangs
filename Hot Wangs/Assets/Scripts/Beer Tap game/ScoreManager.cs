using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] int mugsLeft = 3;
    float timer;

    bool gameOver = false;

    [SerializeField] float timerTotal;

    [SerializeField] Text mugsLeftDisplay;
    [SerializeField] Text timerDisplay;
    [SerializeField] GameObject overflowDisplay;
    [SerializeField] GameObject winDisplay;
    [SerializeField] GameObject loseDisplay;

    [SerializeField] Button reset;

    [SerializeField] BeerController beer;
    [SerializeField] TapController tap;
    [SerializeField] MugController mug;
    [SerializeField] OverflowDetector overflow;

    AudioManager audio;

    private void Start()
    {
        timer = timerTotal;
        audio = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f && !gameOver)
        {
            timer = 0f;
            audio.PlaySound("timer");
            tap.SetTapOn(false);
            loseDisplay.SetActive(true);
            reset.interactable = false;
            Time.timeScale = 0f;
            gameOver = true;
        }

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        timerDisplay.text = Mathf.CeilToInt(timer).ToString();
        mugsLeftDisplay.text = mugsLeft.ToString();
    }

    public void Submit()
    {
        mugsLeft -= 1;
        audio.PlaySound("submit");
        Reset();

        if (mugsLeft <= 0)
        {
            winDisplay.SetActive(true);
            audio.PlaySound("win");
            reset.interactable = false;
            //Time.timeScale = 0f;
            gameOver = true;
            
        }
        StartCoroutine(waitToTransition());
    }

    IEnumerator waitToTransition()
    {
        Debug.Log("Before Transition");
        yield return new WaitForSeconds(3f);
        Debug.Log("Transition");
        if (OverflowDetector.mistake)
        {
            SceneManager.LoadScene("MistakeScene");
        }
        else
        {
            SceneManager.LoadScene("NoMistakeScene");
        }
    }

    public void Reset()
    {
        overflowDisplay.SetActive(false);
        Time.timeScale = 1f;
        tap.SetTapOn(false);
        beer.ResetBeer();
        mug.ResetMug();
        overflow.SetOverflowed(false);
    }

    public bool GetGameOver()
    {
        return gameOver;
    }
}
