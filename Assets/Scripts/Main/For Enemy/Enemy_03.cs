using UnityEngine;

public class Enemy_03 : MonoBehaviour
{
    PlayerHp player;

    private float StanTime;

    [SerializeField]
    private GameObject Bullet;

    [SerializeField]
    private float AttackChargeTime = 3;
    private float AttackChargeCount = 0;

    [Header("HP設定")]
    [SerializeField] private int maxHp = 10;
    public int HP; // 外部やインスペクター確認用

    [SerializeField]
    private int AttackPow = 1;

    private Vector3 PlayerPos;

    void Start()
    {
        player = FindObjectOfType<PlayerHp>();
        HP = maxHp; // 初期化
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Debug.Log("[Enemy_03] 敵が撃破されました！");
            Destroy(gameObject);
        }

        if (player != null)
        {
            transform.LookAt(player.transform.position);

            if (Vector3.Distance(player.transform.position, transform.position) > 3)
            {
                transform.position += transform.forward * Time.deltaTime;
            }
            else
            {
                if (AttackChargeCount > 0)
                {
                    AttackChargeCount -= Time.deltaTime;
                }
                else
                {
                    AttackChargeCount = AttackChargeTime;
                    if (Bullet != null)
                    {
                        Instantiate(Bullet, transform.position, transform.rotation);
                    }
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHp>().TakeDamage(AttackPow, gameObject);
            //Playerに毒を与える処理
        }
        else if (collision.gameObject.CompareTag("Bullet"))
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
            if (projectile.CanDamage(this.gameObject))
            {
                damage = projectile.GetDamage();
                HP -= damage;
                Debug.Log($"[Enemy_03 Hit] 弾から {damage} のダメージ！ 残りHP: {HP}");
            }
        }
        else
        {
            damage = 5;
            HP -= damage;
            Debug.Log($"[Enemy_03 Hit] フォールバック {damage} ダメージ！ 残りHP: {HP}");
        }
    }

    /// <summary>
    /// 外部（必殺技や他のスキル）から直接ダメージを与える共通メソッド
    /// </summary>
    public void TakeDamage(int damage)
    {
        HP -= damage;
        Debug.Log($"[Enemy_03 External Damage] 外部から {damage} のダメージ！ 残りHP: {HP}");
    }

    public void EnemyStan(float num)
    {
        StanTime = num;
    }
}