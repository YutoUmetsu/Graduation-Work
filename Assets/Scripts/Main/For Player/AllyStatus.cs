using UnityEngine;

public class AllyStatus : MonoBehaviour
{
    [Header("’‡ŠÔ‚ÌHP")]
    [SerializeField] private int hp = 10;

    [Header("UŒ‚—Í")]
    [SerializeField] private int attackPower = 1;

    [Header("ˆÚ“®‘¬“x")]
    [SerializeField] private float moveSpeed = 3.0f;

    [Header("UŒ‚1")]
    [SerializeField] private GameObject attack1Prefab;
    [SerializeField] private float attack1Cooldown = 1.0f;

    [Header("UŒ‚2")]
    [SerializeField] private GameObject attack2Prefab;
    [SerializeField] private float attack2Cooldown = 3.0f;

    [Header("•KE‹Z")]
    [SerializeField] private GameObject specialAttackPrefab;

    // •KE‹Z‚ÌƒN[ƒ‹ƒ^ƒCƒ€‚Í10•bŒÅ’è
    private const float specialAttackCooldown = 10.0f;

    // •KE‹Z’†‚©‚Ç‚¤‚©
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

    // •KE‹Z’†‚©‚Ç‚¤‚©
    public bool IsUsingSpecial => isUsingSpecial;

    /// <summary>
    /// *•KE‹Z‚ğ ‚Í‚¶‚ß‚é
    /// </summary>
    public void StartSpecial()
    {
        isUsingSpecial = true;
    }

    /// <summary>
    /// *•KE‹Z‚ğ ‚¨‚í‚é
    /// </summary>
    public void EndSpecial()
    {
        isUsingSpecial = false;
    }

    /// <summary>
    /// *‚³‚¢‚¾‚¢HP‚ğ ‚ ‚Æ‚©‚ç‚Ó‚â‚·
    /// </summary>
    /// <param name="amount">‚Ó‚¦‚é ‚·‚¤‚¿</param>
    public void IncreaseHp(int amount)
    {
        hp += amount;
    }

    /// <summary>
    /// *‚±‚¤‚°‚«‚è‚å‚­‚ğ ‚ ‚Æ‚©‚ç‚Ó‚â‚·
    /// </summary>
    /// <param name="amount">‚Ó‚¦‚é ‚·‚¤‚¿</param>
    public void IncreaseAttackPower(int amount)
    {
        attackPower += amount;
    }

    /// <summary>
    /// *‚¢‚Ç‚¤‚»‚­‚Ç‚ğ ‚ ‚Æ‚©‚ç‚Ó‚â‚·
    /// </summary>
    /// <param name="amount">‚Ó‚¦‚é ‚·‚¤‚¿</param>
    public void IncreaseMoveSpeed(float amount)
    {
        moveSpeed += amount;
    }

    /// <summary>
    /// *‚±‚¤‚°‚«‚P‚Ì ƒN[ƒ‹ƒ_ƒEƒ“‚ğ ‚ ‚Æ‚©‚ç‚Ö‚ç‚·
    /// </summary>
    /// <param name="amount">‚Ö‚ç‚· ‚·‚¤‚¿</param>
    public void ReduceAttack1Cooldown(float amount)
    {
        attack1Cooldown = Mathf.Max(0f, attack1Cooldown - amount);
    }

    /// <summary>
    /// *‚±‚¤‚°‚«2‚Ì ƒN[ƒ‹ƒ_ƒEƒ“‚ğ ‚ ‚Æ‚©‚ç‚Ö‚ç‚·
    /// </summary>
    /// <param name="amount">‚Ö‚ç‚· ‚·‚¤‚¿</param>
    public void ReduceAttack2Cooldown(float amount)
    {
        attack2Cooldown = Mathf.Max(0f, attack2Cooldown - amount);
    }
}