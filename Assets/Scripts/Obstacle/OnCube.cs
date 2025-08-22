using UnityEngine;

public class OnCube : MonoBehaviour
{
    public int checkCube;
    public GameObject wall;

    CheckLaser laserObject;

    private void Start()
    {
        laserObject = GetComponentInParent<CheckLaser>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Cube")
        {
            laserObject.OnCubeCheck(checkCube);
            if(wall != null) wall.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.name == "Cube")
        {
            laserObject.OutCubeCheck(checkCube);
            if (wall != null) wall.SetActive(true);
        }
    }
}
