using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Trick1stShot : MonoBehaviour
{
    [Header("弾の設定")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    private PlayerHp playerHP;

    bool cooltimeUp = true;
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
        if (cooltimeUp)
        {
            // 弾を生成
            Instantiate(
                bulletPrefab,
                firePoint.position,
                Quaternion.identity
            );
        }
        cooltimeUp = false;
        StartCoroutine(CountCooltime());
    }

    IEnumerator CountCooltime()
    {
        yield return new
        WaitForSeconds(4f);
        cooltimeUp = true;
    }
}
