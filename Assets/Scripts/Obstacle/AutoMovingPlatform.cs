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

        // Rigidbody 설정을 올바르게 해줍니다.
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        if (waypoints.Length == 0 || rb == null) return;

        // 현재 웨이포인트를 목표 지점으로 설정
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // 목표 지점을 향해 이동
        Vector3 newPosition = Vector3.MoveTowards(
            rb.position,
            targetWaypoint.position,
            speed * Time.fixedDeltaTime
        );

        // Rigidbody를 사용하여 물리적으로 이동
        rb.MovePosition(newPosition);

        // 목표 지점에 거의 도달했는지 확인
        if (Vector3.Distance(rb.position, targetWaypoint.position) < 0.1f)
        {
            // 다음 웨이포인트로 인덱스 변경 (순환)
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    // 플레이어가 발판과 충돌했을 때 부모로 설정
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어의 부모를 이 발판으로 설정
            collision.transform.SetParent(this.transform);
        }
    }

    // 플레이어가 발판에서 떨어졌을 때 부모 해제
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어의 부모를 다시 null로 설정
            collision.transform.SetParent(null);
        }
    }
}