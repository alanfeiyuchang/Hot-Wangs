using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverflowDetector : MonoBehaviour
{
    public static bool mistake = false;

    [SerializeField] GameObject overflowDisplay;
    [SerializeField] TapController tap;

    AudioManager audio;

    bool overflowed = false;

    private void Start()
    {
        audio = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Beer" || collision.gameObject.name == "Froth")
        {
            mistake = true;
            overflowDisplay.SetActive(true);
            audio.PlaySound("overflow");
            tap.SetTapOn(false);
            Time.timeScale = 0f;
            overflowed = true;
        }
    }

    public bool GetOverflowed()
    {
        return overflowed;
    }

    public void SetOverflowed(bool isOverflowed)
    {
        overflowed = isOverflowed;
    }
}
