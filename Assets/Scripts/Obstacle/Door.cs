using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void DoorOpen()
    {
        animator.SetBool("character_nearby", true);
    }

}
