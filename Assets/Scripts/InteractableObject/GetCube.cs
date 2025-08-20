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
        // ť�긦 ��� �ִ� ���ȿ��� ȸ��
        if (isHeld)
        {
            // ���� ȸ���� y�� ȸ������ ��� �����ݴϴ�.
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
            return "F�� ������ ť�긦 �����ϴ�. Ȥ�� ���콺Ŭ������ ���������ֽ��ϴ�.";
        }
        else
        {
            return "F�� ������ ť�긦 ��ϴ�.";
        }
    }

    private void ThrowCube()
    {
        if (!isHeld) return;
        // ť���� �θ� ���� ����
        rb.useGravity = true;
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
        rb.isKinematic = false; // ���� Ȱ��ȭ
        col.isTrigger = false;

        // ���� �ʱ�ȭ
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
        // ť�긦 ��� �ְ�, �浹�� ������Ʈ�� �÷��̾ �ƴ� ��
        if (isHeld && !other.CompareTag("Player"))
        {
            // ť�긦 �����ϴ�.
            DropCube();
        }
    }
    void PickUpCube() // ť�� ���
    {
        transform.localScale = originScale;
        rb.useGravity = false;
        // ť���� ũ�⸦ 0.3���� �����մϴ�.
        ResetCube();
        rb.angularVelocity = Vector3.zero; // ȸ�� �ӵ� �ʱ�ȭ
        if (rb != null)
        {
            rb.isKinematic = true; // ���� ��Ȱ��ȭ
            col.isTrigger = true; // Ʈ���ŷ� ����
        }
        isHeld = true;
    }

    void DropCube()
    {
        // ť�� ����
        rb.useGravity = true;
        transform.SetParent(null);
        transform.localScale = originScale;
        transform.localRotation = Quaternion.identity;
        if (rb != null)
        {
            rb.isKinematic = false; // ���� Ȱ��ȭ
            col.isTrigger = false; // Ʈ���ŷ� ����
        }
        isHeld = false;
    }

    void ResetCube()
    { // ī�޶������̳ʸ� ������
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