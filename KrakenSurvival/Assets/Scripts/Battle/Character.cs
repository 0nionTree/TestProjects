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

    // �÷��̾� �������� ���� ������ �ִ� ����(GameObject)�� ��ȯ�ϴ� �Լ�
    private void GetNearestMonster()
    {
        GameObject nearest = null;                      // ������� ã�� ���� ����� ����
        float nearestDistance = Mathf.Infinity;         // ���� ����� �Ÿ� (�ʱⰪ�� ���Ѵ�� ����)
        weaponAngle = 0f;  // �⺻�� ����

        // GameManager�� ��ϵ� ��� ���͸� ��ȸ
        foreach (GameObject monster in GameManager.instance.monsters)
        {
            if (monster == null) continue;              // �̹� ���ŵ� ���Ͱ� ����Ʈ�� ���� ��� ����

            NormalState normalState = monster.GetComponent<NormalState>();
            if (normalState == null || normalState.isLive == false) continue; // ���Ͱ� ������� ���� ��쿡�� ����

            // �÷��̾� ��ġ�� ���� ��ġ ���� �Ÿ� ���
            float distance = Vector2.Distance(transform.position, monster.transform.position);

            // ���� �Ÿ����� �� ����� ���Ͱ� �ִٸ� ����
            if (distance < nearestDistance)
            {
                nearestDistance = distance;             // ���� ����� �Ÿ� ����
                nearest = monster;                      // ���� ����� ���� ����
            }
        }

        // ���� ����� ���͸� ã�� ���
        if (nearest != null)
        {
            Vector2 targetPos = nearest.transform.position;
            Vector2 direction = (targetPos - (Vector2)transform.position).normalized;

            weaponAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // ����׿� �� ǥ�� (�� �信�� Ȯ�� ����)
            // �÷��̾� �� ���� ����� ���� �������� ���� ���� �׸�
            Debug.DrawLine(transform.position, targetPos, Color.red);
        }
        nearestMonster = nearest;
    }

}
