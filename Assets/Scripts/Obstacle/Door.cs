using UnityEngine;

public class Door : MonoBehaviour,IInteractable
{
    public Animator animator;
    public float doorTiem;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void DoorOpen()
    {
        animator.SetBool("character_nearby", true);
        if (doorTiem > 0)
        {
            Invoke("DoorClose", doorTiem);
        }
    }
    public void DoorClose()
    {
        animator.SetBool("character_nearby", false);
    }
    public void Interact()
    {
        DoorOpen();
    }

    public string GetPrompt()
    {
        return "문을 열수 있을 것 같다";
    }

}
