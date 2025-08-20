using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTelleporter : MonoBehaviour
{
    public Transform player;
    public Transform receiver;
    private bool canUsePortal;

    private bool playerIsOverlapping = false;

    private void OnTriggerEnter(Collider other)
    {
        playerIsOverlapping = true;
        if (other.tag == "Player")
        {
           onTeleport();
        }
        playerIsOverlapping = false;
    }

    private void onTeleport()
    {
        if (playerIsOverlapping && !recentlyTeleported)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProdict = Vector3.Dot(transform.up, portalToPlayer);

            if (dotProdict < 0f)
            {
                float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
                rotationDiff += 180f;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                Vector3 exitOffset = receiver.forward * 1.0f + Vector3.up*0.01f;

                player.position = receiver.position + positionOffset + exitOffset;

                playerIsOverlapping = false;
            }
             StartCoroutine(TeleportCooldown(0.2f));
        }
    }
    private bool recentlyTeleported = false;
    private IEnumerator TeleportCooldown(float seconds)
    {
        recentlyTeleported = true;
        yield return new WaitForSeconds(seconds);
        recentlyTeleported = false;
    }
}
