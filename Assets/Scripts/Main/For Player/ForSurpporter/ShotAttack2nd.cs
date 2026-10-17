using UnityEngine;

public class ShotAttack2nd : MonoBehaviour
{
    [Header("’e‚ÌÝ’è")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [Header("”­ŽËÝ’è")]
    [SerializeField] private float fireInterval = 1f;
    [SerializeField] private float searchRange = 20f;

    [Header("“G‚ÌÝ’è")]
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
        // ˆê”Ô‹ß‚¢“G‚ð’T‚·
        GameObject nearestEnemy = FindNearestEnemy();

        // “G‚ª‚¢‚È‚¯‚ê‚ÎŒ‚‚½‚È‚¢
        if (nearestEnemy == null)
        {
            return;
        }

        for(int n=0; n<4; n++)
        {
            // ’e‚ð¶¬
            Instantiate(
                bulletPrefab,
                firePoint.position,
                Quaternion.Euler(0, 0 + 90 * n, 0)
            );
        }

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
