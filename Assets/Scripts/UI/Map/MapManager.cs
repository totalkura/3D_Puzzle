using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    private static MapManager instance;
    private GameObject _player;
    [SerializeField] private MapData mapdata;

    public GameObject mainScene;
    public GameObject[] offObjects;

    public int stageNum;
    public int nowStage;

    List<string> message;

    public TextMeshProUGUI textMeshUGUI;

    public static MapManager Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MapManager>();

                if (instance == null)
                {
                    instance = new GameObject("MapManager").AddComponent<MapManager>();
                }
            }
            return instance;
        }
    }


    private void Start()
    {
        SoundManager.instance.StopSounds();
        SoundManager.instance.PlayBGM(SoundManager.bgm.InGame);

        TextMessage();

        stageNum = GameManager.Instance.userSelectStage;
        mapdata = new MapData();
        _player = Resources.Load<GameObject>("Prefabs/Player");
        _player.name = "Player";
        //플레이어 시작위치
        Instantiate(_player, mapdata.LoadStagePosition(stageNum), Quaternion.Euler(0, 180, 0));

        
        int checkScene = PlayerPrefs.GetInt("CheckScene");
        if (PlayerPrefs.HasKey("CheckScene") && checkScene == 0)
        {
            CharacterManager.Instance.Player.controller.isPlay = false;

            StartCoroutine(TextDelay());

            Invoke("SceneFalse", 12.0f);
        }
        else if (PlayerPrefs.HasKey("CheckScene") && checkScene == 1)
        {
            SceneFalse();
        }

        GameObjectTurnOff();
    }


    public void ReStart()
    {
        GameManager.Instance.userSelectStage = nowStage;
        SceneManager.LoadScene("InGameScene");
    }

    public void SceneFalse()
    {
        CharacterManager.Instance.Player.controller.isPlay = true;
        PlayerPrefs.SetInt("CheckScene", 1);
        mainScene.SetActive(false);
    }

    IEnumerator TextDelay()
    {
        for (int i = 0; i < message.Count; i++)
        {
            textMeshUGUI.text = message[i];

            yield return new WaitForSeconds(3.0f);
        }
    }

    private void TextMessage()
    {
        message = new List<string>();
        message.Add(".....");
        message.Add("머리가 너무 아파..");
        message.Add("여긴 어디지..");
        message.Add("(주위를 살펴 본다)");
    }

    private void GameObjectTurnOff()
    {
        for (int i = 0; i < offObjects.Length; i++)
        {
            offObjects[i].gameObject.SetActive(false);
        }
    }
}
