using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    private static MapManager instance;
    private GameObject _player;
    private MapData mapdata;

    public int stageNum;

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
        stageNum = GameManager.Instance.userSelectStage;
        mapdata = new MapData();
        _player = Resources.Load<GameObject>("Prefabs/Player");

        //플레이어 시작위치
        //Instantiate(_player, Vector3.zero, Quaternion.identity);
        Instantiate(_player, mapdata.LoadStagePosition(stageNum), Quaternion.Euler(0, 180, 0));

        SceneManager.sceneLoaded += SceneChange;

    }

    private void SceneChange(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.name == "MainScene")
        {
            //씬 이동시 파괴
            SceneManager.sceneLoaded -= SceneChange;
            Destroy(gameObject);
        }
    }

    public void ReStart()
    {
        GameManager.Instance.userSelectStage = stageNum;
        SceneManager.LoadScene("InGameScene");
    }
}
