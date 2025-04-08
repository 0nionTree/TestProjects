using System.Collections.Generic;
using UnityEngine;

public class ResolutionChanger : MonoBehaviour
{
    private List<Vector2Int> resolutions = new List<Vector2Int>()
    {
        new Vector2Int(3088, 1440),
        new Vector2Int(1920, 1080),
        new Vector2Int(1600, 900),
        new Vector2Int(1280, 720),
        new Vector2Int(960, 540),
        new Vector2Int(640, 360),
    };

    private void Start()
    {
        Debug.Log($"�÷���: {Application.platform}");
        Debug.Log($"��ũ�� ũ��: {Screen.width}x{Screen.height}");
        Debug.Log($"�ػ�: {Screen.currentResolution.width}x{Screen.currentResolution.height}");
        Debug.Log($"DPI: {Screen.dpi}");
        float screenAspect = (float)Screen.width / Screen.height;
        Debug.Log("���� ȭ�� ����: " + screenAspect);

        // ���� ������� UI ���� ���� ����
        if (screenAspect < 1.5f)
        {
            // 4:3 ����� ���
        }
        else if (screenAspect < 1.8f)
        {
            // 16:10 ����� ���
        }
        else
        {
            // 16:9 �̻� (��κ��� �ֽ� ����Ʈ��)
        }
    }

    // �ػ� ���� �Լ�
    public void SetResolution1920x1080Window()
    {
        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
    }

    public void SetResolution1920x1080FullScreen()
    {
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
    }

    public void SetResolution1280x720Window()
    {
        Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
    }

    public void SetResolution1280x720FullScreen()
    {
        Screen.SetResolution(1280, 720, FullScreenMode.FullScreenWindow);
    }

    public void SetResolutionAutoBig()
    {
        SetResolutionAuto(preferBigger: true);
    }

    public void SetResolutionAutoSmall()
    {
        SetResolutionAuto(preferBigger: false);
    }

    // ���� ó�� �Լ�
    private void SetResolutionAuto(bool preferBigger)
    {
        // ���� ȭ�� ���� ��� (���� / ����)
        float currentAspect = (float)Screen.width / Screen.height;

        // ���� ȭ�� ���� (ũ�� �񱳿�)
        int currentArea = Screen.width * Screen.height;

        // ���� ������ �ػ󵵸� ������ ����
        Vector2Int? bestMatch = null;
        float bestAspectDiff = float.MaxValue;

        // �̸� ���ǵ� �ػ� ����Ʈ ��ȸ
        foreach (var res in resolutions)
        {
            int area = res.x * res.y;
            float aspect = (float)res.x / res.y;
            float aspectDiff = Mathf.Abs(aspect - currentAspect);

            // ���纸�� ū �ػ󵵸� ã�� ���
            // or ���� �ػ󵵸� ã�� ���� �б�
            bool isValid = preferBigger ? area >= currentArea : area <= currentArea;

            // ���ǿ� �����ϰ�, ���� ���̰� �� ���� �ػ󵵸� ����
            if (isValid && aspectDiff < bestAspectDiff)
            {
                bestMatch = res;
                bestAspectDiff = aspectDiff;
            }
        }

        // ���� ���õ� �ػ� ����
        if (bestMatch.HasValue)
        {
            // FullScreenWindow ���� â ���� ��üȭ�� ���
            Screen.SetResolution(bestMatch.Value.x, bestMatch.Value.y, FullScreenMode.FullScreenWindow);
            Debug.Log($"[�ػ� �ڵ� ���� �Ϸ�] {bestMatch.Value.x} x {bestMatch.Value.y}");
        }
        else
        {
            Debug.LogWarning("���ǿ� �´� �ػ󵵸� ã�� ���߽��ϴ�.");
        }
    }
}