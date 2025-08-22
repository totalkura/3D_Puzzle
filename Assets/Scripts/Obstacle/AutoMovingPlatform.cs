using UnityEngine;

public class AutoMovingPlatform : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5f;

    private int currentWaypointIndex = 0;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on the platform. Please add one.");
        }

        // Rigidbody ������ �ùٸ��� ���ݴϴ�.
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        if (waypoints.Length == 0 || rb == null) return;

        // ���� ��������Ʈ�� ��ǥ �������� ����
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // ��ǥ ������ ���� �̵�
        Vector3 newPosition = Vector3.MoveTowards(
            rb.position,
            targetWaypoint.position,
            speed * Time.fixedDeltaTime
        );

        // Rigidbody�� ����Ͽ� ���������� �̵�
        rb.MovePosition(newPosition);

        // ��ǥ ������ ���� �����ߴ��� Ȯ��
        if (Vector3.Distance(rb.position, targetWaypoint.position) < 0.1f)
        {
            // ���� ��������Ʈ�� �ε��� ���� (��ȯ)
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    // �÷��̾ ���ǰ� �浹���� �� �θ�� ����
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾��� �θ� �� �������� ����
            collision.transform.SetParent(this.transform);
        }
    }

    // �÷��̾ ���ǿ��� �������� �� �θ� ����
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾��� �θ� �ٽ� null�� ����
            collision.transform.SetParent(null);
        }
    }
}