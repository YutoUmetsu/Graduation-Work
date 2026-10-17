using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class Attack2ndBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private void Start()
    {
        StartCoroutine(BulletDelete());
    }


    void Update()
    {
        // 弾を前方向に移動
        transform.position += transform.forward * speed * Time.deltaTime;

    }

    private void OnCollisionEnter(Collision collision)
    {
        // ぶつかった相手がEnemyタグか確認
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator BulletDelete()
    {
        yield return new
        WaitForSeconds(2f);
        Destroy(gameObject);

    }
}
