using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private PauseMenuManager EscapePanel;

    [Header("Movement")]
    public float walkSpeed; //3
    public float dashSpeed; //10
    public float curMoveSpeed;
    public float jumpPower; //500
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;
    public float canJumpRay;//1.5
    public bool isMove;
    public bool isDash = false;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot; //인풋엑션에서 받아오는 마우스의 델타값
    public float lookSensitivity; // 민감도
    private Vector2 mouseDelta; //여기 델타값을 넣어줌
    public bool canLook = true;
    public bool characterGetItem;

    public Action inventory;
    private Rigidbody _rigidbody;
    public bool isPlay;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        isPlay = true;
        curMoveSpeed = walkSpeed;
    }

    void Start()
    {
        EscapePanel = FindObjectOfType<PauseMenuManager>();
        if(EscapePanel != null)
        EscapePanel.gameObject.SetActive(false); // 게임 시작시 일시정지 패널 비활성화
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate() // 리지드바디나 물리연산은 픽스드업데이트
    {
        if(isMove && isPlay)
            move();
    }

    private void LateUpdate()
    {
        if (canLook && isPlay)
        {
            CameraLook();
        }
    }

    void move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;

        if (isDash && CharacterManager.Instance.Player.condition.HasStamina(10 * Time.deltaTime))
        {
            SoundManager.instance.PlayOther(SoundManager.playerActive.work,2.2f);
            curMoveSpeed = dashSpeed;
        }
        else
        {
            SoundManager.instance.PlayOther(SoundManager.playerActive.work,1.4f);
            curMoveSpeed = walkSpeed;
            isDash = false;
        }
            dir *= curMoveSpeed;
        dir.y = _rigidbody.velocity.y; //점프를했을때만

        _rigidbody.velocity = dir;

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            isMove = true;
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isMove = false;
            curMovementInput = Vector2.zero;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            isDash = true; // 대쉬 시작
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isDash = false;
        }
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            
            Debug.Log("Escape Key Pressed");
            isPlay = !isPlay;
            Time.timeScale = isPlay ? 1f : 0f;

            //UI 
            if (EscapePanel != null)
            EscapePanel.gameObject.SetActive(!isPlay);

            else
            Debug.LogWarning("EscapePanel not found!");

            ToggleCursor(); //커서 토글
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
        if(isPlay)
        if (context.phase == InputActionPhase.Started && isGrounded() && CharacterManager.Instance.Player.condition.HasStaminaForJump(20))
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

    public void ToggleCursor() //일시정지
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
    public void OnShootLeft(InputAction.CallbackContext context)
    {
        if (context.performed) GetComponent<PortalShooter>().ShootPortal(true);
    }

    public void OnShootRight(InputAction.CallbackContext context)
    {
        if (context.performed) GetComponent<PortalShooter>().ShootPortal(false);
    }


}
