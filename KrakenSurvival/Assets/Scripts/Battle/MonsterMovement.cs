using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public Transform target;         // 따라갈 대상 ( Player)

    private Rigidbody2D rb;                 // 몬스터의 Rigidbody2D
    private SpriteRenderer spriteRenderer;  // 몬스터의 SpriteRenderer

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();               // Rigidbody2D 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();// SpriteRenderer 가져오기
    }

    void FixedUpdate()
    {
        if (target == null) return; // 추적 대상이 없으면 아무 것도 하지 않음

        float moveSpeed = GetComponent<NormalState>().moveSpeed;                    // 몬스터의 이동 속도 가져오기

        Vector2 direction = ((Vector2)target.position - rb.position).normalized;    // 플레이어(타겟) 방향 벡터 계산 후 정규화

        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime); // 물리 기반 이동 (현재 위치 + 방향 * 속도 * 고정 프레임 시간)

        // 방향에 따라 스프라이트 반전 처리
        if (direction.x < -0.01f)
        {
            spriteRenderer.flipX = true;    // 왼쪽 방향 > 좌우 반전
        }
        else if (direction.x > 0.01f)
        {
            spriteRenderer.flipX = false;   // 오른쪽 방향 > 원래대로
        }
    }
}