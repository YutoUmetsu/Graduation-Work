using UnityEngine;
using UnityEngine.UIElements;

public class BulletShotArea : MonoBehaviour
{
    [Header("’e‚Ìİ’è")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [Header("”­Ëİ’è")]
    [SerializeField] private float fireInterval = 1f;
    [SerializeField] private float searchRange = 20f;

    [Header("“G‚Ìİ’è")]
    [SerializeField] private string enemyTag = "Enemy";

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        // ˆê”Ô‹ß‚¢“G‚ğ’T‚·
        GameObject nearestEnemy = FindNearestEnemy();

        // “G‚ª‚¢‚È‚¯‚ê‚ÎŒ‚‚½‚È‚¢
        if (nearestEnemy == null)
        {
            return;
        }

   
        // ’e‚ğ¶¬
        Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.identity
        );
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
}
