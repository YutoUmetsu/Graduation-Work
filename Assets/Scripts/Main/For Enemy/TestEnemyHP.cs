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
            Debug.Log($"[Enemy] 敵が撃破されました！");
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            ProcessHit(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            ProcessHit(other.gameObject);
        }
    }

    /// <summary>
    /// 弾が当たったときの共通ダメージ処理
    /// </summary>
    private void ProcessHit(GameObject bulletObject)
    {
        ProjectileDamage projectile = bulletObject.GetComponent<ProjectileDamage>();
        int damage = 0;

        if (projectile != null)
        {
            // まだこの弾からダメージを受けていない場合のみ処理する（1ヒット固定）
            if (projectile.CanDamage(this.gameObject))
            {
                damage = projectile.GetDamage();
                hp -= damage;
                Debug.Log($"[Hit - Component] 弾から {damage} のダメージを受けました！ 残りHP: {hp}");
            }
        }
        else
        {
            // コンポーネントがついていない場合のフォールバック
            damage = 5;
            hp -= damage;
            Debug.Log($"[Hit - Fallback] コンポーネントなしのためフォールバックの {damage} ダメージを受けました！ 残りHP: {hp}");
        }
    }
}