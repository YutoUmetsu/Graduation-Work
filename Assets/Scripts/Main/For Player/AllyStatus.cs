using UnityEngine;

public class AllyStatus : MonoBehaviour
{
    [Header("仲間のHP")]
    [SerializeField] private int hp = 10;

    [Header("攻撃力")]
    [SerializeField] private int attackPower = 1;

    [Header("移動速度")]
    [SerializeField] private float moveSpeed = 3.0f;

    [Header("攻撃1")]
    [SerializeField] private GameObject attack1Prefab;
    [SerializeField] private float attack1Cooldown = 1.0f;

    [Header("攻撃2")]
    [SerializeField] private GameObject attack2Prefab;
    [SerializeField] private float attack2Cooldown = 3.0f;

    [Header("必殺技")]
    [SerializeField] private GameObject specialAttackPrefab;

    // 元のステータス
    private int baseAttackPower;
    private float baseMoveSpeed;

    // 必殺技のクールタイムは10秒固定
    private const float specialAttackCooldown = 10.0f;

    // 必殺技中かどうか
    private bool isUsingSpecial = false;

    public int Hp => hp;
    public int AttackPower => attackPower;
    public float MoveSpeed => moveSpeed;

    public float Attack1Cooldown => attack1Cooldown;
    public float Attack2Cooldown => attack2Cooldown;

    public GameObject Attack1Prefab => attack1Prefab;
    public GameObject Attack2Prefab => attack2Prefab;

    public GameObject SpecialAttackPrefab => specialAttackPrefab;
    public float SpecialAttackCooldown => specialAttackCooldown;

    public bool IsUsingSpecial => isUsingSpecial;

    private void Awake()
    {
        // バフ適用前の値を保存
        baseAttackPower = attackPower;
        baseMoveSpeed = moveSpeed;
    }

    /// <summary>
    /// *こうげきりょくを ばいりつで へんこう
    /// </summary>
    /// <param name="multiplier">ばいりつ</param>
    public void ApplyAttackPowerMultiplier(float multiplier)
    {
        attackPower =
            Mathf.RoundToInt(baseAttackPower * multiplier);
    }

    /// <summary>
    /// *いどうそくどを ばいりつで へんこう
    /// </summary>
    /// <param name="multiplier">ばいりつ</param>
    public void ApplyMoveSpeedMultiplier(float multiplier)
    {
        moveSpeed =
            baseMoveSpeed * multiplier;
    }

    /// <summary>
    /// *必殺技を はじめる
    /// </summary>
    public void StartSpecial()
    {
        isUsingSpecial = true;
    }

    /// <summary>
    /// *必殺技を おわる
    /// </summary>
    public void EndSpecial()
    {
        isUsingSpecial = false;
    }

    /// <summary>
    /// *さいだいHPを あとからふやす
    /// </summary>
    /// <param name="amount">ふえる すうち</param>
    public void IncreaseHp(int amount)
    {
        hp += amount;
    }

    /// <summary>
    /// *こうげき１の クールダウンを あとからへらす
    /// </summary>
    /// <param name="amount">へらす すうち</param>
    public void ReduceAttack1Cooldown(float amount)
    {
        attack1Cooldown = Mathf.Max(0.5f, attack1Cooldown - amount);
    }

    /// <summary>
    /// *こうげき2の クールダウンを あとからへらす
    /// </summary>
    /// <param name="amount">へらす すうち</param>
    public void ReduceAttack2Cooldown(float amount)
    {
        attack2Cooldown = Mathf.Max(0.5f, attack2Cooldown - amount);
    }
}