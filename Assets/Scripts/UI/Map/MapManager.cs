using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    private static MapManager instance;
    private GameObject _player;
    [SerializeField] private MapData mapdata;

    public GameObject mainScene;

    public int stageNum;
    public int nowStage;


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
            Invoke("SceneFalse", 5.0f);
        }
        else if (PlayerPrefs.HasKey("CheckScene") && checkScene == 1)
        {
            SceneFalse();
        }
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
}
