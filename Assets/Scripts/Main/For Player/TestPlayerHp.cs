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
    //*‚©‚¢‚Ó‚­ ‚·‚é
    public void Heal(int amount)
    {
        Hp = Mathf.Min(Hp + amount, MaxHp);
    }
    //*‚³‚¢‚¾‚¢HP‚ª ‚Ó‚¦‚é(‚»‚Ì‚Ô‚ñ‚©‚¢‚Ó‚­‚à ‚·‚é‚æI)
    public void IncreaseMaxHp(int amount)
    {
        MaxHp += amount;
        Hp += amount;
    }
}
