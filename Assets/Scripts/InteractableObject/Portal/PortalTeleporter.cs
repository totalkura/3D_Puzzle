using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public Transform player;
    public Transform reciever;

    private bool playerIsOverlapping = false;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (reciever == null)
        {
            Debug.LogError("Reciever portal is not assigned in the PortalTeleporter script.");
        }
    }

    void LateUpdate()
    {
        if (!playerIsOverlapping) return;

        Vector3 portalToPlayer = player.position - transform.position;
        // 포탈 전면 방향과 플레이어 위치 비교
        float dotProduct = Vector3.Dot(transform.forward, portalToPlayer);

        if (dotProduct < 0f)
        {
            // 회전 계산
            Quaternion portalRotationDiff = reciever.rotation * Quaternion.Inverse(transform.rotation);
            Vector3 playerDirection = portalRotationDiff * player.forward;
            player.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);

            // 위치 오프셋 계산 + 살짝 위로 띄우기
            Quaternion targetRotation = Quaternion.FromToRotation(player.transform.up, Vector3.up) * player.transform.rotation;
            player.transform.rotation = targetRotation; // 또는 Slerp로 부드럽게
            Vector3 offset = portalRotationDiff * portalToPlayer;
            Vector3 adjustedPosition = reciever.position + offset;
            adjustedPosition.y += 2f; // 1미터 정도 위로 띄우기

            player.position = adjustedPosition;

            playerIsOverlapping = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsOverlapping = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsOverlapping = false;
        }
    }
}
