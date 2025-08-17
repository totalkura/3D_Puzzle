using UnityEngine;

public class Cubes : MonoBehaviour,IInteractable
{

    public string GetPrompt()
    {
        return "?";
    }


    public void Interact()
    {
        CharacterManager.Instance.Player.controller.characterGetItem = !CharacterManager.Instance.Player.controller.characterGetItem;

        if (CharacterManager.Instance.Player.controller.characterGetItem)
        {
            gameObject.transform.SetParent(CharacterManager.Instance.Player.gameObject.transform);
        }
        else if (!CharacterManager.Instance.Player.controller.characterGetItem)
        {
            gameObject.transform.SetParent(null);
        }
    }
}
