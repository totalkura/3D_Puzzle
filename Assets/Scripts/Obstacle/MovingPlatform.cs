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

        // ���� �ε����� ���� ����
        if (other.gameObject.CompareTag("Wall"))
        {
            //Debug.Log("���� �ε���, ���� ����");
            direction *= -1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ���ǿ��� �������� �θ� ����
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}





