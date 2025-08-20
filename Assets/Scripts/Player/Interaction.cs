using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public LayerMask layerMask;
    public LayerMask layer;

    private Rigidbody objectRigidbody;
    private Transform getObjects;
    private float rayDistance;
    bool checkObject;

    public GameObject curInteractGameObject;

    private IInteractable curInteractable; //원래 아이템이었지만, 지금은 문열기, 큐브들기, +@ 

    public TextMeshProUGUI promptText;
    private Camera _camera;

    private void Awake()
    {
        rayDistance = 2f;
        GameObject foundObj = GameObject.Find("PromptText");
        promptText = foundObj.GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        
        layer = LayerMask.GetMask("Default");
        _camera = Camera.main;
    }

    void Update()
    {
        if (Time.time - lastCheckTime > checkRate && CharacterManager.Instance.Player.controller.isPlay)
        {

            lastCheckTime = Time.time;

            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); // 화면정중앙에 레이쏨
            Debug.DrawRay(ray.origin, ray.direction * 2f, Color.green);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayDistance, layerMask))
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

    private void FixedUpdate()
    {

        if (checkObject)
        {
            objectRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

            Vector3 holdPosition = _camera.transform.position + _camera.transform.forward * rayDistance;
            Vector3 cubeSize = Vector3.Scale(getObjects.GetComponent<BoxCollider>().size*0.4f, getObjects.lossyScale);

            if (!Physics.CheckBox(holdPosition, cubeSize,getObjects.rotation,layer))
            {
                objectRigidbody.MovePosition(holdPosition);
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
            if (curInteractGameObject.transform.name == "Cube")
            {
                checkObject = !checkObject;

                if (checkObject)
                {
                    PickUpObject();
                    objectRigidbody.useGravity = false;
                }
                else
                {
                    objectRigidbody.useGravity = true;
                    getObjects = null;
                }
                return;
            }

            curInteractable.Interact();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);

        }
    }


    public void PickUpObject()
    {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, layerMask))
        {
            getObjects = hit.transform;

            objectRigidbody = getObjects.GetComponent<Rigidbody>();
        }
    }

}
