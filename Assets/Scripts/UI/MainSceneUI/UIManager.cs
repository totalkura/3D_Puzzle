using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class UIManager : MonoBehaviour
{

    // ========== SetActive ===================

    [Header("메인 메뉴")]
    public GameObject StageScene;
    public GameObject MainButton;
    public GameObject BackSpace;
    public GameObject OptionScene;
    public GameObject Option;

    [Header("페이드 애니메이터")]
    public Animator animator;
    public float transitionTime;


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

    public void OnNewStageSelector(int sceneNum)
    {
        GameManager.Instance.StageCheck(sceneNum);
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
        BackSpace.SetActive(false);
        StageScene.SetActive(false);
        animator.SetTrigger("FadeOut");
        MainButton.SetActive(true);

        if (StageScene != null)
        {
            BackSpace.SetActive(true);
        }
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

    //여기 아래부터 테스트

    [Header("TEST")]
    public GameObject thisObjects;
    public GameObject stage;

    public Sprite[] _images;
    public List<GameObject> stages;

    public int maxStage;

    private void Start()
    {
        maxStage = 9;
        Tests();
        StageScene.SetActive(false);
    }

    
    public void Tests()        
    {
        _images = Resources.LoadAll<Sprite>("Images");
        thisObjects = Resources.Load<GameObject>("Prefabs\\Stage1");
        stage = GameObject.Find("Stages");

        stages = new List<GameObject>();

        for (int i = 0; i < maxStage; i++)
        {
           
            GameObject tesss = Instantiate(thisObjects, stage.transform);
            tesss.name = "Stage" + (i+1);
            stages.Add(tesss);
        }

        int a = 0;

        foreach (GameObject gameObject in stages)
        {
            a++;
            Transform[] stagesObjects = gameObject.GetComponentsInChildren<Transform>();

            foreach (Transform transform in stagesObjects)
            {
                if (transform.name == "StageNum")
                {
                    TextMeshProUGUI testMesh = transform.GetComponent<TextMeshProUGUI>();
                    testMesh.text = "Stage" + a;
                }
                else if (transform.name == "Lock")
                {
                    Image images = transform.GetComponent<Image>();
                    //images.color = Color.
                    if (a == 1)
                        images.sprite = _images[a - 1];
                    else
                    {
                        Color randomColor = new Color(
                                 255, // R
                                 255, // G
                                 255,  // B
                                 255
                                    );

                        images.color = randomColor;
                    }
                    
                }
            }
        }

        /*
        transforms = stage.GetComponentsInChildren<Transform>();

        stages = new List<GameObject>();

        foreach (Transform transforms in transforms)
        {
            if (transforms.name.StartsWith("Stage"))
            {
                if (transforms.name == "Stages" || transforms.name == "StageNum") 
                    continue;
                stages.Add(transforms.gameObject);
            }
        }
        */


    }
}
