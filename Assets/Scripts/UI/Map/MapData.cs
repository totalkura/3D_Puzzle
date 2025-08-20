using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapData
{
    List<Vector3> stagePosition = new List<Vector3>();

    public MapData()
    {
        LoadStageData();
    }

    private List<Vector3> LoadStageData()
    {
        GameObject[] stageTransforms = GameObject.FindGameObjectsWithTag("CheckPoint");

        //맨처음 위치
        stagePosition.Add(Vector3.zero);

        //스테이지 입구 위치 가져오기
        foreach (GameObject transforms in stageTransforms)
        {
            stagePosition.Add(transforms.transform.position);
        }

        stagePosition = stagePosition.OrderByDescending(v => v.x).ToList();

        return stagePosition;
    }

    public Vector3 LoadStagePosition(int stagenum)
    {
        
        return stagePosition[stagenum];
    }

}
