using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public LayerMask layerMask;
    public LayerMask layer;
    public LayerMask portals;

    private Rigidbody objectRigidbody;
    private Transform getObjects;
    private float rayDistance;
    bool checkObject;
    private float throwForce = 10f;

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
        portals = LayerMask.GetMask("Portal");
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
            else if(Physics.Raycast(ray, out hit, 7.0f, portals))
            {
                if (hit.transform.name == "ColliderPlane_A" || hit.transform.name == "ColliderPlane_B")
                    PortalSettings(0, 1);
                else if ((hit.transform.name == "ColliderPlane_C" || hit.transform.name == "ColliderPlane_D") && PortalManager.Instance.bluePortal)
                    PortalSettings(2, 3);
                else if (hit.transform.name == "ColliderPlane_E" || hit.transform.name == "ColliderPlane_F")
                    PortalSettings(4, 5);

            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
                PortalSettings(-1, -1);
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

    public void OnShoot(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            ThrowCube();
        }
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

    private void ThrowCube()
    {
        if (!checkObject) return;
        objectRigidbody.useGravity = true;

        if (objectRigidbody != null)
        {
            objectRigidbody.isKinematic = false; // 물리 활성화
            objectRigidbody.velocity = Vector3.zero; // 기존 속도 초기화
            objectRigidbody.rotation = Quaternion.identity;
            objectRigidbody.angularVelocity = Vector3.zero; // 회전 속도 초기화

            // 큐브를 플레이어의 시선 방향으로 던집니다.
            checkObject = false;
            objectRigidbody.AddForce(CharacterManager.Instance.Player.controller.cameraContainer.forward * throwForce, ForceMode.Impulse);
        }
    }

    private void PortalSettings(int firstPortalNum, int secondPortalNum)
    {
        for (int i = 0; i < PortalManager.Instance.portals.Length; i++)
        {
            if(i == firstPortalNum )
                PortalManager.Instance.portals[firstPortalNum].SetActive(true);
            else if(i == secondPortalNum)
                PortalManager.Instance.portals[secondPortalNum].SetActive(true);
            else
                PortalManager.Instance.portals[i].SetActive(false);
        }
        
        
    }
}
