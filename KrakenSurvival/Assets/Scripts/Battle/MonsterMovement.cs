using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public Transform target;         // ���� ��� ( Player)

    private Rigidbody2D rb;                 // ������ Rigidbody2D
    private SpriteRenderer spriteRenderer;  // ������ SpriteRenderer

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();               // Rigidbody2D ��������
        spriteRenderer = GetComponent<SpriteRenderer>();// SpriteRenderer ��������
    }

    void FixedUpdate()
    {
        if (target == null) return; // ���� ����� ������ �ƹ� �͵� ���� ����

        float moveSpeed = GetComponent<NormalState>().moveSpeed;                    // ������ �̵� �ӵ� ��������

        Vector2 direction = ((Vector2)target.position - rb.position).normalized;    // �÷��̾�(Ÿ��) ���� ���� ��� �� ����ȭ

        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime); // ���� ��� �̵� (���� ��ġ + ���� * �ӵ� * ���� ������ �ð�)

        // ���⿡ ���� ��������Ʈ ���� ó��
        if (direction.x < -0.01f)
        {
            spriteRenderer.flipX = true;    // ���� ���� > �¿� ����
        }
        else if (direction.x > 0.01f)
        {
            spriteRenderer.flipX = false;   // ������ ���� > �������
        }
    }
}