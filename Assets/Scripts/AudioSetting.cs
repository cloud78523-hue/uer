using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume); // slider的數值改變時，執行SetMasterVolume方法
        musicSlider.onValueChanged.AddListener(SetMusicVolume); // slider的數值改變時，執行SetMusicVolume方法
        sfxSlider.onValueChanged.AddListener(SetSFXVolume); // slider的數值改變時，執行SetSFXVolume方法
    }

    public void SetMasterVolume(float volume)
    {
        float v = Mathf.Clamp(volume, 0.0001f, 1f); // 限制音量在0.0001到1之間 
        float db = Mathf.Log10(v) * 20f; // 計算音量對應的db值 
        audioMixer.SetFloat("MasterVolume", db); // 設定主音量成db值 
    }
    public void SetMusicVolume(float volume)
    {
        float v = Mathf.Clamp(volume, 0.0001f, 1f); // 限制音量在0.0001到1之間 
        float db = Mathf.Log10(v) * 20f; // 計算音量對應的db值 
        audioMixer.SetFloat("MusicVolume", db); // 設定音樂音量成db值 
    }

    public void SetSFXVolume(float volume)
    {
        float v = Mathf.Clamp(volume, 0.0001f, 1f); // 限制音量在0.0001到1之間 
        float db = Mathf.Log10(v) * 20f; // 計算音量對應的db值 
        audioMixer.SetFloat("SFXVolume", db); // 設定音效音量成db值 
    }
}
