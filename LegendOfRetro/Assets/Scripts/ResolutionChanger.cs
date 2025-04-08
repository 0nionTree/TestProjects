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
        Debug.Log($"플랫폼: {Application.platform}");
        Debug.Log($"스크린 크기: {Screen.width}x{Screen.height}");
        Debug.Log($"해상도: {Screen.currentResolution.width}x{Screen.currentResolution.height}");
        Debug.Log($"DPI: {Screen.dpi}");
        float screenAspect = (float)Screen.width / Screen.height;
        Debug.Log("현재 화면 비율: " + screenAspect);

        // 비율 기반으로 UI 동적 조정 가능
        if (screenAspect < 1.5f)
        {
            // 4:3 기기일 경우
        }
        else if (screenAspect < 1.8f)
        {
            // 16:10 기기일 경우
        }
        else
        {
            // 16:9 이상 (대부분의 최신 스마트폰)
        }
    }

    // 해상도 변경 함수
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

    // 공통 처리 함수
    private void SetResolutionAuto(bool preferBigger)
    {
        // 현재 화면 비율 계산 (가로 / 세로)
        float currentAspect = (float)Screen.width / Screen.height;

        // 현재 화면 면적 (크기 비교용)
        int currentArea = Screen.width * Screen.height;

        // 가장 유사한 해상도를 저장할 변수
        Vector2Int? bestMatch = null;
        float bestAspectDiff = float.MaxValue;

        // 미리 정의된 해상도 리스트 순회
        foreach (var res in resolutions)
        {
            int area = res.x * res.y;
            float aspect = (float)res.x / res.y;
            float aspectDiff = Mathf.Abs(aspect - currentAspect);

            // 현재보다 큰 해상도를 찾는 경우
            // or 작은 해상도를 찾는 경우로 분기
            bool isValid = preferBigger ? area >= currentArea : area <= currentArea;

            // 조건에 부합하고, 비율 차이가 더 작은 해상도면 갱신
            if (isValid && aspectDiff < bestAspectDiff)
            {
                bestMatch = res;
                bestAspectDiff = aspectDiff;
            }
        }

        // 최종 선택된 해상도 적용
        if (bestMatch.HasValue)
        {
            // FullScreenWindow 모드는 창 없는 전체화면 모드
            Screen.SetResolution(bestMatch.Value.x, bestMatch.Value.y, FullScreenMode.FullScreenWindow);
            Debug.Log($"[해상도 자동 설정 완료] {bestMatch.Value.x} x {bestMatch.Value.y}");
        }
        else
        {
            Debug.LogWarning("조건에 맞는 해상도를 찾지 못했습니다.");
        }
    }
}