using UnityEngine;

public class PortalShooter : MonoBehaviour
{
    public GameObject bluePortalPrefab;
    public GameObject orangePortalPrefab;
    private GameObject bluePortal;
    private GameObject orangePortal;

    private Interaction interaction;

    private void Awake()
    {
        interaction = GetComponent<Interaction>();
    }

    public void ShootPortal(bool isBlue)
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2f, interaction.layerMask))
        {
            float offset = 0.05f;
            Vector3 spawnPoint = hit.point + hit.normal * offset;

            Quaternion spawnRotation = Quaternion.LookRotation(hit.normal) * Quaternion.Euler(0, 180, 0);

            GameObject prefab = isBlue ? bluePortalPrefab : orangePortalPrefab;
            GameObject portalInstance = isBlue ? bluePortal : orangePortal;

            if (portalInstance == null)
            {
                portalInstance = Instantiate(prefab, spawnPoint, spawnRotation);
            }
            else
            {
                portalInstance.SetActive(false);
                portalInstance.transform.position = spawnPoint;
                portalInstance.transform.rotation = spawnRotation;
                portalInstance.SetActive(true);
            }

            Portal portalComponent = portalInstance.GetComponent<Portal>();
            if (portalComponent != null)
            {
                portalComponent.SetPortalColor(isBlue);
            }

            if (isBlue)
            {
                bluePortal = portalInstance;
            }
            else
            {
                orangePortal = portalInstance;
            }

            if (bluePortal != null && orangePortal != null)
            {
                Portal bluePortalComponent = bluePortal.GetComponent<Portal>();
                Portal orangePortalComponent = orangePortal.GetComponent<Portal>();

                bluePortalComponent.linkedPortal = orangePortalComponent;
                orangePortalComponent.linkedPortal = bluePortalComponent;
            }
        }
        else
        {
            return;
        }
    }
}