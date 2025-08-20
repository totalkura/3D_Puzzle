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
    private float camCurXRot; //��ǲ���ǿ��� �޾ƿ��� ���콺�� ��Ÿ��
    public float lookSensitivity; // �ΰ���
    private Vector2 mouseDelta; //���� ��Ÿ���� �־���
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
        EscapePanel.gameObject.SetActive(false); // ���� ���۽� �Ͻ����� �г� ��Ȱ��ȭ
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate() // ������ٵ� ���������� �Ƚ��������Ʈ
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
        dir.y = _rigidbody.velocity.y; //��������������

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
            isDash = true; // �뽬 ����
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

            ToggleCursor(); //Ŀ�� ���
        }
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity; //���콺�� �¿�� �����̸� ���콺��Ÿ�� X���� �����̴µ� ĳ���Ͱ� �¿�� �����̰ԵǷ��� �� ���� Y���� �����־�� �¿�� ������ �����ι޴°�. ���콺��Ÿ������ x���� y��, y���� x�� �־��־�� ������ �츮�� ���ϴ� ȿ���� �� �� �ִ�.
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // �ּҰ����� ������ �ּҰ���, �ִ밪���� ũ�� �ִ밪�� ��ȯ��
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0); //���̳ʽ������������� ���콺��Ÿ���� y���� ���콺�� �Ʒ��� �巡���ϰԵǸ� ���̳ʽ��� �ǰԵȴ�. �����̼��� ���� �������� ���̳ʽ��ΰ���ö󰡰� �÷����ΰ��� �������Եȴ�. ������ �츮�� �����ϴ°Ͱ� ���̴� ���� �ݴ밡 �ȴ�.


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
          _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse); //���������������ִ� ���޽�
        }

    }

    bool isGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward*0.2f) + (transform.up*0.01f), Vector3.down),//�׶��带 �������ϴ� ��찡 ���ܼ� ��¦ ���� �÷���
            new Ray(transform.position + (-transform.forward*0.2f) + (transform.up*0.01f), Vector3.down),
            new Ray(transform.position + (transform.right*0.2f) + (transform.up*0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right*0.2f) + (transform.up*0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], canJumpRay, groundLayerMask)) //�׶��巹�̾��ũ�� �ش��ϴ°͸� ����
            {
                return true;
            }
        }
        return false;
    }

    public void ToggleCursor() //�Ͻ�����
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
