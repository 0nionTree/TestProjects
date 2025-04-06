using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;                 // 캐릭터의 Rigidbody2D
    private SpriteRenderer spriteRenderer;  // 캐릭터의 SpriteRenderer
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();               // Rigidbody2D 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();// SpriteRenderer 가져오기
    }

    void Update()
    {
        // 키 입력 감지
        movement.x = Input.GetAxisRaw("Horizontal"); // A/D 또는 ←/→
        movement.y = Input.GetAxisRaw("Vertical");   // W/S 또는 ↑/↓

        movement = movement.normalized; // 대각선 이동 시 속도 보정

        
        
        if (movement.x < -0.01f)
        {
            spriteRenderer.flipX = true;    // 왼쪽 방향 > 좌우 반전
        }
        else if (movement.x > 0.01f)
        {
            spriteRenderer.flipX = false;   // 오른쪽 방향 > 원래대로
        }
    }

    void FixedUpdate()
    {
        // 물리 기반 이동
        float moveSpeed = GetComponent<NormalState>().moveSpeed;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}