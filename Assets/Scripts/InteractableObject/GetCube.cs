using UnityEngine;

public class GetCube : MonoBehaviour, IInteractable
{
    private bool isHeld = false;
    private Rigidbody rb;
    private Transform playerHoldPoint;
    private Vector3 originScale;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originScale = transform.localScale;
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
        if (!isHeld) // 큐브 들기
        {
            //부모를 가지기전에 먼저 크기변환해줌
            transform.localScale = Vector3.one * 0.2f;
            playerHoldPoint = CharacterManager.Instance.Player.controller.holdPoint; // 플레이어 컨트롤러에서 HoldPoint를 가져와서 부모로 설정
            transform.SetParent(playerHoldPoint);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(0, 0, 0);

            if (rb != null)
            {
                rb.isKinematic = true; // 물리 비활성화
            }
            isHeld = true;
        }
        else // 큐브 놓기
        {
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