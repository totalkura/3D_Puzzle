using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PortalCamera : MonoBehaviour
{
    private Transform playerCamera;
    private Portal myPortal;
    private Portal linkedPortal;

    private Camera portalCam;

    void Awake()
    {
        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            playerCamera = mainCam.transform;
        }

        myPortal = GetComponentInParent<Portal>();
        portalCam = GetComponent<Camera>();

        // ���� �ؽ�ó ���� ����
        if (portalCam.targetTexture != null)
        {
            portalCam.targetTexture.Release();
        }
        portalCam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        myPortal.GetComponentInChildren<Renderer>().material.mainTexture = portalCam.targetTexture;
    }

    void LateUpdate()
    {
        // 1. ����� ������ ������ ī�޶� ��Ȱ��ȭ
        if (myPortal.linkedPortal == null)
        {
            portalCam.enabled = false;
            return;
        }

        // 2. ����� ���� ���� ������Ʈ �� ī�޶� Ȱ��ȭ
        if (linkedPortal == null)
        {
            linkedPortal = myPortal.linkedPortal;
        }
        if (!portalCam.enabled)
        {
            portalCam.enabled = true;
        }

        // 3. ���� ���� ����� ��ġ�� ȸ���� ���
        Transform portalTransform = myPortal.transform;
        Transform linkedTransform = linkedPortal.transform;

        // 4. ����� �̿��� ��ġ/ȸ�� ��ȯ (�ٽ�)
        Matrix4x4 portalToLinked = linkedTransform.localToWorldMatrix * Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 180, 0), Vector3.one) * portalTransform.worldToLocalMatrix;

        // 5. ���� ��ġ�� ȸ�� ����
        Matrix4x4 finalMatrix = portalToLinked * playerCamera.localToWorldMatrix;
        transform.SetPositionAndRotation(finalMatrix.GetColumn(3), finalMatrix.rotation);
    }
}