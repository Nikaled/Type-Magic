using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundPanel : MonoBehaviour
{
    public static SoundPanel instance;
    private bool IsSoundOn = true;
    private bool IsMusicOn = true;

    public Slider SoundSlider;
    public Slider MusicSlider;
    public Toggle SoundToggle;
    public Toggle MusicToggle;
    public GameObject BackgroundMusicObject;

    public AudioMixerGroup Mixer;

    private float _startSoundVolume = 0;
    private float _startMusicVolume = -25;
    private float _volumeSoundBeforeMute;
    private float _volumeMusicBeforeMute;
    
    private readonly string SoundPrefsName = "SoundPrefsName";
    private readonly string MusicPrefsName = "MusicPrefsName";
    private readonly string ToggleSoundPrefsName = "ToggleSoundPrefsName";
    private readonly string ToggleMusicPrefsName = "ToggleMusicPrefsName";
    private void Start()
    {
        instance = this;
        LoadVolumeSettings();
    }
    public void LoadVolumeSettings()
    {
        LoadVolumeSettingsFromPrefs();
        SetVolumeAndSlider(_startSoundVolume, ref _volumeSoundBeforeMute, SoundSlider, "Master");
        SetVolumeAndSlider(_startMusicVolume, ref _volumeMusicBeforeMute, MusicSlider, "Music");

        SoundToggle.isOn = IsSoundOn;
        MusicToggle.isOn = IsMusicOn;
    }
    private void LoadVolumeSettingsFromPrefs()
    {
        SetSoundValue(ref _startSoundVolume, SoundPrefsName);
        SetSoundValue(ref _startMusicVolume, MusicPrefsName);
        SetToggle(ref IsSoundOn, ToggleSoundPrefsName);
        SetToggle(ref IsMusicOn, ToggleMusicPrefsName);

        void SetSoundValue(ref float ValueToSet, string prefsKey)
        {
            if (PlayerPrefs.HasKey(prefsKey))
            {
                ValueToSet = PlayerPrefs.GetFloat(prefsKey);
            }
        }
        void SetToggle(ref bool Toggle, string prefsKey)
        {
            if (PlayerPrefs.HasKey(prefsKey))
            {
                Toggle = Convert.ToBoolean(PlayerPrefs.GetInt(prefsKey));

            }
        }
    }
    private void SetVolumeAndSlider(float StartVolumeLevel, ref float VolumeBeforeMute, Slider slider, string MixerName)
    {
        VolumeBeforeMute = StartVolumeLevel;
        slider.value = StartVolumeLevel;
        Mixer.audioMixer.SetFloat(MixerName, StartVolumeLevel);
    }
    
    public void Toggle(bool enabled, string MixerName, ref float VolumeBeforeMute)
    {
        if (enabled)
            Mixer.audioMixer.SetFloat(MixerName, VolumeBeforeMute);
        else
        {
            Mixer.audioMixer.GetFloat(MixerName, out VolumeBeforeMute);
            Mixer.audioMixer.SetFloat(MixerName, -80);
        }
    }
    public void ToggleSound(bool enabled)
    {
        Toggle(enabled, "Master", ref _volumeSoundBeforeMute);

        PlayerPrefs.SetInt(ToggleSoundPrefsName, Convert.ToInt32(enabled));
        PlayerPrefs.SetFloat(SoundPrefsName, _volumeSoundBeforeMute);

    }
    public void ToggleMusic(bool enabled)
    {
        Toggle(enabled, "Music", ref _volumeMusicBeforeMute);

        PlayerPrefs.SetInt(ToggleMusicPrefsName, Convert.ToInt32(enabled));
        PlayerPrefs.SetFloat(MusicPrefsName, _volumeMusicBeforeMute);
    }
    public void ChangeSlider(ref float volume, string MixerName, Slider slider, Toggle toggle)
    {
        Mixer.audioMixer.SetFloat(MixerName, volume);
        toggle.isOn = true;
        if (volume == slider.minValue)
        {
            volume = -80;
            Mixer.audioMixer.SetFloat(MixerName, volume);
        }
    }
    public void ChangeSoundVolume(float volume)
    {
        ChangeSlider(ref volume, "Master", SoundSlider, SoundToggle);
        _volumeSoundBeforeMute = volume;
        PlayerPrefs.SetFloat(SoundPrefsName, volume);
    }
    public void ChangeMusicVolume(float volume)
    {
        ChangeSlider(ref volume, "Music", MusicSlider, MusicToggle);
        _volumeMusicBeforeMute = volume;
        PlayerPrefs.SetFloat(MusicPrefsName, volume);
    }
}
