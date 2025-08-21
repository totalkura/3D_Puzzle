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
    public Transform playerCamera;  // 보통 Main Camera
    public PortalPair[] portalPairs;

    //포탈 담기
    public GameObject[] cameras;
    public GameObject[] portals;

    public bool setSwitchOne;
    public bool setSwitchTwo;

    public GameObject lastPortal;
    public Door door;

    private RenderTexture[] renderTextures;

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
        // RenderTexture 생성
        renderTextures = new RenderTexture[2];
        for (int i = 0; i < renderTextures.Length; i++)
        {
            renderTextures[i] = new RenderTexture(Screen.width, Screen.height, 24);
        }

        foreach (GameObject pair in cameras)
            pair.gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        if (!setSwitchOne && !setSwitchTwo)
            SelectPortalCamera(0);
        else if (setSwitchOne && !setSwitchTwo)
        {
            SelectPortalCamera(1);
            SelectPortalCamera(2);
        }
        else if (setSwitchOne && setSwitchTwo)
        {
            SelectPortalCamera(2);
            SelectPortalCamera(3);
        }

    }

    void UpdatePortalCamera(Transform portal, Transform otherPortal, Camera portalCam)
    {
        //플레이어 카메라와 상대 포탈의 차이
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
        portalCam.transform.position = portal.position + playerOffsetFromPortal;

        //회전 계산
        Quaternion portalRotDiff = portal.rotation * Quaternion.Inverse(otherPortal.rotation);
        Vector3 newCamDir = portalRotDiff * playerCamera.forward;

        portalCam.transform.rotation = Quaternion.LookRotation(newCamDir, Vector3.up);

    }

    private void SelectPortalCamera(int selectNumOne)
    {

        playerCamera = Camera.main.transform;

        if (portalPairs[selectNumOne].cameraA.targetTexture != null)
        {
            portalPairs[selectNumOne].cameraA.targetTexture.Release();
        }

        if (portalPairs[selectNumOne].cameraB.targetTexture != null)
        {
            portalPairs[selectNumOne].cameraB.targetTexture.Release();
        }

        portalPairs[selectNumOne].cameraA.targetTexture = renderTextures[0];
        portalPairs[selectNumOne].cameraB.targetTexture = renderTextures[1];
        portalPairs[selectNumOne].cameraMatB.mainTexture = renderTextures[0];
        portalPairs[selectNumOne].cameraMatA.mainTexture = renderTextures[1];

        // Teleporter 연결
        var teleA = portalPairs[selectNumOne].portalA.GetComponentInChildren<PortalTeleporter>();
        var teleB = portalPairs[selectNumOne].portalB.GetComponentInChildren<PortalTeleporter>();

        teleA.reciever = portalPairs[selectNumOne].portalB;
        teleB.reciever = portalPairs[selectNumOne].portalA;


        UpdatePortalCamera(portalPairs[selectNumOne].portalA, portalPairs[selectNumOne].portalB, portalPairs[selectNumOne].cameraA);
        UpdatePortalCamera(portalPairs[selectNumOne].portalB, portalPairs[selectNumOne].portalA, portalPairs[selectNumOne].cameraB);
    }



    public void CheckSwitch(int checkNum)
    {
        if (checkNum == 0)
        {
            setSwitchOne = !setSwitchOne;
        }
        else if (checkNum == 1) 
        {
            setSwitchTwo = !setSwitchTwo;

            lastPortal.SetActive(setSwitchTwo);
            

        }

        if (setSwitchOne && setSwitchTwo) 
            door.DoorOpen();
        else door.DoorClose();
    }
}
