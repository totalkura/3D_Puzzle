using UnityEngine;

public class MapManager : MonoBehaviour
{
    private static MapManager instance;

    public int stageNum;

    public static MapManager Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MapManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        
    }

    public void LoadCheckPoint(int stage)
    {
        Vector3 playerPosition = CharacterManager.Instance.Player.transform.position;

        PlayerPrefs.SetFloat("positionX", playerPosition.x);
        PlayerPrefs.SetFloat("positionY", playerPosition.y);
        PlayerPrefs.SetFloat("positionZ", playerPosition.z);




    }


}
