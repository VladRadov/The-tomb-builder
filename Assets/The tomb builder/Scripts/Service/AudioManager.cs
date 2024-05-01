using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourceMusic;
    [SerializeField] private AudioSource _audioSourceSound;
    [SerializeField] private List<Sound> _audio;

    public static AudioManager Instance { get; private set; }

    public void ChangeStateMusic()
    {
        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.IsMusicOn == "1")
            _audioSourceMusic.Play();
        else
            _audioSourceMusic.Stop();
    }

    public void ChangeStateSound()
    {
        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.IsSoundOn == "1")
            _audioSourceSound.Play();
        else
            _audioSourceSound.Stop();
    }

    public bool IsAudioOn()
        => ContainerSaveerPlayerPrefs.Instance.SaveerData.IsMusicOn == "1" ? true : false;

    public bool IsSoundOn()
        => ContainerSaveerPlayerPrefs.Instance.SaveerData.IsSoundOn == "1" ? true : false;

    public void PlayClickButton() => PlaySound("ClickButton");

    public void PlayMusicMenu() => PlayMusic("Menu");

    public void PlayMusicGame() => PlayMusic("Game");

    public void PlayWinner() => PlayMusic("Win");

    public void PlayGameOver() => PlayMusic("GameOver");

    public void PlayGetCoin() => PlaySound("GetCoin");

    public void PlayDropTomb() => PlaySound("DropTomb");

    public void PlayBuildBlock() => PlaySound("BuildBlock");

    private void PlayMusic(string name)
    {
        var audio = FindAudio(name);

        if (audio != null && IsAudioOn())
        {
            _audioSourceMusic.clip = audio.Music;
            _audioSourceMusic.Play();
        }
    }

    private void SetMusicSource(string name)
    {
        var audio = FindAudio(name);

        if (audio != null)
        {
            _audioSourceMusic.clip = audio.Music;

            if(IsAudioOn())
                _audioSourceMusic.Play();
        }
    }

    private void PlaySound(string name)
    {
        var audio = FindAudio(name);

        if (audio != null && IsSoundOn())
        {
            _audioSourceSound.clip = audio.Music;
            _audioSourceSound.Play();
        }
    }

    private Sound FindAudio(string name)
        => _audio.Find(audio => audio.Name == name);

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);

        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        if (IsAudioOn() == false)
            _audioSourceMusic.Stop();

        if (IsSoundOn() == false)
            _audioSourceSound.Stop();

        SetMusicSource(ManagerScenes.Instance.NameActiveScene);
    }
}