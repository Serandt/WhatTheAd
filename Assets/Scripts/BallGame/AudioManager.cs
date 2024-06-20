using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioClip loseLive;
    public AudioClip gameOver;
    public AudioClip conditionComplete;

    private AudioSource audioSource;

    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void LoseLive()
    {
        Debug.Log("PlayLoseLive");
        audioSource.clip = loseLive;
        audioSource.Play();
    }

    public void GameOver()
    {
        audioSource.clip = gameOver;
        audioSource.Play();
    }

    public void ConditionComplete()
    {
        audioSource.clip = conditionComplete;        audioSource.Play();    }
}
