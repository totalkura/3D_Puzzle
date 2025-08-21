using UnityEditor.SceneManagement;
using UnityEngine;

public class CreateCube : MonoBehaviour,IInteractable
{
    public GameObject cube;
    public GameObject wall;

    GameObject cubes;

    bool cubeCreate;

    public string GetPrompt()
    {
        return "큐브를 생성합니다";
    }


    public void Interact()
    {
        Vector3 spawnPosition = transform.position + transform.forward * 1.0f;

        if (!cubeCreate)
        {
            cubeCreate = true;
            cubes = Instantiate(cube, spawnPosition, transform.rotation);
            cubes.name = "Cube";
            
        }
        else
        {
            cubes.transform.position = spawnPosition;
        }

        if (wall != null && wall.activeSelf)
        {
            wall.SetActive(false);
        }
        else if (wall != null && !wall.activeSelf)
        {
            wall.SetActive(true);
        }

    }
}
