using UnityEngine;

public class Enemy_02 : MonoBehaviour
{
    PlayerHp player;

    private float StanTime;

    [SerializeField]
    private float AttackChargeTime = 3;
    private float AttackChargeCount = 0;

    [Header("HP設定")]
    [SerializeField] private int maxHp = 10;
    public int HP; // 外部やインスペクター確認用

    [SerializeField]
    private int AttackPow;

    [SerializeField]
    private float MoveTime = 1;
    private float MoveCount = 0;

    private Vector3 PlayerPos;
    private Vector3 MyPos;

    void Start()
    {
        player = FindObjectOfType<PlayerHp>();
        MyPos = transform.position;
        HP = maxHp; // 初期化
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Debug.Log("[Enemy_02] 敵が撃破されました！");
            Destroy(gameObject);
        }

        if (StanTime > 0)
        {
            StanTime -= Time.deltaTime;
            return;
        }

        if (AttackChargeCount < AttackChargeTime)
        {
            AttackChargeCount += Time.deltaTime;
            if (player != null)
            {
                PlayerPos = player.transform.position;
            }
        }
        else
        {
            if (MoveCount < MoveTime)
            {
                MoveCount += Time.deltaTime;
                if (MoveCount > MoveTime)
                    MoveCount = MoveTime;

                transform.position = Vector3.Lerp(MyPos, PlayerPos, MoveCount / MoveTime);
            }
            else
            {
                AttackChargeCount = 0;
                MoveCount = 0;
                MyPos = transform.position;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHp>().TakeDamage(AttackPow, gameObject);
            //Playerに毒を付与
        }
        else if (other.CompareTag("Bullet"))
        {
            ProcessHit(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            ProcessHit(collision.gameObject);
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
                Debug.Log($"[Enemy_02 Hit] 弾から {damage} のダメージ！ 残りHP: {HP}");
            }
        }
        else
        {
            damage = 5;
            HP -= damage;
            Debug.Log($"[Enemy_02 Hit] フォールバック {damage} ダメージ！ 残りHP: {HP}");
        }
    }

    /// <summary>
    /// 外部（必殺技や他のスキル）から直接ダメージを与える共通メソッド
    /// </summary>
    public void TakeDamage(int damage)
    {
        HP -= damage;
        Debug.Log($"[Enemy_02 External Damage] 外部から {damage} のダメージ！ 残りHP: {HP}");
    }

    public void EnemyStan(float num)
    {
        StanTime = num;
    }
}