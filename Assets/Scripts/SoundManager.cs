using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] AudioClip[] bgms;
    [SerializeField] AudioClip[] others;

    [SerializeField] AudioSource listenBgm;
    [SerializeField] AudioSource listenOther;

    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float bgmVolume = 1f;
    [Range(0f, 1f)] public float otherVolume = 1f;

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
    }

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

    public void Start()
    {
        PlayBGM(bgm.Main);
        UpdateVolume();
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

    public void PlayOther(other otherIndex)
    {
        listenOther.loop = false;
        listenOther.PlayOneShot(others[(int)otherIndex]);
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
        Debug.Log(values);
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
