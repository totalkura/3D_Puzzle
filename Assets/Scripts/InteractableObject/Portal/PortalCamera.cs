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

        // 렌더 텍스처 동적 생성
        if (portalCam.targetTexture != null)
        {
            portalCam.targetTexture.Release();
        }
        portalCam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        myPortal.GetComponentInChildren<Renderer>().material.mainTexture = portalCam.targetTexture;
    }

    void LateUpdate()
    {
        // 1. 연결된 포털이 없으면 카메라를 비활성화
        if (myPortal.linkedPortal == null)
        {
            portalCam.enabled = false;
            return;
        }

        // 2. 연결된 포털 정보 업데이트 및 카메라 활성화
        if (linkedPortal == null)
        {
            linkedPortal = myPortal.linkedPortal;
        }
        if (!portalCam.enabled)
        {
            portalCam.enabled = true;
        }

        // 3. 포털 간의 상대적 위치와 회전을 계산
        Transform portalTransform = myPortal.transform;
        Transform linkedTransform = linkedPortal.transform;

        // 4. 행렬을 이용한 위치/회전 변환 (핵심)
        Matrix4x4 portalToLinked = linkedTransform.localToWorldMatrix * Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 180, 0), Vector3.one) * portalTransform.worldToLocalMatrix;

        // 5. 최종 위치와 회전 적용
        Matrix4x4 finalMatrix = portalToLinked * playerCamera.localToWorldMatrix;
        transform.SetPositionAndRotation(finalMatrix.GetColumn(3), finalMatrix.rotation);
    }
}