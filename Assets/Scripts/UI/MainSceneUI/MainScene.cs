using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{

    public void OnStageSelector(int sceneNum)
    {
        MapManager.LoadScene(1);
    }

    public void OnReLoder()
    {
        int currentSceneNum = MapManager.GetActiveSceneNum();
        MapManager.LoadScene(currentSceneNum);
    }

}
