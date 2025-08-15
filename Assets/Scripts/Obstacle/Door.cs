using UnityEngine;

public class Door : MonoBehaviour,IInteractable
{
    public Animator animator;
    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void DoorOpen()
    {
        animator.SetBool("character_nearby", true);
    }

    public string GetPrompt()
    {
        return "���� ���� ���� �� ����";
    }

    public void Interact()
    {
        DoorOpen();
    }


}
