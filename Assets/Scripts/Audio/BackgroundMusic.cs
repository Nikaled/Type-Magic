using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
   public static BackgroundMusic instance { get; private set; }
    [SerializeField] private AudioClip musicChill;
    [SerializeField] private AudioClip musicNeChill;
    private AudioSource musicSource;
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        if (instance == null) { 
        DontDestroyOnLoad(this);
        instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        musicSource.clip = musicChill;
    }
    public void SetMusicToChill()
    {
        musicSource.clip = musicChill;
        musicSource.Play();
    }
    public void SetMusicToNeChill()
    {
        musicSource.clip = musicNeChill;
    }
    public void PauseMusic()
    {
        musicSource.Pause();
    }
    public void UnPauseMusic()
    {
        musicSource.UnPause();

    }
}
