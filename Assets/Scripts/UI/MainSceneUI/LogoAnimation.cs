using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class LogoAnimation : MonoBehaviour
{
    [Header("로고 설정")]
    public TextMeshProUGUI mainLogo;
    public TextColorGradient textColorGradient; // 텍스트 색상 그라데이션 컴포넌트
    public float fadeDuration = 1f; // 페이드 인/아웃 시간
    public float displayTime = 1f;  // 유지 시간
    public Vector3 maxScale = new Vector3(1.0f, 1.0f, 1.0f); // 목표 스케일

    [Header("배경 애니메이션")]
    public GameObject background; // 배경 애니메이션 오브젝트
    public Vector3 rotationAxis = Vector3.forward;
    public float RotateSpeed; // 배경 회전 속도

    private void Start()
    {
        // 게임 시작 시 한 번만 코루틴 실행
        StartCoroutine(LoopFadeLogo());
        StartCoroutine(RotaionBG());
    }

    IEnumerator RotaionBG() 
    { 
        
        while (true)
        {
           background.transform.Rotate(rotationAxis, RotateSpeed * Time.deltaTime); // 배경 회전
           yield return null; // 다음 프레임까지 대기
        }
        
    }

    IEnumerator LoopFadeLogo()
    {
        Color c = mainLogo.color;

        while (true) // 무한 반복
        {
            // 🔹 페이드 인
            float time = 0f;
            while (time < fadeDuration)
            {
                time += Time.deltaTime;
                float progress = time / fadeDuration;

                c.a = Mathf.Lerp(0f, 1f, time / fadeDuration);
                mainLogo.color = c;

                mainLogo.transform.localScale = Vector3.Lerp(Vector3.zero, maxScale, progress);

                yield return null;
            }

            // 🔹 유지
            yield return new WaitForSeconds(displayTime);

            // 🔹 페이드 아웃
            time = 0f;
            while (time < fadeDuration)
            {
                time += Time.deltaTime;
                float progress = time / fadeDuration;

                c.a = Mathf.Lerp(1f, 0f, time / fadeDuration);
                mainLogo.color = c;

                mainLogo.transform.localScale = Vector3.Lerp(maxScale, Vector3.zero, progress);

                yield return null;
            }

            // 🔹 다시 반복
            yield return new WaitForSeconds(0.2f); // 약간의 텀을 줄 수도 있음
        }
    }
}
