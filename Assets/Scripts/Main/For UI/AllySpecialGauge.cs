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
    [SerializeField] private float chargePerSecond = 10.0f; // 1秒あたりの自動チャージ量

    private const float specialCooldown = 10.0f;

    private int currentGauge = 0;
    private float cooldownTimer = 0.0f;
    private float chargeAccumulator = 0.0f; // 小数点以下のチャージ量を蓄積する変数

    public int CurrentGauge => currentGauge;
    public int MaxGauge => maxGauge;

    private void Start()
    {
        currentGauge = 0;
        cooldownTimer = 0.0f;
        chargeAccumulator = 0.0f;

        gaugeSlider.minValue = 0;
        gaugeSlider.maxValue = maxGauge;
        gaugeSlider.value = currentGauge;

        statusText.text = "";
    }

    private void Update()
    {
        // クールタイム中の処理
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

        // クールタイム終了後、MAXでなければ時間経過で自動チャージ
        if (!IsMax())
        {
            chargeAccumulator += chargePerSecond * Time.deltaTime;

            if (chargeAccumulator >= 1.0f)
            {
                int addAmount = Mathf.FloorToInt(chargeAccumulator);
                IncreaseGauge(addAmount);
                chargeAccumulator -= addAmount;
            }
        }

        // テキスト表示の更新
        if (IsMax())
        {
            statusText.text = "OK!(Push X to SPAct)";
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
        chargeAccumulator = 0.0f; // 端数もリセット
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