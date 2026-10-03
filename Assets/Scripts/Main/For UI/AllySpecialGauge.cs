using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AllySpecialGauge : MonoBehaviour
{
    [Header("ゲージ")]
    [SerializeField] private Slider gaugeSlider;

    [Header("表示")]
    [SerializeField] private TMP_Text statusText;

    [Header("ゲージ設定")]
    [SerializeField] private int maxGauge = 100;

    private const float specialCooldown = 10.0f;

    private int currentGauge = 0;
    private float cooldownTimer = 0.0f;

    public int CurrentGauge => currentGauge;
    public int MaxGauge => maxGauge;

    private void Start()
    {
        currentGauge = 0;
        cooldownTimer = 0.0f;

        gaugeSlider.minValue = 0;
        gaugeSlider.maxValue = maxGauge;
        gaugeSlider.value = currentGauge;

        statusText.text = "";
    }

    private void Update()
    {
        if (cooldownTimer > 0.0f)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer < 0.0f)
            {
                cooldownTimer = 0.0f;
            }

            statusText.text = cooldownTimer.ToString("F1");
            return;
        }

        if (IsMax())
        {
            statusText.text = "OK!";
        }
        else
        {
            statusText.text = "SP Charging...";
        }
    }

    /// <summary>
    /// 必殺技ゲージを増やす
    /// </summary>
    public void IncreaseGauge(int amount)
    {
        currentGauge = Mathf.Min(maxGauge, currentGauge + amount);
        gaugeSlider.value = currentGauge;
    }

    /// <summary>
    /// 必殺技ゲージを減らす
    /// </summary>
    public void DecreaseGauge(int amount)
    {
        currentGauge = Mathf.Max(0, currentGauge - amount);
        gaugeSlider.value = currentGauge;
    }

    /// <summary>
    /// 必殺技を使ってゲージとクールタイムをリセットする
    /// </summary>
    public void ResetGauge()
    {
        currentGauge = 0;
        gaugeSlider.value = currentGauge;

        cooldownTimer = specialCooldown;
    }

    /// <summary>
    /// 必殺技ゲージがMAXか確認する
    /// </summary>
    public bool IsMax()
    {
        return currentGauge >= maxGauge;
    }

    /// <summary>
    /// 必殺技を使用可能か確認する
    /// </summary>
    public bool CanUseSpecial()
    {
        return cooldownTimer <= 0.0f && IsMax();
    }
}
