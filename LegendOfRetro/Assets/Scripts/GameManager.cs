using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글턴 전역화 - 어디서든 접근 가능

    [Header("플레이어 캐릭터")]
    public GameObject playerCharacter;
    private NormalState playerState;

    [Header("몬스터 리스트")]
    public List<GameObject> monsters = new List<GameObject>();

    [Header("텍스트 출력")]
    public GameObject canvas;
    public GameObject textPrefab;

    [Header("체력바")]
    public HealthBar healthbar;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);  // 중복 방지
    }

    private void Start()
    {
        DOTween.Init(false, true, LogBehaviour.Verbose).SetCapacity(200, 50);

        playerState = playerCharacter.GetComponent<NormalState>();
    }

    public void Damage(GameObject user, GameObject target, float damage)    // 대미지를 주는 함수
    {
        NormalState targetState = target.GetComponent<NormalState>();

        float cul;

        if (targetState.isLive == false)// 대상이 죽어있으면 대미지 없음
            return;

        if (targetState.isInvincible)   // 대상이 무적 상태라면 면역 출력
        {
            ShowFloatingText(target, "면역!");// 대상 위치에 "면역" 텍스트 출력
            damage = 0;
        }
        else    // 무적 상태가 아니라면 대미지 계산
        {
            if ((cul = targetState.curHp - damage) > 0) // 대미지 받은 후의 체력이 0 이상이라면
                targetState.curHp = cul;    // 방어력이 없으니 별도의 계산공식 없이 즉시 대미지 반영
            else    // 대미지 받은 후의 체력이 0 이하라면
            {
                targetState.curHp = 0;  // 체력을 0으로 변경
                targetState.isLive = false; // 대상 사망
                Debug.Log($"{target.name}은 사망했다");
            }

            HitEffect(target);                          // 대상에게 반짝이는 효과 생성
            ShowFloatingText(target, damage.ToString());// 대상 위치에 대미지 텍스트 출력

            if (target == playerCharacter)              // 대상이 플레이어 캐릭터라면 체력바 반영
            {
                healthbar.SetHealth(playerState.curHp, playerState.maxHp);
            }
            Debug.Log($"{user.name}가 {target.name}에게 {damage}의 대미지");
        }
    }

    public void HitEffect(GameObject target)    // 대미지를 받으면 반짝이는 이펙트
    {
        SpriteRenderer sr = target.GetComponent<SpriteRenderer>();
        if (sr == null) return;

        Color originalColor = sr.color;

        // 이 GameObject의 트윈을 모두 제거
        DOTween.Kill(target);

        Sequence seq = DOTween.Sequence().SetId(target);
        Color flashColor = new Color(100f, 100f, 100f, 1f);
        seq.Append(sr.DOColor(flashColor, 0.05f));
        seq.Append(sr.DOColor(originalColor, 0.05f));
    }

    public void ShowFloatingText(GameObject target, string message)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);  // 대상(target)의 위치를 화면 좌표(UI 기준)로 변환

        GameObject obj = Instantiate(textPrefab, canvas.transform);     // TMP 텍스트 프리팹을 Canvas 하위에 생성

        RectTransform rt = obj.GetComponent<RectTransform>();           // 생성된 오브젝트에서 RectTransform과 TMP 컴포넌트 가져오기
        TextMeshProUGUI tmp = obj.GetComponent<TextMeshProUGUI>();

        if (rt == null || tmp == null) return;                          // 컴포넌트가 없으면 중단

        tmp.text = message;             // 텍스트 설정

        rt.position = screenPos;        // 위치를 화면상의 대상 위치로 설정

        Color color = tmp.color;        // 알파값(투명도) 초기화 – 완전 불투명으로 설정
        color.a = 1f;
        tmp.color = color;

        // 애니메이션 파라미터 설정
        float moveAmount = 15f;         // 얼마나 위로 튈지 (Y값 기준)
        float moveDuration = 0.1f;      // 위로 올라가고 내려오는 데 걸리는 시간
        float fadeDuration = 0.4f;      // 투명해지는 데 걸리는 시간
        float shrinkScale1 = 1.1f;       // 확대 비율
        float shrinkScale2 = 0.5f;       // 축소 비율

        // 원래 위치와 위로 튀는 위치 계산
        Vector2 originalPos = rt.anchoredPosition;
        Vector2 upPos = originalPos + new Vector2(0, moveAmount); // 위로 moveAmount 만큼

        // DOTween Sequence 생성: 애니메이션을 순차적으로 실행
        Sequence seq = DOTween.Sequence();

        seq.Append(rt.DOAnchorPos(upPos, moveDuration).SetEase(Ease.OutQuad));      // 먼저 위로 튀어오르기 (Ease: OutQuad = 빠르게 시작해서 천천히 끝)
        seq.Join(rt.DOScale(shrinkScale1, moveDuration).SetEase(Ease.OutQuad));      // 튀어오르며 살짝 커짐
        seq.Append(rt.DOAnchorPos(originalPos, moveDuration).SetEase(Ease.InQuad)); // 다시 제자리로 내려오기 (Ease: InQuad = 천천히 시작해서 빠르게 끝)
        seq.Append(tmp.DOFade(0f, fadeDuration));                               // 내려온 뒤 텍스트를 투명하게 만들기
        seq.Join(rt.DOScale(shrinkScale2, fadeDuration).SetEase(Ease.InQuad));  // 투명해지며 작아지게 만들기
        seq.OnComplete(() => Destroy(obj));                                     // 모든 애니메이션이 끝나면 텍스트 오브젝트 파괴
    }
}
