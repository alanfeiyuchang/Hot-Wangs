using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;

    AudioManager audioScript;

    private void Start()
    {
        audioScript = FindObjectOfType<AudioManager>();
        slider.value = audioScript.sliderVal;
        SetVolume(audioScript.sliderVal);
    }

    public void SetVolume(float sliderVal)
    {
        if (audioScript != null)
        {
            mixer.SetFloat("Volume", Mathf.Log10(sliderVal) * 20);
            audioScript.sliderVal = sliderVal;
        }
    }
}
