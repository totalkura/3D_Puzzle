using UnityEngine;

public class StageCheck : MonoBehaviour
{

    [SerializeField]
    private int stageCheck;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (stageCheck > MapManager.Instance.nowStage)
            {
                if(stageCheck >= GameManager.Instance.userLastStage)
                    PlayerPrefs.SetInt("LastStage",stageCheck);
                GameManager.Instance.userLastStage = stageCheck;
                MapManager.Instance.nowStage = stageCheck;
            }
        }
    }
}
