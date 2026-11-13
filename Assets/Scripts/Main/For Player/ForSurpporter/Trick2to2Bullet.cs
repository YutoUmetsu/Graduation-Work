using UnityEngine;

public class IceStorm : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("消滅時間")]
    [SerializeField] private float destroyTime = 2f;

    private Transform target;
    private bool isMoving = false;

    [SerializeField] private string enemyTag = "Enemy";

    [SerializeField] private float searchRange = 20f;

    [SerializeField] private Transform firePoint;

    public void SetTarget(Transform enemy)
    {
        target = enemy;
        isMoving = true;

        // 2秒後に消滅
        Destroy(gameObject, destroyTime);
    }

    private void Start()
    {
        SetTarget(FindNearestEnemy().transform);
    }

    void Update()
    {
        if (!isMoving)
            return;

        // ターゲットがいなくなった場合
        if (target == null)
        {
            SetTarget(FindNearestEnemy().transform);
            return;
        }

        // 敵の方向へ移動
        Vector2 direction =
            ((Vector2)target.position - (Vector2)transform.position).normalized;

        transform.position +=
            (Vector3)(direction * moveSpeed * Time.deltaTime);
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies =
            GameObject.FindGameObjectsWithTag(enemyTag);

        GameObject nearestEnemy = null;

        float nearestDistance = searchRange;

        foreach (GameObject enemy in enemies)
        {
            float distance =
                Vector3.Distance(
                    firePoint.position,
                    enemy.transform.position
                );

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    //消去用
    private void OnCollisionEnter(Collision collision)
    {
        // ぶつかった相手がEnemyタグか確認
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}