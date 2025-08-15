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
    private float camCurXRot; //��ǲ���ǿ��� �޾ƿ��� ���콺�� ��Ÿ��
    public float lookSensitivity; // �ΰ���
    private Vector2 mouseDelta; //���� ��Ÿ���� �־���
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
       // // Main Camera ������Ʈ�� �����ɴϴ�.
       //_camera = Camera.main;

       // // ��ũ������ ���콺 ��ġ���� Ray�� ����ϴ�.
       // Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
       // float rayDistance = 100f;

       // // Scene �信 �������� �׸��ϴ� (������)
       // Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.blue);

       // RaycastHit hit;
       // if (Physics.Raycast(ray, out hit, rayDistance))
       // {
       //     if (hit.collider.gameObject.name == "DoorComputer") 
       //     {
       //         Debug.Log("���콺 Ŀ���� " + hit.collider.gameObject.name + "�� ����ŵ�ϴ�.");
       //         //MapManager.Instance.GetDoor(hit.collider.gameObject);
       //     }


       //    // Debug.Log("���콺 Ŀ���� " + hit.collider.gameObject.name + "�� ����ŵ�ϴ�.");
       // }
    }
    // Update is called once per frame
    void FixedUpdate() // ������ٵ� ���������� �Ƚ��������Ʈ
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
        dir.y = _rigidbody.velocity.y; //��������������

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
        // Shift Ű�� ������ �ִ� ����(Performed)
        if (context.phase == InputActionPhase.Performed)
        {
            curMoveSpeed = dashSpeed; // �̵� �ӵ��� �޸��� �ӵ��� ����
        }
        // Shift Ű���� ���� ���� ����(Canceled)
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMoveSpeed = walkSpeed; // �̵� �ӵ��� ���� �ӵ��� �ǵ���
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
        if (context.phase == InputActionPhase.Started && isGrounded())
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

    void ToggleCursor() //�Ͻ�����
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
