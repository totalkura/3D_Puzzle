using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PortalCamera : MonoBehaviour
{
    [SerializeField]private Transform playerCamera;
    [SerializeField]private Transform myPortal;
    [SerializeField]private Transform linkedPortal;

    void Update()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - linkedPortal.position;
        transform.position = myPortal.position + playerOffsetFromPortal;

        float angularDifferenceBetweenPortals = Quaternion.Angle(myPortal.rotation, linkedPortal.rotation);

        Quaternion portalRotationDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortals, Vector3.up);
    }

}