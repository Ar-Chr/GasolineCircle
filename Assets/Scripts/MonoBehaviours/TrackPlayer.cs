using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    [SerializeField] private Sound[] music;    

    private int tracksPlayed;
    private bool[] alreadyPlayed;

    private AudioSource currentTrack;

    private float currentVolume;

    private bool trackBeingChosen;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void Update()
    {
        if (currentTrack == null)
        {
            if (!trackBeingChosen)
            {
                StartCoroutine(Delay(4f, PlayNewTrack));
                trackBeingChosen = true;
            }
            return;
        }

        currentTrack.volume = currentVolume;
    }

    private void OnEnable()
    {
        currentVolume = 0.7f;
        trackBeingChosen = false;
        tracksPlayed = 0;
        alreadyPlayed = new bool[music.Length];
    }

    private void PlayNewTrack()
    {
        if (tracksPlayed == music.Length - 1)
            alreadyPlayed = new bool[music.Length];

        int musicNumber = Random.Range(0, music.Length - tracksPlayed);
        for (int i = 0; i < music.Length; i++)
        {
            if (alreadyPlayed[i])
                continue;

            if (musicNumber == 0)
            {
                Play(music[i]);
                alreadyPlayed[i] = true;
                return;
            }

            musicNumber--;
        }
    }
    private void Play(Sound track)
    {
        AudioManager.Instance.Play(track);
        currentTrack = track.source;
        trackBeingChosen = false;
    }

    private void HandleGameStateChanged(GameManager.GameState previous, GameManager.GameState current)
    {
        if (current == GameManager.GameState.PAUSED)
            currentVolume = 0.3f;

        if (current == GameManager.GameState.RUNNING)
            currentVolume = 0.7f;
    }

    private IEnumerator Delay(float delayInSeconds, System.Action action)
    {
        yield return new WaitForSeconds(delayInSeconds);
        action();
    }
}
