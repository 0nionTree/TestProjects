using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "Skills/Skill_SwordArt")]
public class Skill_SwordArt : SkillBase
{
    public override void Activate(GameObject user, GameObject target)
    {
        // 쿨타임 설정
        currSkillCooldown = baseSkillCooldown;

        // 사용자 캐릭터 정보 가져오기
        Character character = user.GetComponent<Character>();

        // 검 오브젝트 및 관련 컴포넌트 참조
        GameObject swordObject = character.sword;
        Sword sword = swordObject.GetComponent<Sword>();
        Transform swordT = swordObject.transform;

        // 바라보는 각도 (월드 좌표 기준)
        float baseAngle = character.Angle;

        // 충돌 활성화 및 효과 설정
        sword.isAttacking = true;   // 검 충돌 On
        sword.spUp = true;          // SP 회복 효과 On

        // 대미지 설정
        sword.damage = character.attackPower * (power * 0.01f);

        // 검 원래 위치 (★ 반드시 localPosition 기준으로 저장해야 함)
        Vector3 originPos = swordT.localPosition;

        // 공격 궤적 설정: 플레이어 기준 -45도 ~ +45도 사이 5개 포인트
        float radius = 3f;
        float[] angles = { -90f, -45f, 0f, 45f, 90f };            // 방향 각도
        float[] durations = { 0.1f, 0.05f, 0.05f, 0.05f, 0.05f }; // 각 단계별 이동 시간

        // DOTween 연속 이동 시퀀스 생성
        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < angles.Length; i++)
        {
            // 각도 → 라디안 변환
            float angleRad = (baseAngle + angles[i]) * Mathf.Deg2Rad;

            // 로컬 기준으로 이동할 위치 계산
            Vector3 offset = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0) * radius;

            // 로컬 좌표 기준 이동 (검은 플레이어 자식 오브젝트이므로 localMove 사용)
            seq.Append(swordT.DOLocalMove(offset, durations[i]).SetEase(Ease.OutQuad));
        }

        // 마지막에 원래 위치로 복귀
        seq.Append(swordT.DOLocalMove(originPos, 0.2f).SetEase(Ease.InQuad))

            // 모든 이동이 끝나면 상태 초기화
            .OnComplete(() =>
            {
                sword.isAttacking = false;          // 충돌 Off
                sword.HitMonsters.Clear();          // 피격된 몬스터 리스트 초기화
            });

        // 디버깅 로그
        Debug.Log($"{skillName} 사용!");
    }

}