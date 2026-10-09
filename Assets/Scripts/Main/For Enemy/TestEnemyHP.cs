using UnityEngine;

public class TestEnemyHP : MonoBehaviour
{
    [Header("HP設定")]
    [SerializeField] private int maxHp = 10;
    private int hp;

    void Start()
    {
        hp = maxHp;
    }

    void Update()
    {
        HpCheck();
    }

    void HpCheck()
    {
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // 共通の汎用ダメージコンポーネントからダメージを取得する
            ProjectileDamage projectile = collision.gameObject.GetComponent<ProjectileDamage>();
            if (projectile != null)
            {
                hp -= projectile.GetDamage();
            }
            else
            {
                // コンポーネントがついていない場合はフォールバックとして5ダメージ
                hp -= 5;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            ProjectileDamage projectile = other.GetComponent<ProjectileDamage>();
            if (projectile != null)
            {
                hp -= projectile.GetDamage();
            }
            else
            {
                hp -= 5;
            }
        }
    }
}