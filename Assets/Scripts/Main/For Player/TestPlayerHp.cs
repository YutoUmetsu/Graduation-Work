using UnityEngine;

public class TestPlayerHp : MonoBehaviour
{
    [SerializeField] private int Hp;
    [SerializeField] private int MaxHp = 100;

    private PlayerBuffStatus playerBuffStatus;

    // 自動回復用
    private float autoRecoveryTimer = 0.0f;

    private const float AutoRecoveryInterval = 5.0f;

    public int CurrentHp => Hp;
    public int MaxHP => MaxHp;

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
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Hp -= 1;
        }
    }

    /// <summary>
    /// *かいふく する
    /// </summary>
    /// <param name="amount">*かいふく りょう</param>
    public void Heal(int amount)
    {
        Hp = Mathf.Min(Hp + amount, MaxHp);
    }

    /// <summary>
    /// *HPバフを はんえい
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

        // 増えた最大HPのぶんだけ回復
        Hp += difference;
    }

    /// <summary>
    /// *じどう かいふく
    /// </summary>
    private void AutoRecovery()
    {
        if (playerBuffStatus == null)
            return;

        // 自動回復バフがないなら何もしない
        float recoveryPercent =
            playerBuffStatus.GetAutoRecoveryPercent();

        if (recoveryPercent <= 0.0f)
            return;

        autoRecoveryTimer -= Time.deltaTime;

        if (autoRecoveryTimer > 0.0f)
            return;

        // 最大HPの○％回復
        int recoveryAmount =
            Mathf.CeilToInt(MaxHp * recoveryPercent);

        Heal(recoveryAmount);

        // 5秒後にまた回復
        autoRecoveryTimer = AutoRecoveryInterval;
    }
}