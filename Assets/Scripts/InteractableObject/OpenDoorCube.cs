using UnityEngine;

public class OpenDoorCube : MonoBehaviour, IInteractable
{
    Door door;



    public string GetPrompt()
    {
        return "잘하면 문을 열 수 있을지도?";
    }

    public void Interact()
    {

        door = GetComponentInParent<Door>();
        door.DoorOpen();
        //문에대한정보
    }

}
