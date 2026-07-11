using UnityEngine;

public class MonsterShooter : MonoBehaviour
{
    [Header("射撃設定")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootInterval = 1.0f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null)
        {
            Debug.LogWarning("弾プレハブが設定されていません。");
            return;
        }

        Transform spawnPoint = firePoint != null ? firePoint : transform;

        Instantiate(
            bulletPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );
    }
}