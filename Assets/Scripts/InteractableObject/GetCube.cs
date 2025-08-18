using UnityEngine;

public class GetCube : MonoBehaviour, IInteractable
{
    private bool isHeld = false;
    private Rigidbody rb;
    private Vector3 originScale;
    private float rotationSpeed = 100f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originScale = transform.localScale;
    }
    void Update()
    {
            // 큐브를 들고 있는 동안에만 회전
            if (isHeld)
            {
                // 현재 회전에 y축 회전값을 계속 더해줍니다.
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
    }

    public string GetPrompt()
    {
        if (isHeld)
        {
            return "F를 누르면 큐브를 놓습니다.";
        }
        else
        {
            return "F를 누르면 큐브를 듭니다.";
        }
    }
    public void Interact()
    {
        if (!isHeld)
        {
            // 큐브 들기
            // 카메라컨테이너를 가져옴
            Transform playerCamera = CharacterManager.Instance.Player.controller.cameraContainer;

            // 큐브의 크기를 0.2로 통일합니다.
            transform.localScale = Vector3.one * 0.3f;
            // 큐브의 부모를 카메라로 설정합니다.
            transform.SetParent(playerCamera);

            // 큐브의 로컬 위치를 카메라를 기준으로 조정합니다.
            // x: 좌우, y: 상하, z: 앞뒤 (값은 직접 조정해 보세요)
            Vector3 pickUpPosition = new Vector3(0f, 0.5f, 1.5f);
            transform.localPosition = pickUpPosition;

            // 큐브의 회전을 카메라의 회전과 동일하게 맞춥니다.
            transform.localRotation = Quaternion.identity;



            if (rb != null)
            {
                rb.isKinematic = true; // 물리 비활성화
            }
            isHeld = true;
        }
        else
        {
            // 큐브 놓기
            transform.SetParent(null);
            transform.localScale = originScale;
            if (rb != null)
            {
                rb.isKinematic = false; // 물리 활성화
            }
            isHeld = false;
        }
    }
}