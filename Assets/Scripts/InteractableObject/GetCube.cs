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
            return "F�� ������ ť�긦 �����ϴ�.";
        }
        else
        {
            return "F�� ������ ť�긦 ��ϴ�.";
        }
    }

    public void Interact()
    {
        if (!isHeld) // ť�� ���
        {
            //�θ� ���������� ���� ũ�⺯ȯ����
            transform.localScale = Vector3.one * 0.2f;
            playerHoldPoint = CharacterManager.Instance.Player.controller.holdPoint; // �÷��̾� ��Ʈ�ѷ����� HoldPoint�� �����ͼ� �θ�� ����
            transform.SetParent(playerHoldPoint);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(0, 0, 0);

            if (rb != null)
            {
                rb.isKinematic = true; // ���� ��Ȱ��ȭ
            }
            isHeld = true;
        }
        else // ť�� ����
        {
            transform.SetParent(null);
            transform.localScale = originScale;
            if (rb != null)
            {
                rb.isKinematic = false; // ���� Ȱ��ȭ
            }
            isHeld = false;
        }
    }
}