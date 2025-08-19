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
        // z������ �̵�
        rb.MovePosition(transform.position + new Vector3(0, 0, direction * speed * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �÷��̾� �¿��
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }

        // ���� �ε����� ���� ����
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("���� �ε���, ���� ����");
            direction *= -1f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // ���ǿ��� �������� �θ� ����
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}





