using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField]
    private int userLastStage;
    public int userSelectStage;
    
    public Vector3 playerPosition;


    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(PlayerPrefs.HasKey("LastStage"))
            userLastStage = PlayerPrefs.GetInt("LastStage");
        DontDestroyOnLoad(gameObject);
    }

    public int StageLoad(int stage)
    {
        return userSelectStage = stage;
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }
}
