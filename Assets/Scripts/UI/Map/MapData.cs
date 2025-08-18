using System.Collections.Generic;
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

        //��ó�� ��ġ
        stagePosition.Add(Vector3.zero);

        //�������� �Ա� ��ġ ��������
        foreach (GameObject transforms in stageTransforms)
        {
            stagePosition.Add(transforms.transform.position);
        }

        return stagePosition;
    }

    public Vector3 LoadStagePosition(int stagenum)
    {
        return stagePosition[stagenum];
    }
}
