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
        work,
        run
    }

    public static SoundManager instance;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] AudioClip[] bgms;
    [SerializeField] AudioClip[] others;

    [SerializeField] AudioSource listenBgm;
    [SerializeField] AudioSource listenOther;

    [Range(0f, 1f)] public float masterVolume;
    [Range(0f, 1f)] public float bgmVolume;
    [Range(0f, 1f)] public float otherVolume;

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


        //시작시 볼륨 값 고정
        masterVolume = 0.7f;
        bgmVolume = 0.5f;
        otherVolume = 0.3f;
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
    }

    public void PlayBGM(bgm bgmIndex)
    {
        listenBgm.clip = bgms[(int)bgmIndex];
        listenBgm.Play();
    }

    public void PlayOther(other otherIndex, int checks = 0)
    {
        AudioClip clip = others[(int)otherIndex];

        if (listenOther.isPlaying && listenOther.clip == clip)
        {
            float checkPercent = listenOther.time / clip.length;

            if (checkPercent < 0.65f)
            {
                if (checks == 1)
                    listenOther.pitch = 2.0f;
                return;
            }
        }

        if (checks == 0) listenOther.pitch = 1.0f;
        else if (checks == 1) listenOther.pitch = 2.0f;

            listenOther.clip = clip;
        listenOther.loop = false;
        listenOther.Play();

    }

    private void UpdateVolume()
    {
        audioMixer.SetFloat("Master", LinearToDecibel(masterVolume));
        audioMixer.SetFloat("BGM", LinearToDecibel(bgmVolume));
        audioMixer.SetFloat("Other", LinearToDecibel(otherVolume));
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
        otherVolume = values;
        UpdateVolume();
    }

}
