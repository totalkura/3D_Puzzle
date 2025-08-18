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
            // ť�긦 ��� �ִ� ���ȿ��� ȸ��
            if (isHeld)
            {
                // ���� ȸ���� y�� ȸ������ ��� �����ݴϴ�.
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
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
        if (!isHeld)
        {
            // ť�� ���
            // ī�޶������̳ʸ� ������
            Transform playerCamera = CharacterManager.Instance.Player.controller.cameraContainer;

            // ť���� ũ�⸦ 0.2�� �����մϴ�.
            transform.localScale = Vector3.one * 0.3f;
            // ť���� �θ� ī�޶�� �����մϴ�.
            transform.SetParent(playerCamera);

            // ť���� ���� ��ġ�� ī�޶� �������� �����մϴ�.
            // x: �¿�, y: ����, z: �յ� (���� ���� ������ ������)
            Vector3 pickUpPosition = new Vector3(0f, 0.5f, 1.5f);
            transform.localPosition = pickUpPosition;

            // ť���� ȸ���� ī�޶��� ȸ���� �����ϰ� ����ϴ�.
            transform.localRotation = Quaternion.identity;



            if (rb != null)
            {
                rb.isKinematic = true; // ���� ��Ȱ��ȭ
            }
            isHeld = true;
        }
        else
        {
            // ť�� ����
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