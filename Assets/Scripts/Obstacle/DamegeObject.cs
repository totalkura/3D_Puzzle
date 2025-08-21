using UnityEngine;

public class DamegeObject : MonoBehaviour
{
    public int dameged;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CharacterManager.Instance.Player.condition.HasHealth(dameged);

        }
    }
}
