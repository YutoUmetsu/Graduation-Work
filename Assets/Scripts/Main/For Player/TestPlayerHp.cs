using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private int Hp;
    [SerializeField] private int MaxHp = 100;

    // 現在のシールド耐久値
    private int currentShield = 0;

    private PlayerBuffStatus playerBuffStatus;

    // 自動回復用
    private float autoRecoveryTimer = 0.0f;
    private const float AutoRecoveryInterval = 5.0f;

    public int CurrentHp => Hp;
    public int MaxHP => MaxHp;
    public int CurrentShield => currentShield; // UI表示用など

    private void Start()
    {
        playerBuffStatus = GetComponent<PlayerBuffStatus>();

        Hp = MaxHp;
        autoRecoveryTimer = AutoRecoveryInterval;
    }

    private void Update()
    {
        HpCheck();
        AutoRecovery();
    }

    private void HpCheck()
    {
        if (Hp <= 0)
        {
            Destroy(gameObject);
            Debug.Log("プレイヤーが倒れた！");
        }
    }

    /// <summary>
    /// シールド（バリア）を付与する
    /// </summary>
    /// <param name="amount">付与するシールド量</param>
    public void AddShield(int amount)
    {
        currentShield += amount;
        Debug.Log($"シールドを {amount} 付与！ (現在シールド: {currentShield})");
    }

    /// <summary>
    /// シールドを解除・リセットする
    /// </summary>
    public void ClearShield()
    {
        currentShield = 0;
        Debug.Log("シールドが全消失・消滅しました。");
    }

    // 外部のスクリプトから呼び出すダメージ処理（シールド優先消費）
    public void TakeDamage(int Edamage)
    {
        int remainingDamage = Edamage;

        // 1. シールドがある場合は優先的に消費
        if (currentShield > 0)
        {
            if (currentShield >= remainingDamage)
            {
                // シールドだけでダメージを全吸収
                currentShield -= remainingDamage;
                Debug.Log($"シールドがダメージを吸収！ 残りシールド: {currentShield}");
                return;
            }
            else
            {
                // シールドが破壊され、残りのダメージがHPへ
                remainingDamage -= currentShield;
                currentShield = 0;
                Debug.Log("シールドが破壊された！ 残りダメージがHPに与えられます。");
            }
        }

        // 2. 残りダメージを本体HPから削る
        Hp -= remainingDamage;

        Debug.Log("受けたダメージ：" + Edamage + $" (HP直接減算: {remainingDamage})");
        Debug.Log("残りHP：" + Hp);

        if (Hp <= 0)
        {
            Hp = 0;
        }
    }

    /// <summary>
    /// かいふく する
    /// </summary>
    public void Heal(int amount)
    {
        Hp = Mathf.Min(Hp + amount, MaxHp);
    }

    /// <summary>
    /// HPバフを はんえい
    /// </summary>
    public void ApplyHpBuff()
    {
        if (playerBuffStatus == null)
            return;

        int newMaxHp = playerBuffStatus.GetMaxHp();
        int difference = newMaxHp - MaxHp;

        if (difference <= 0)
            return;

        MaxHp = newMaxHp;
        Hp += difference;
    }

    /// <summary>
    /// じどう かいふく
    /// </summary>
    private void AutoRecovery()
    {
        if (playerBuffStatus == null)
            return;

        float recoveryPercent = playerBuffStatus.GetAutoRecoveryPercent();

        if (recoveryPercent <= 0.0f)
            return;

        autoRecoveryTimer -= Time.deltaTime;

        if (autoRecoveryTimer > 0.0f)
            return;

        int recoveryAmount = Mathf.CeilToInt(MaxHp * recoveryPercent);
        Heal(recoveryAmount);

        autoRecoveryTimer = AutoRecoveryInterval;
    }
}