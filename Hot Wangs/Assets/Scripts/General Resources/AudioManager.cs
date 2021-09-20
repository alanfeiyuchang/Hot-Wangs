using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] SoundData[] soundDatas;

    public static AudioManager instance;

    [HideInInspector] public float sliderVal;

    string currentMusic = "music";

    [System.Serializable]
    public struct SoundData
    {
        public string soundName;
        public AudioSource soundSource;
        public float minPitch;
        public float maxPitch;
        public float minVol;
        public float maxVol;
    }

    Dictionary<string, SoundData> sounds = new Dictionary<string, SoundData>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        sliderVal = 0.25f;

        foreach (SoundData s in soundDatas)
        {
            sounds.Add(s.soundName, s);
        }

        PlaySound("music");
    }

    private void Update()
    {
    }

    public void PlaySound(string soundName)
    {
        if (sounds.ContainsKey(soundName))
        {
            if (soundName.Contains("music"))
            {
                sounds[currentMusic].soundSource.Stop();
                currentMusic = soundName;
            }
            sounds[soundName].soundSource.pitch = Random.Range(sounds[soundName].minPitch, sounds[soundName].maxPitch);
            sounds[soundName].soundSource.volume = Random.Range(sounds[soundName].minVol, sounds[soundName].maxVol);
            sounds[soundName].soundSource.Play();
        }
        else
        {
            Debug.LogError("Sound requested does not exist");
        }
    }

    public void StopSound(string soundName)
    {
        if (sounds.ContainsKey(soundName))
        {
            sounds[soundName].soundSource.Stop();
        }
        else
        {
            Debug.LogError("Sound requested does not exist");
        }
    }

    public bool IsSoundPlaying(string soundName)
    {
        bool result = false;
        if (sounds.ContainsKey(soundName))
        {
            if (sounds[soundName].soundSource.isPlaying)
            {
                result = true;
            }
        }
        else
        {
            Debug.LogError("Sound requested does not exist");
        }
        return result;
    }
}
