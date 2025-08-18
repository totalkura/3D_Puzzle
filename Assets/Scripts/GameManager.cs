using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField]
    private int userLastStage;
    public int userSelectStage;

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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (PlayerPrefs.HasKey("LastStage"))
                userLastStage = PlayerPrefs.GetInt("LastStage");
        }
        else if (instance != this) Destroy(gameObject);

    }

    public int StageCheck(int stage)
    {
        return userSelectStage = stage;
    }

    //UI매니저에서 관리 아래거
    /*
    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }
    */
}
