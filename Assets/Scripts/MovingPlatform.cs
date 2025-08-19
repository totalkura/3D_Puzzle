using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 3f;
    private float direction = 1;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void FixedUpdate()
    {
        // z축으로 이동
        rb.MovePosition(transform.position + new Vector3(0, 0, direction * speed * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어 태우기
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }

        // 벽에 부딪히면 방향 반전
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("벽에 부딪힘, 방향 반전");
            direction *= -1f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 발판에서 내려오면 부모 해제
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}





