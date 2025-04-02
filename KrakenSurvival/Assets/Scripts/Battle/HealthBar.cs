using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage; // Fill 이미지 연결
    public Image backImage; // 체력 감소 이미지 연결
    public TMP_Text fillText;

    private Tween currentTween; // 중복 애니메이션 방지용

    public void SetHealth(float current, float max)
    {
        float ratio = Mathf.Clamp01(current / max); // 0~1 사이로 제한

        fillImage.fillAmount = ratio;               // 체력바 이미지 줄이기/늘리기

        // 기존 트윈이 실행 중이라면 정지
        if (currentTween != null && currentTween.IsActive())
            currentTween.Kill();

        // backImage를 부드럽게 0.3초에 걸쳐 변경
        currentTween = backImage.DOFillAmount(ratio, 0.3f).SetEase(Ease.OutQuad);

        fillText.text = $"{current} / {max}";       // 체력바 수치 변경
    }
}