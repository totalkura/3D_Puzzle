using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    public float walkSpeed; //3
    public float dashSpeed; //10
    public float curMoveSpeed;
    public float jumpPower; //500
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;
    public float canJumpRay;//1.5

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot; //인풋엑션에서 받아오는 마우스의 델타값
    public float lookSensitivity; // 민감도
    private Vector2 mouseDelta; //여기 델타값을 넣어줌
    public bool canLook = true;

    public Action inventory;
    private Rigidbody _rigidbody;
    //Camera _camera;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        curMoveSpeed = walkSpeed;
    }


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
       // // Main Camera 컴포넌트를 가져옵니다.
       //_camera = Camera.main;

       // // 스크린상의 마우스 위치에서 Ray를 만듭니다.
       // Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
       // float rayDistance = 100f;

       // // Scene 뷰에 레이저를 그립니다 (디버깅용)
       // Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.blue);

       // RaycastHit hit;
       // if (Physics.Raycast(ray, out hit, rayDistance))
       // {
       //     if (hit.collider.gameObject.name == "DoorComputer") 
       //     {
       //         Debug.Log("마우스 커서가 " + hit.collider.gameObject.name + "를 가리킵니다.");
       //         //MapManager.Instance.GetDoor(hit.collider.gameObject);
       //     }


       //    // Debug.Log("마우스 커서가 " + hit.collider.gameObject.name + "를 가리킵니다.");
       // }
    }
    // Update is called once per frame
    void FixedUpdate() // 리지드바디나 물리연산은 픽스드업데이트
    {
        move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    void move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= curMoveSpeed;
        dir.y = _rigidbody.velocity.y; //점프를했을때만

        _rigidbody.velocity = dir;

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        // Shift 키를 누르고 있는 동안(Performed)
        if (context.phase == InputActionPhase.Performed)
        {
            curMoveSpeed = dashSpeed; // 이동 속도를 달리기 속도로 변경
        }
        // Shift 키에서 손을 떼는 순간(Canceled)
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMoveSpeed = walkSpeed; // 이동 속도를 원래 속도로 되돌림
        }
    }


    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity; //마우스를 좌우로 움직이면 마우스델타의 X값이 움직이는데 캐릭터가 좌우로 움직이게되려면 그 축을 Y축을 돌려주어야 좌우로 움직임 실제로받는값. 마우스델타값에서 x값은 y에, y값은 x에 넣어주어야 실제로 우리가 원하는 효과를 얻어낼 수 있다.
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // 최소값보다 작으면 최소값을, 최대값보다 크면 최대값을 반환함
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0); //마이너스를붙인이유는 마우스델타값의 y값이 마우스를 아래로 드래그하게되면 마이너스가 되게된다. 로테이션을 직접 돌려보면 마이너스로가면올라가고 플러스로가면 밑을보게된다. 실제로 우리가 동작하는것과 보이는 것이 반대가 된다.

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && isGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse); //순간적으로힘을주는 임펄스
        }

    }

    bool isGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward*0.2f) + (transform.up*0.01f), Vector3.down),//그라운드를 인지못하는 경우가 생겨서 살짝 위로 올려줌
            new Ray(transform.position + (-transform.forward*0.2f) + (transform.up*0.01f), Vector3.down),
            new Ray(transform.position + (transform.right*0.2f) + (transform.up*0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right*0.2f) + (transform.up*0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], canJumpRay, groundLayerMask)) //그라운드레이어마스크에 해당하는것만 검출
            {
                return true;
            }
        }
        return false;
    }

    void ToggleCursor() //일시정지
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
