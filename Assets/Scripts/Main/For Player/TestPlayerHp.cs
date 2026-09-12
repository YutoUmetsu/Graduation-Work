using UnityEngine;

public class TestPlayerHp : MonoBehaviour
{

    [SerializeField] private int Hp;
    [SerializeField] private int MaxHp = 10000;

    public int CurrentHp => Hp;
    public int MaxHP => MaxHp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hp = MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        HpCheck();
    }

    void HpCheck()
    {
        if (Hp <= 0)
        {
            Destroy(this.gameObject);
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
    /// *Ç©Ç¢Ç”Ç≠ Ç∑ÇÈ
    /// </summary>
    /// <param name="amount">*Ç©Ç¢Ç”Ç≠ ÇËÇÂÇ§</param>
    public void Heal(int amount)
    {
        Hp = Mathf.Min(Hp + amount, MaxHp);
    }
    /// <summary>
    /// *Ç≥Ç¢ÇæÇ¢HPÇ™ Ç”Ç¶ÇÈ(ÇªÇÃÇ‘ÇÒÇ©Ç¢Ç”Ç≠Ç‡ Ç∑ÇÈÇÊÅI)
    /// </summary>
    /// <param name="amount">*Ç”Ç¶ÇÈ ÇËÇÂÇ§</param>
    public void IncreaseMaxHp(int amount)
    {
        MaxHp += amount;
        Hp += amount;
    }
}
