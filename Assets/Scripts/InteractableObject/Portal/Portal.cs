using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal linkedPortal; // ����� �ٸ� ����

    // ������� �ʾ��� ���� ��Ƽ����
    public Material blueUnlinkedMaterial;
    public Material orangeUnlinkedMaterial;

    private Material originalMaterial;
    private Renderer quadRenderer;

    void Awake()
    {
        quadRenderer = GetComponentInChildren<Renderer>();
        if (quadRenderer != null)
        {
            originalMaterial = quadRenderer.material;
        }
    }

    // ������ ������ �����ϴ� �Լ� (PortalShooter���� ȣ��)
    public void SetPortalColor(bool isBlue)
    {
        if (quadRenderer != null)
        {
            if (isBlue)
            {
                quadRenderer.material = blueUnlinkedMaterial;
            }
            else
            {
                quadRenderer.material = orangeUnlinkedMaterial;
            }
        }
    }

    void Update()
    {
        if (quadRenderer != null)
        {
            if (linkedPortal != null)
            {
                quadRenderer.material = originalMaterial;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || linkedPortal == null)
        {
            return;
        }

        Rigidbody playerRb = other.GetComponent<Rigidbody>();
        if (playerRb != null)
        {
            Teleport(playerRb);
        }
    }

    private void Teleport(Rigidbody player)
    {
        // 1. ��ġ ��ȯ
        Vector3 localPos = transform.InverseTransformPoint(player.transform.position);
        Vector3 newPos = linkedPortal.transform.TransformPoint(localPos);
        player.position = newPos;

        //  2. ȸ�� ��ȯ
        // ������ ������ ȸ���� �״�� ������ ��, Y������ 180�� ȸ��
        player.transform.rotation = linkedPortal.transform.rotation * Quaternion.Euler(0, 180, 0);


        // 3. �ӵ� ��ȯ
        Vector3 localVel = transform.InverseTransformDirection(player.velocity);
        player.velocity = linkedPortal.transform.TransformDirection(localVel);

        // ���� ��Ȱ��ȭ
        gameObject.SetActive(false);
        linkedPortal.gameObject.SetActive(false);
    }
}