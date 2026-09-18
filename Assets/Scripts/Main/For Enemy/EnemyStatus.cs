using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private int hp = 10;

    [Header("攻撃力")]
    [SerializeField] private int attackPower = 1;

    [Header("撃破時EXP")]
    [SerializeField] private int exp = 10;

    [Header("攻撃方法")]
    [SerializeField] private AttackType attackType;

    [Header("接近攻撃")]
    [SerializeField] private GameObject meleeAttackPrefab;

    [Header("突撃攻撃")]
    [SerializeField] private GameObject chargeAttackPrefab;

    [Header("射撃攻撃")]
    [SerializeField] private GameObject rangedAttackPrefab;

    public int Hp => hp;
    public int AttackPower => attackPower;
    public int Exp => exp;

    public AttackType AttackType => attackType;

    public GameObject MeleeAttackPrefab => meleeAttackPrefab;
    public GameObject ChargeAttackPrefab => chargeAttackPrefab;
    public GameObject RangedAttackPrefab => rangedAttackPrefab;
    /// <summary>
    /// *ダメージを くらった
    /// </summary>
    /// <param name="amount">*うけた ダメージ</param>
    public void TakeDamage(int amount)
    {
        hp -= amount;

        if (hp <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// *し  
    /// </summary>
    private void Die()
    {
        // 後でプレイヤーにEXPを渡す
        Destroy(gameObject);
    }
    /// <summary>
    /// *さいだいHPを ふやす
    /// </summary>
    /// <param name="amount">*ふえる りょう</param>
    public void IncreaseHp(int amount)
    {
        hp += amount;
    }

    /// <summary>
    /// *こうげきりょくを ふやす
    /// </summary>
    /// <param name="amount">*ふえる りょう</param>
    public void IncreaseAttackPower(int amount)
    {
        attackPower += amount;
    }
    /// <summary>
    /// *もってるけいけんちを ふやす
    /// </summary>
    /// <param name="amount">*ふえる りょう</param>
    public void IncreaseExp(int amount)
    {
        exp += amount;
    }
}
/// <summary>
/// *もってる こうげきは どれ？
/// </summary>
public enum AttackType
{
    Melee,
    Charge,
    Ranged
}