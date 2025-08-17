using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal linkedPortal; // 연결된 다른 포털

    // 연결되지 않았을 때의 머티리얼
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

    // 포털의 색상을 설정하는 함수 (PortalShooter에서 호출)
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
        // 1. 위치 변환
        Vector3 localPos = transform.InverseTransformPoint(player.transform.position);
        Vector3 newPos = linkedPortal.transform.TransformPoint(localPos);
        player.position = newPos;

        //  2. 회전 변환
        // 나가는 포털의 회전을 그대로 가져온 뒤, Y축으로 180도 회전
        player.transform.rotation = linkedPortal.transform.rotation * Quaternion.Euler(0, 180, 0);


        // 3. 속도 변환
        Vector3 localVel = transform.InverseTransformDirection(player.velocity);
        player.velocity = linkedPortal.transform.TransformDirection(localVel);

        // 포털 비활성화
        gameObject.SetActive(false);
        linkedPortal.gameObject.SetActive(false);
    }
}