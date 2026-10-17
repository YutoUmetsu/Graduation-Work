using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Trick1stShot : MonoBehaviour
{
    [Header("弾の設定")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    bool cooltimeUp;

    public PlayerHp playerHP;

    private void Start()
    {
        cooltimeUp = true;   
    }

    // プレイヤーがダメージを受けたときに呼ばれる
    public void OnDamaged(GameObject attacker)
    {
        if (cooltimeUp == true)
        {
            //ここにスタンのプログラムを入れる
            // 弾を生成
            Instantiate(
                bulletPrefab,
                firePoint.position,
                Quaternion.identity
            );       
            cooltimeUp = false;
            StartCoroutine(CountCooltime());
        }
 
    }

    IEnumerator CountCooltime()
    {
        yield return new
        WaitForSeconds(4f);
        cooltimeUp = true;
    }
}
