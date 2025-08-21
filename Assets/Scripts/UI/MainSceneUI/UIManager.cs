using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    // ========== SetActive ===================

    [Header("Stage Create")]
    public GameObject thisObjects;
    public GameObject stage;

    public Sprite[] _images;
    public List<GameObject> stages;

    public int maxStage;

    [Header("메인 메뉴")]
    public GameObject StageScene;
    public GameObject MainButton;
    public GameObject BackSpace;
    public GameObject OptionScene;
    public GameObject Option;

    [Header("페이드 애니메이터")]
    public Animator animator;
    public float transitionTime;



    private void Start()
    {
        maxStage = 6;
        Tests();
        StageScene.SetActive(false);
    }

    // ========== 씬 전환 ===================
    public void OnNEWStageSelector(int sceneNum)
    {
        GameManager.Instance.StageCheck(sceneNum);
        StartCoroutine(LoadSceneWithFade("InGameScene"));
    }

    // ========== 스테이지 선택 버튼 ===================

    public void OnStageSelector()
    {
        if (StageScene.activeSelf)
        {
            // 메인 메뉴가 켜져 있으면 → 스테이지 선택 화면으로 전환
            StageScene.SetActive(false);
            animator.SetTrigger("FadeOut");
            MainButton.SetActive(true);
        }
        else
        {
            // 스테이지 선택 화면이 켜져 있으면 → 메인 메뉴로 전환
            MainButton.SetActive(false);
            animator.SetTrigger("FadeOut");
            StageScene.SetActive(true);
        }
    }

    // ========== 메인 메뉴 버튼 ===================

    public void OnNewStageSelector()
    {
        GameManager.Instance.StageCheck(0);
        PlayerPrefs.SetInt("CheckScene", 0);
        animator.SetTrigger("FadeOut");
        SceneManager.LoadScene("InGameScene");
    }

    public void OnReLoder(int sceneNum)
    {
        GameManager.Instance.StageCheck(sceneNum);
        animator.SetTrigger("FadeOut");
        SceneManager.LoadScene("InGameScene");
    }

    // ========== 스테이지 선택 버튼 ===================

    public void OnStage1(int sceneNum)
    {
        GameManager.Instance.StageCheck(sceneNum);
        animator.SetTrigger("FadeOut");
        SceneManager.LoadScene("InGameScene");
    }


    public void OnLoadStage()
    {
        GameManager.Instance.StageCheck(GameManager.Instance.userLastStage);
        animator.SetTrigger("FadeIn");
        Invoke("InvokeStage", 1f);
    }

    public void InvokeStage() 
    {
        SceneManager.LoadScene("InGameScene");
    }


    // ========== 뒤로가기 버튼 ===================

    public void OnBackSpace()
    {
        StageScene.SetActive(false);
        animator.SetTrigger("FadeOut");
        MainButton.SetActive(true);
    }

    // ========== 옵션 버튼 ===================
    public void OnOption()
    {
        MainButton.SetActive(false);
        OptionScene.SetActive(true);
        animator.SetTrigger("FadeOut");
    }

    // 옵션 버튼이 눌리면 -> 메인 메뉴로 전환
    public void CloseOption()
    {
        OptionScene.SetActive(false);  // 옵션 메뉴 비활성화
        MainButton.SetActive(true);    // 메인 메뉴 다시 활성화
        animator.SetTrigger("FadeOut");
    }


    // ========== 데이터 초기화 ===================

    public void ResetData()
    {
        Debug.Log("Check");
        PlayerPrefs.DeleteAll();
        GameManager.Instance.userLastStage = 0;
        SceneManager.LoadScene("MainScene");
    }


    // ========== 씬 전환 애니메이션 ===================

    public void LoadStage(int sceneNum)
    {
        GameManager.Instance.StageCheck(sceneNum);
        StartCoroutine(LoadSceneWithFade("InGameScene"));
    }

    private IEnumerator LoadSceneWithFade(string sceneName)
    {
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(transitionTime); // FadeOut 애니메이션 길이
        SceneManager.LoadScene(sceneName);
    }




    
    public void Tests()        
    {
        _images = Resources.LoadAll<Sprite>("Images\\Stage");
        thisObjects = Resources.Load<GameObject>("Prefabs\\Stage1");
        stage = GameObject.Find("Stages");

        stages = new List<GameObject>();

        for (int i = 0; i < maxStage; i++)
        {
            GameObject tesss = Instantiate(thisObjects, stage.transform);
            tesss.name = "Stage" + (i+1);
            tesss.GetComponentInChildren<LockButton>().setInit(i +1);
            stages.Add(tesss); 
        }

        int stageValue = 0;
        foreach (GameObject gameObject in stages)
        {
            stageValue++;
            Transform[] stagesObjects = gameObject.GetComponentsInChildren<Transform>();

            foreach (Transform transform in stagesObjects)
            {
                if (transform.name == "StageNum")
                {
                    TextMeshProUGUI testMesh = transform.GetComponent<TextMeshProUGUI>();
                    testMesh.text = "Stage" + stageValue;
                }
                else if (transform.name == "Button")
                {
                    Button button = transform.GetComponent<Button>();

                    int nullNum = stageValue;

                    button.onClick.AddListener(() =>
                    {
                        int stageNum = GameManager.Instance.StageCheck(nullNum);
                        OnStage1(stageNum);
                    });

                    Image images = transform.GetComponent<Image>();
                    
                    if(_images.Length > stageValue - 1)
                        images.sprite = _images[stageValue - 1];
                }
                else if (transform.name == "Lock")
                {
                    Image images = transform.GetComponent<Image>();
                    Color createColor = new Color(255, 255, 255, 0 );
                    images.color = createColor;
                }

            }
        }
    }
}
