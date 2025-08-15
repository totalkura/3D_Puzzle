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
        return "문을 열수 있을 것 같다";
    }

    public void Interact()
    {
        DoorOpen();
    }


}
