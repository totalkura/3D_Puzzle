using UnityEngine;

[System.Serializable]
public class PortalPair
{
    public Transform portalA;
    public Transform portalB;

    public Camera cameraA;
    public Camera cameraB;

    public Material cameraMatA;
    public Material cameraMatB;
}

public class PortalManager : MonoBehaviour
{
    public static PortalManager Instance { get; private set; }
    public Transform playerCamera;  // ���� Main Camera
    public PortalPair[] portalPairs;

    //��Ż ���
    public GameObject[] portals;
    public GameObject[] cameras;

    public bool setSwitchOne;
    public bool setSwitchTwo;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        foreach (var pair in portalPairs)
        {
            playerCamera = Camera.main.transform;
            // RenderTexture ����
            var rtA = new RenderTexture(Screen.width, Screen.height, 24);
            var rtB = new RenderTexture(Screen.width, Screen.height, 24);

            pair.cameraA.targetTexture = rtA;
            pair.cameraMatB.mainTexture = rtA;

            pair.cameraB.targetTexture = rtB;
            pair.cameraMatA.mainTexture = rtB;

            // Teleporter ����
            var teleA = pair.portalA.GetComponentInChildren<PortalTeleporter>();
            var teleB = pair.portalB.GetComponentInChildren<PortalTeleporter>();

            teleA.reciever = pair.portalB;
            teleB.reciever = pair.portalA;
        }

        foreach (GameObject pair in portals)
            pair.gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        foreach (var pair in portalPairs)
        {
            UpdatePortalCamera(pair.portalA, pair.portalB, pair.cameraA);
            UpdatePortalCamera(pair.portalB, pair.portalA, pair.cameraB);
        }
    }

    void UpdatePortalCamera(Transform portal, Transform otherPortal, Camera portalCam)
    {
        //�÷��̾� ī�޶�� ��� ��Ż�� ����
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
        portalCam.transform.position = portal.position + playerOffsetFromPortal;

        //ȸ�� ���
        float angularDiff = Quaternion.Angle(portal.rotation, otherPortal.rotation);
        Quaternion portalRotDiff = Quaternion.AngleAxis(angularDiff, Vector3.up);
        Vector3 newCamDir = portalRotDiff * playerCamera.forward;

        portalCam.transform.rotation = Quaternion.LookRotation(newCamDir, Vector3.up);

    }

}
