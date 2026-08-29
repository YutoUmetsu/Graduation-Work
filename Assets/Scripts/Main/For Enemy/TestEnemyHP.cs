using UnityEngine;

public class TestEnemyHP : MonoBehaviour
{

    int Hp;
    int MaxHp = 10;

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
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Hp -= 5;
        }
    }
}
