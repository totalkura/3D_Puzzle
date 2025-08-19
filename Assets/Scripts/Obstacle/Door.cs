using UnityEngine;

public class Door : MonoBehaviour,IInteractable
{
    public Animator animator;
    public GameObject gameObjects;
    public float doorTime;

    Material material;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if(gameObjects != null)
            material = gameObjects.GetComponent<Renderer>().material;
    }

    public void DoorOpen()
    {
        animator.SetBool("character_nearby", true);
        SoundManager.instance.PlayOther(SoundManager.other.door);
        
        if(material != null)
            material.color = Color.blue;
        if (doorTime > 0)
        {
            Invoke("DoorClose", doorTime);
        }
    }
    public void DoorClose()
    {
        if (material != null)
            material.color = Color.red;
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
