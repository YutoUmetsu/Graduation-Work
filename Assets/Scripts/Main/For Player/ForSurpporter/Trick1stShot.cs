using UnityEngine;

public class Trick1stShot : MonoBehaviour
{
    [Header("敵に与える反撃ダメージ")]
    [SerializeField] private int damage = 20;

    [Header("弾の設定")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    private PlayerHp playerHP;

    private void Awake()
    {
        playerHP = GetComponent<PlayerHp>();
    }

    private void OnEnable()
    {
        if (playerHP == null)
            playerHP = GetComponent<PlayerHp>();

        if (playerHP != null)
        {
            playerHP.Damaged += OnDamaged;
        }
    }

    private void OnDisable()
    {
        if (playerHP != null)
        {
            playerHP.Damaged -= OnDamaged;
        }
    }

    // プレイヤーがダメージを受けたときに呼ばれる
    private void OnDamaged(int receivedDamage, GameObject attacker)
    {
        // 弾を生成
        Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.identity
        );
    }
}
