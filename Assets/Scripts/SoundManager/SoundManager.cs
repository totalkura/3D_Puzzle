using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public enum bgm
    {
        Main,
        InGame
    }

    public enum other
    {
        button,
        door,
    }

    public enum playerActive
    {
        work
    }

    public static SoundManager instance;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] AudioClip[] bgms;
    [SerializeField] AudioClip[] others;
    [SerializeField] AudioClip[] players;

    [SerializeField] AudioSource listenBgm;
    [SerializeField] AudioSource listenOther;
    [SerializeField] AudioSource listenplayer;

    [Range(0f, 1f)] public float masterVolume;
    [Range(0f, 1f)] public float bgmVolume;
    [Range(0f, 1f)] public float mixVolume;

    string musicPath = "Sounds/Musics/";
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();

                if (instance == null)
                {
                    instance = new GameObject("SoundManager").AddComponent<SoundManager>();
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        others = Resources.LoadAll<AudioClip>(musicPath + "OtherSounds");
        bgms = Resources.LoadAll<AudioClip>(musicPath + "BGMs");
        players = Resources.LoadAll<AudioClip>(musicPath + "Players");


        //시작시 볼륨 값 고정
        masterVolume = 0.7f;
        bgmVolume = 0.5f;
        mixVolume = 0.5f;
    }

    public void Start()
    {

        UpdateVolume();
        PlayBGM(bgm.Main);
    }

    public void StopSounds()
    {
        listenBgm.Stop();
        listenOther.Stop();
        listenplayer.Stop();
    }

    public void PlayBGM(bgm bgmIndex)
    {
        listenBgm.clip = bgms[(int)bgmIndex];
        listenBgm.Play();
    }

    public void PlayOther<TEnum>(TEnum mixer, int checks = 0) where TEnum : Enum
    {
        AudioClip clip = null;
        AudioSource source = null;

        string enumName = typeof(TEnum).Name;

        switch (enumName)
        {
            case "other":
                clip = others[Convert.ToInt32(mixer)];
                source = listenOther;
                break;
            case "playerActive":
                clip = players[Convert.ToInt32(mixer)];
                source = listenplayer;
                break;
        }
            

        if (source.isPlaying && source.clip == clip && checks != 0)
        {
            float checkPercent = source.time / clip.length;

            if (checkPercent < 0.65f)
            {
                if (checks == 1)
                    source.pitch = 2.0f;
                return;
            }
        }

        if (checks == 0) source.pitch = 1.0f;
        else if (checks == 1) source.pitch = 2.0f;

        source.clip = clip;
        source.loop = false;
        source.Play();

    }


    private void UpdateVolume()
    {
        audioMixer.SetFloat("Master", LinearToDecibel(masterVolume));
        audioMixer.SetFloat("BGM", LinearToDecibel(bgmVolume));
        audioMixer.SetFloat("Other", LinearToDecibel(mixVolume));
        audioMixer.SetFloat("Player", LinearToDecibel(mixVolume));
    }

    private float LinearToDecibel(float value)
    {
        return Mathf.Approximately(value, 0f) ? -80f : Mathf.Log10(value) * 20f;
    }

    public void SetMasterVolume(float values)
    {
        masterVolume = values;
        UpdateVolume();
    }

    public void SetBgmVolume(float values)
    {
        bgmVolume = values;
        UpdateVolume();
    }

    public void SetOtherVolume(float values)
    {
        mixVolume = values;
        UpdateVolume();
    }

}
