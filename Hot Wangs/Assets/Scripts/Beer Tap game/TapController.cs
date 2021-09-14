using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapController : MonoBehaviour
{
    [SerializeField] Transform handle;
    [SerializeField] float handleOffAngle;
    [SerializeField] float handleOnAngle;

    [SerializeField] SpriteRenderer spout;

    [SerializeField] OverflowDetector overflow;
    [SerializeField] ScoreManager score;

    AudioManager audioScript;

    bool isTapOn = false;

    private void Start()
    {
        audioScript = FindObjectOfType<AudioManager>();
        SetTapOn(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !overflow.GetOverflowed() && !score.GetGameOver())
        {
            ToggleTapOn();
        }
    }

    public bool GetTapOn()
    {
        return isTapOn;
    }

    public void SetTapOn(bool tapOn)
    {
        isTapOn = tapOn;
        Vector3 newAngle = handle.rotation.eulerAngles;
        if (isTapOn)
        {
            newAngle.z = handleOnAngle;
            spout.enabled = true;
            audioScript.PlaySound("beer");
        }
        else
        {
            newAngle.z = handleOffAngle;
            spout.enabled = false;
            audioScript.StopSound("beer");
        }
        handle.rotation = Quaternion.Euler(newAngle);
    }

    public void ToggleTapOn()
    {
        SetTapOn(!isTapOn);
    }
}
