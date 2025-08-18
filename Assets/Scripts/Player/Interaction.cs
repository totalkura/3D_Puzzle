using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObject;
    public GameObject[] itemDatas;

    private IInteractable curInteractable; //원래 아이템이었지만, 지금은 문열기, 큐브들기, +@ 

    public TextMeshProUGUI promptText;
    private Camera _camera;

    private void Awake()
    {
        GameObject foundObj = GameObject.Find("PromptText");
        promptText = foundObj.GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); // 화면정중앙에 레이쏨
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromtText();
                }
            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromtText()
    {
        promptText.gameObject.SetActive(true);         //UI 활성화
        promptText.text = curInteractable.GetPrompt(); //해당오브젝트의 설명(string값)가져옴
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.Interact();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
