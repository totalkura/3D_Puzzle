using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    public int doorNum;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void DoorOpen()
    {
        if(doorNum == MapManager.Instance.doorNumCheck)
            animator.SetBool("character_nearby", true);
    }

}
