using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioClip menu;
    [SerializeField] private AudioClip game;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayClip(int index)
    {
        source.PlayOneShot(clips[index]);
    }

    public void PlayMusic(AudioClip music)
    {
        source.Stop();
        source.clip = music;
        source.Play();
    }
}
