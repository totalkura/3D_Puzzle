using UnityEngine;

public class GetCube : MonoBehaviour, IInteractable
{
    private bool isHeld = false;
    private Rigidbody rb;
    private Vector3 originScale;
    private float rotationSpeed = 100f;
    private float throwForce = 10f;

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
            if (Input.GetMouseButtonDown(0))
            {
                ThrowObject();
            }
        }
    }

    public string GetPrompt()
    {
        if (isHeld)
        {
            return "F�� ������ ť�긦 �����ϴ�. Ȥ�� ���콺Ŭ������ ���������ֽ��ϴ�.";
        }
        else
        {
            return "F�� ������ ť�긦 ��ϴ�.";
        }
    }

    private void ThrowObject()
    {
        if (!isHeld) return;
        // ť���� �θ� ���� ����
        transform.SetParent(null);

        if (rb != null)
        {
            rb.isKinematic = false; // ���� Ȱ��ȭ
            rb.velocity = Vector3.zero; // ���� �ӵ� �ʱ�ȭ
            transform.rotation = Quaternion.identity;
            rb.angularVelocity = Vector3.zero; // ȸ�� �ӵ� �ʱ�ȭ

            // ť�긦 �÷��̾��� �ü� �������� �����ϴ�.
            rb.AddForce(CharacterManager.Instance.Player.controller.cameraContainer.forward * throwForce, ForceMode.Impulse);
        }

        // ���� �ʱ�ȭ
        transform.localScale = originScale;
        isHeld = false;
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
            transform.localRotation = Quaternion.identity;
            if (rb != null)
            {
                rb.isKinematic = false; // ���� Ȱ��ȭ
            }
            isHeld = false;
        }
    }
}