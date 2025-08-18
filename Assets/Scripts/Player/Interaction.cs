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

    private IInteractable curInteractable; //���� �������̾�����, ������ ������, ť����, +@ 

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

            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); // ȭ�����߾ӿ� ���̽�
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
        promptText.gameObject.SetActive(true);         //UI Ȱ��ȭ
        promptText.text = curInteractable.GetPrompt(); //�ش������Ʈ�� ����(string��)������
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
