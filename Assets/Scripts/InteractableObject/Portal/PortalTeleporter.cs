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
        // ��Ż ���� ����� �÷��̾� ��ġ ��
        float dotProduct = Vector3.Dot(transform.forward, portalToPlayer);

        if (dotProduct < 0f)
        {
            // ȸ�� ���
            Quaternion portalRotationDiff = reciever.rotation * Quaternion.Inverse(transform.rotation);
            Vector3 playerDirection = portalRotationDiff * player.forward;
            player.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);

            // ��ġ ������ ��� + ��¦ ���� ����
            Quaternion targetRotation = Quaternion.FromToRotation(player.transform.up, Vector3.up) * player.transform.rotation;
            player.transform.rotation = targetRotation; // �Ǵ� Slerp�� �ε巴��
            Vector3 offset = portalRotationDiff * portalToPlayer;
            Vector3 adjustedPosition = reciever.position + offset;
            adjustedPosition.y += 2f; // 1���� ���� ���� ����

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
