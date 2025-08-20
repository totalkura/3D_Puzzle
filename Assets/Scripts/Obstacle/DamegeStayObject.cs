using UnityEngine;

public class DamegeStayObject : MonoBehaviour
{
    public float damege;

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player")
        {
            CharacterManager.Instance.Player.condition.HasHealth(damege * Time.deltaTime);
        }
    }
}
