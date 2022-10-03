using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] _musicClips;
    [SerializeField] private AudioSource _musicSource;
    [Space]
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private AudioSource _audioSource;


    private int _currentSlot = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayOneShotAudio()
    {

    }

    public void PlayMusic(int slot)
    {

    }
}
