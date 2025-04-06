using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage; // Fill �̹��� ����
    public Image backImage; // ü�� ���� �̹��� ����
    public TMP_Text fillText;

    private Tween currentTween; // �ߺ� �ִϸ��̼� ������

    public void SetHealth(float current, float max)
    {
        float ratio = Mathf.Clamp01(current / max); // 0~1 ���̷� ����

        fillImage.fillAmount = ratio;               // ü�¹� �̹��� ���̱�/�ø���

        // ���� Ʈ���� ���� ���̶�� ����
        if (currentTween != null && currentTween.IsActive())
            currentTween.Kill();

        // backImage�� �ε巴�� 0.3�ʿ� ���� ����
        currentTween = backImage.DOFillAmount(ratio, 0.3f).SetEase(Ease.OutQuad);

        fillText.text = $"{current} / {max}";       // ü�¹� ��ġ ����
    }
}