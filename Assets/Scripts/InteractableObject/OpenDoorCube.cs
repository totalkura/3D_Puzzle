using UnityEngine;

public class OpenDoorCube : MonoBehaviour, IInteractable
{
    Door door;



    public string GetPrompt()
    {
        return "���ϸ� ���� �� �� ��������?";
    }

    public void Interact()
    {

        door = GetComponentInParent<Door>();
        door.DoorOpen();
        //������������
    }

}
