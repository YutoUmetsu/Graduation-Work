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

    public int Hp => hp;
    public int AttackPower => attackPower;
    public float MoveSpeed => moveSpeed;

    public float Attack1Cooldown => attack1Cooldown;
    public float Attack2Cooldown => attack2Cooldown;

    public GameObject Attack1Prefab => attack1Prefab;
    public GameObject Attack2Prefab => attack2Prefab;

    /// <summary>
    /// *さいだいHPを あとからふやす
    /// </summary>
    /// <param name="amount">ふえる すうち</param>
    public void IncreaseHp(int amount)
    {
        hp += amount;
    }

    /// <summary>
    /// *こうげきりょくを あとからふやす
    /// </summary>
    /// <param name="amount">ふえる すうち</param>
    public void IncreaseAttackPower(int amount)
    {
        attackPower += amount;
    }

    /// <summary>
    /// *いどうそくどを あとからふやす
    /// </summary>
    /// <param name="amount">ふえる すうち</param>
    public void IncreaseMoveSpeed(float amount)
    {
        moveSpeed += amount;
    }

    /// <summary>
    /// *こうげき１の クールダウンを あとからへらす
    /// </summary>
    /// <param name="amount">ふえる すうち</param>
    public void ReduceAttack1Cooldown(float amount)
    {
        attack1Cooldown = Mathf.Max(0f, attack1Cooldown - amount);
    }

    /// <summary>
    /// *こうげき2の クールダウンを あとからへらす
    /// </summary>
    /// <param name="amount">ふえる すうち</param>
    public void ReduceAttack2Cooldown(float amount)
    {
        attack2Cooldown = Mathf.Max(0f, attack2Cooldown - amount);
    }
}
