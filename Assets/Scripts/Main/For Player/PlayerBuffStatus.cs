using UnityEngine;

public class PlayerBuffStatus : MonoBehaviour
{
    [Header("バフレベル")]
    [SerializeField] private int hpLevel = 0;
    [SerializeField] private int attackPowerLevel = 0;
    [SerializeField] private int moveSpeedLevel = 0;
    [SerializeField] private int attackSpeedLevel = 0;
    [SerializeField] private int specialCooldownLevel = 0;
    [SerializeField] private int expLevel = 0;
    [SerializeField] private int autoRecoveryLevel = 0;

    private const int MaxBuffLevel = 10;

    // 初期最大HP
    private const int BaseMaxHp = 100;

    // 最大HP
    private const int MaxHp = 1000;

    // 攻撃力
    private const float BaseAttackPowerMultiplier = 1.0f;
    private const float MaxAttackPowerMultiplier = 1.25f;

    // 移動速度
    private const float BaseMoveSpeedMultiplier = 1.0f;
    private const float MaxMoveSpeedMultiplier = 1.5f;

    // 攻撃クールタイム短縮
    private const float MaxAttackCooldownReduction = 1.5f;
    private const float MinAttackCooldown = 0.5f;

    // 取得経験値
    private const float MaxExpMultiplier = 1.5f;

    // 自動回復
    private const float AutoRecoveryInterval = 5.0f;
    private const float MaxAutoRecoveryPercent = 0.05f;

    public int HpLevel => hpLevel;
    public int AttackPowerLevel => attackPowerLevel;
    public int MoveSpeedLevel => moveSpeedLevel;
    public int AttackSpeedLevel => attackSpeedLevel;
    public int SpecialCooldownLevel => specialCooldownLevel;
    public int ExpLevel => expLevel;
    public int AutoRecoveryLevel => autoRecoveryLevel;

    /// <summary>
    /// *バフのレベルを あげる
    /// </summary>
    public void IncreaseHpLevel()
    {
        hpLevel = Mathf.Min(MaxBuffLevel, hpLevel + 1);
    }

    public void IncreaseAttackPowerLevel()
    {
        attackPowerLevel = Mathf.Min(MaxBuffLevel, attackPowerLevel + 1);
    }

    public void IncreaseMoveSpeedLevel()
    {
        moveSpeedLevel = Mathf.Min(MaxBuffLevel, moveSpeedLevel + 1);
    }

    public void IncreaseAttackSpeedLevel()
    {
        attackSpeedLevel = Mathf.Min(MaxBuffLevel, attackSpeedLevel + 1);
    }

    public void IncreaseSpecialCooldownLevel()
    {
        specialCooldownLevel = Mathf.Min(MaxBuffLevel, specialCooldownLevel + 1);
    }

    public void IncreaseExpLevel()
    {
        expLevel = Mathf.Min(MaxBuffLevel, expLevel + 1);
    }

    public void IncreaseAutoRecoveryLevel()
    {
        autoRecoveryLevel = Mathf.Min(MaxBuffLevel, autoRecoveryLevel + 1);
    }

    /// <summary>
    /// *げんざいの さいだいHPを かえす
    /// </summary>
    public int GetMaxHp()
    {
        float rate =
            (float)hpLevel / MaxBuffLevel;

        return Mathf.RoundToInt(
            Mathf.Lerp(BaseMaxHp, MaxHp, rate)
        );
    }

    /// <summary>
    /// *こうげきりょくの ばいりつを かえす
    /// </summary>
    public float GetAttackPowerMultiplier()
    {
        float rate =
            (float)attackPowerLevel / MaxBuffLevel;

        return Mathf.Lerp(
            BaseAttackPowerMultiplier,
            MaxAttackPowerMultiplier,
            rate
        );
    }

    /// <summary>
    /// *いどうそくどの ばいりつを かえす
    /// </summary>
    public float GetMoveSpeedMultiplier()
    {
        float rate =
            (float)moveSpeedLevel / MaxBuffLevel;

        return Mathf.Lerp(
            BaseMoveSpeedMultiplier,
            MaxMoveSpeedMultiplier,
            rate
        );
    }

    /// <summary>
    /// *こうげきクールタイムの へらす じかんを かえす
    /// </summary>
    public float GetAttackCooldownReduction()
    {
        float rate =
            (float)attackSpeedLevel / MaxBuffLevel;

        return MaxAttackCooldownReduction * rate;
    }

    /// <summary>
    /// *こうげきクールタイムを かえす
    /// </summary>
    public float GetAttackCooldown(float originalCooldown)
    {
        float cooldown =
            originalCooldown - GetAttackCooldownReduction();

        return Mathf.Max(
            MinAttackCooldown,
            cooldown
        );
    }

    /// <summary>
    /// *取得経験値の ばいりつを かえす
    /// </summary>
    public float GetExpMultiplier()
    {
        float rate =
            (float)expLevel / MaxBuffLevel;

        return Mathf.Lerp(
            1.0f,
            MaxExpMultiplier,
            rate
        );
    }

    /// <summary>
    /// *自動回復の かいふくりょうを かえす
    /// </summary>
    public float GetAutoRecoveryPercent()
    {
        float rate =
            (float)autoRecoveryLevel / MaxBuffLevel;

        return MaxAutoRecoveryPercent * rate;
    }

    /// <summary>
    /// *自動回復の かんかくを かえす
    /// </summary>
    public float GetAutoRecoveryInterval()
    {
        return AutoRecoveryInterval;
    }
}