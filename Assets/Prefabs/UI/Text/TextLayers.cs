using System;
using System.Collections;
using TMPro;
using UnityEngine;

[System.Serializable]
public class TextLayers : MonoBehaviour
{
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI shadowText1;
    public TextMeshProUGUI shadowText2;
    public float animationDuration = 0.8f; // 动画持续时间
    public float maxFontScale = 1.5f; 
    public float normalFontScale = 1.2f;// 最大缩放尺寸
    private  Vector3 originalScale; // 初始尺寸

    private void Awake()
    {
        originalScale = mainText.transform.localScale;
    }

    // 逐渐增加分数
    public void UpdateScoreIncrementally(int targetScore)
    {
        StartCoroutine(IncrementScore(targetScore));
    }
    
    // 直接更新Combo的值并触发动画
    public void UpdateCombo(int comboCount)
    {
        mainText.text = comboCount.ToString();
        shadowText1.text = comboCount.ToString();
        shadowText2.text = comboCount.ToString();
        StartCoroutine(PlayScaleAnimation());
    }


    // score increment coroutine
    private IEnumerator IncrementScore(int targetScore)
    {
        float times = animationDuration/0.08f;
        // 启动动画
        StartCoroutine(PlayScaleAnimation());

        int currentScore = int.Parse(mainText.text);
        
        while (currentScore < targetScore)
        {
            int increment = Mathf.CeilToInt((targetScore - currentScore) / times);
            currentScore += increment;

            // 更新文本但不再次触发动画
            UpdateTextWithoutAnimation(currentScore.ToString());

            yield return new WaitForSeconds(0.04f); // 更新频率
        }

        // 确保最终分数正确
        if (currentScore != targetScore)
        {
            UpdateTextWithoutAnimation(targetScore.ToString());
        }
    }

    // 更新文本但不触发动画
    private void UpdateTextWithoutAnimation(string text)
    {
        mainText.text = text;
        shadowText1.text = text;
        shadowText2.text = text;
    }

    // 播放缩放动画
    private IEnumerator PlayScaleAnimation()
    {
        Vector3 maxScale = originalScale * maxFontScale; // 1.5倍最大缩放尺寸
        Vector3 targetScale = originalScale * normalFontScale; // 目标缩放尺寸

        // 确保目标缩放尺寸不超过最大尺寸
        targetScale = Vector3.Min(targetScale, maxScale);

        float timer = 0;
        while (timer <= animationDuration / 2)
        {
            Vector3 newScale = Vector3.Lerp(originalScale, targetScale, timer / (animationDuration / 2));
            mainText.transform.localScale = newScale;
            shadowText1.transform.localScale = newScale;
            shadowText2.transform.localScale = newScale;

            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0;
        while (timer <= animationDuration / 2)
        {
            Vector3 newScale = Vector3.Lerp(targetScale, originalScale, timer / (animationDuration / 2));
            mainText.transform.localScale = newScale;
            shadowText1.transform.localScale = newScale;
            shadowText2.transform.localScale = newScale;

            timer += Time.deltaTime;
            yield return null;
        }

        mainText.transform.localScale = originalScale;
        shadowText1.transform.localScale = originalScale;
        shadowText2.transform.localScale = originalScale;
    }


}
