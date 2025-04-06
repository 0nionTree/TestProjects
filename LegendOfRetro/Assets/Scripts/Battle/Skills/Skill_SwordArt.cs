using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "Skills/Skill_SwordArt")]
public class Skill_SwordArt : SkillBase
{
    public override void Activate(GameObject user, GameObject target)
    {
        // ��Ÿ�� ����
        currSkillCooldown = baseSkillCooldown;

        // ����� ĳ���� ���� ��������
        Character character = user.GetComponent<Character>();

        // �� ������Ʈ �� ���� ������Ʈ ����
        GameObject swordObject = character.sword;
        Sword sword = swordObject.GetComponent<Sword>();
        Transform swordT = swordObject.transform;

        // �ٶ󺸴� ���� (���� ��ǥ ����)
        float baseAngle = character.Angle;

        // �浹 Ȱ��ȭ �� ȿ�� ����
        sword.isAttacking = true;   // �� �浹 On
        sword.spUp = true;          // SP ȸ�� ȿ�� On

        // ����� ����
        sword.damage = character.attackPower * (power * 0.01f);

        // �� ���� ��ġ (�� �ݵ�� localPosition �������� �����ؾ� ��)
        Vector3 originPos = swordT.localPosition;

        // ���� ���� ����: �÷��̾� ���� -45�� ~ +45�� ���� 5�� ����Ʈ
        float radius = 3f;
        float[] angles = { -90f, -45f, 0f, 45f, 90f };            // ���� ����
        float[] durations = { 0.1f, 0.05f, 0.05f, 0.05f, 0.05f }; // �� �ܰ躰 �̵� �ð�

        // DOTween ���� �̵� ������ ����
        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < angles.Length; i++)
        {
            // ���� �� ���� ��ȯ
            float angleRad = (baseAngle + angles[i]) * Mathf.Deg2Rad;

            // ���� �������� �̵��� ��ġ ���
            Vector3 offset = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0) * radius;

            // ���� ��ǥ ���� �̵� (���� �÷��̾� �ڽ� ������Ʈ�̹Ƿ� localMove ���)
            seq.Append(swordT.DOLocalMove(offset, durations[i]).SetEase(Ease.OutQuad));
        }

        // �������� ���� ��ġ�� ����
        seq.Append(swordT.DOLocalMove(originPos, 0.2f).SetEase(Ease.InQuad))

            // ��� �̵��� ������ ���� �ʱ�ȭ
            .OnComplete(() =>
            {
                sword.isAttacking = false;          // �浹 Off
                sword.HitMonsters.Clear();          // �ǰݵ� ���� ����Ʈ �ʱ�ȭ
            });

        // ����� �α�
        Debug.Log($"{skillName} ���!");
    }

}