using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioClip hoverSoundClip; // 按鈕滑過音效 
    [SerializeField] private AudioClip footStepSound; // 腳步聲音效
    [SerializeField] private AudioSource sfxSoundSource; // 音效來源

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
           
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayButtonHoverSound()
    {
        sfxSoundSource.PlayOneShot(hoverSoundClip);
    }

    public void PlayFootStepSound()
    {
        sfxSoundSource.PlayOneShot(footStepSound);
    }
}
