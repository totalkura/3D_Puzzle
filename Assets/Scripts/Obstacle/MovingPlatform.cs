using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 3f;
    private float direction = 1;

    private Rigidbody rb;

    [Header("Check XYZ")]
    public bool checkX;
    public bool checkY;
    public bool checkZ;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        if (checkX )
            rb.MovePosition(transform.position + new Vector3(direction * speed * Time.fixedDeltaTime, 0, 0));
        else if(checkY)
            rb.MovePosition(transform.position + new Vector3(0, direction * speed * Time.fixedDeltaTime, 0));
        else if(checkZ)
            rb.MovePosition(transform.position + new Vector3(0, 0, direction * speed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }

        // 벽에 부딪히면 방향 반전
        if (other.gameObject.CompareTag("Wall"))
        {
            //Debug.Log("벽에 부딪힘, 방향 반전");
            direction *= -1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 발판에서 내려오면 부모 해제
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}





