using UnityEngine;

public class GetCube : MonoBehaviour, IInteractable
{
    private bool isHeld = false;
    private Rigidbody rb;
    private Collider col;
    private Vector3 originScale;
    private float rotationSpeed = 100f;
    private float throwForce = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        originScale = transform.localScale;
    }
    void Update()
    {
        // 큐브를 들고 있는 동안에만 회전
        if (isHeld)
        {
            // 현재 회전에 y축 회전값을 계속 더해줍니다.
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            if (Input.GetMouseButtonDown(0))
            {
                ThrowCube();
            }

            if (Input.GetMouseButtonDown(1))
            {
                ResetCube();
            }
        }
    }

    public string GetPrompt()
    {
        if (isHeld)
        {
            return "F를 누르면 큐브를 놓습니다. 혹은 마우스클릭으로 던질수도있습니다.";
        }
        else
        {
            return "F를 누르면 큐브를 듭니다.";
        }
    }

    private void ThrowCube()
    {
        if (!isHeld) return;
        // 큐브의 부모 관계 해제
        rb.useGravity = true;
        transform.SetParent(null);

        if (rb != null)
        {
            rb.isKinematic = false; // 물리 활성화
            rb.velocity = Vector3.zero; // 기존 속도 초기화
            transform.rotation = Quaternion.identity;
            rb.angularVelocity = Vector3.zero; // 회전 속도 초기화

            // 큐브를 플레이어의 시선 방향으로 던집니다.
            rb.AddForce(CharacterManager.Instance.Player.controller.cameraContainer.forward * throwForce, ForceMode.Impulse);
        }
        rb.isKinematic = false; // 물리 활성화
        col.isTrigger = false;

        // 상태 초기화
        transform.localScale = originScale;
        isHeld = false;
    }

    public void Interact()
    {
        if (!isHeld)
        {
            PickUpCube();
        }
        else
        {
            DropCube();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        // 큐브를 들고 있고, 충돌한 오브젝트가 플레이어가 아닐 때
        if (isHeld && !other.CompareTag("Player"))
        {
            // 큐브를 놓습니다.
            DropCube();
        }
    }
    void PickUpCube() // 큐브 들기
    {
        transform.localScale = originScale;
        rb.useGravity = false;
        // 큐브의 크기를 0.3으로 통일합니다.
        ResetCube();
        rb.angularVelocity = Vector3.zero; // 회전 속도 초기화
        if (rb != null)
        {
            rb.isKinematic = true; // 물리 비활성화
            col.isTrigger = true; // 트리거로 설정
        }
        isHeld = true;
    }

    void DropCube()
    {
        // 큐브 놓기
        rb.useGravity = true;
        transform.SetParent(null);
        transform.localScale = originScale;
        transform.localRotation = Quaternion.identity;
        if (rb != null)
        {
            rb.isKinematic = false; // 물리 활성화
            col.isTrigger = false; // 트리거로 설정
        }
        isHeld = false;
    }

    void ResetCube()
    { // 카메라컨테이너를 가져옴
        transform.SetParent(null);
        transform.localScale = originScale;
        transform.localScale = Vector3.one * 0.6f;
        Transform HoldPoint = CharacterManager.Instance.Player.controller.cameraContainer;

        transform.SetParent(HoldPoint);

        Vector3 pickUpPosition = new Vector3(0f, 0.5f, 1.5f);
        transform.localPosition = pickUpPosition;
        transform.localRotation = Quaternion.identity;
    }
}