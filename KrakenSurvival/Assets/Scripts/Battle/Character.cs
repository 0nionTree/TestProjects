using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string charName;

    public int maxSp = 10;
    public int curSp = 0;

    public float attackCooldown = 1;

    public int level = 1;
    public int maxExp = 100;
    public int curExp = 0;

    public int gold = 0;

    public GameObject weapon;
    private float weaponAngle = 0f;

    private GameObject nearestMonster;

    void Update()
    {
        GetNearestMonster();
        weapon.transform.rotation = Quaternion.Euler(0f, 0f, weaponAngle-90f);
    }

    // 플레이어 기준으로 가장 가까이 있는 몬스터(GameObject)를 반환하는 함수
    private void GetNearestMonster()
    {
        GameObject nearest = null;                      // 현재까지 찾은 가장 가까운 몬스터
        float nearestDistance = Mathf.Infinity;         // 가장 가까운 거리 (초기값은 무한대로 설정)
        weaponAngle = 0f;  // 기본값 설정

        // GameManager에 등록된 모든 몬스터를 순회
        foreach (GameObject monster in GameManager.instance.monsters)
        {
            if (monster == null) continue;              // 이미 제거된 몬스터가 리스트에 있을 경우 무시

            NormalState normalState = monster.GetComponent<NormalState>();
            if (normalState == null || normalState.isLive == false) continue; // 몬스터가 살아있지 않을 경우에도 무시

            // 플레이어 위치와 몬스터 위치 간의 거리 계산
            float distance = Vector2.Distance(transform.position, monster.transform.position);

            // 현재 거리보다 더 가까운 몬스터가 있다면 갱신
            if (distance < nearestDistance)
            {
                nearestDistance = distance;             // 가장 가까운 거리 갱신
                nearest = monster;                      // 가장 가까운 몬스터 갱신
            }
        }

        // 가장 가까운 몬스터를 찾은 경우
        if (nearest != null)
        {
            Vector2 targetPos = nearest.transform.position;
            Vector2 direction = (targetPos - (Vector2)transform.position).normalized;

            weaponAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 디버그용 선 표시 (씬 뷰에서 확인 가능)
            // 플레이어 → 가장 가까운 몬스터 방향으로 빨간 선을 그림
            Debug.DrawLine(transform.position, targetPos, Color.red);
        }
        nearestMonster = nearest;
    }

}
