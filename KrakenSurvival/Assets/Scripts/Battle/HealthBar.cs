using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage; // Fill �̹��� ����
    public TMP_Text fillText;

    public void SetHealth(float current, float max)
    {
        float ratio = Mathf.Clamp01(current / max); // 0~1 ���̷� ����
        fillImage.fillAmount = ratio;
        fillText.text = $"{current} / {max}";
    }
}