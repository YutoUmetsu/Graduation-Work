using UnityEngine;

public class ShieldBashAttack : MonoBehaviour
{
    [Header("’eEƒAƒNƒVƒ‡ƒ“İ’è")]
    [SerializeField] private GameObject bashPrefab;
    [SerializeField] private Transform firePoint;

    [Header("”­Ëİ’è")]
    [SerializeField] private float fireInterval = 2.0f; // UŒ‚ŠÔŠu
    [SerializeField] private float searchRange = 15.0f; // õ“G”ÍˆÍ

    [Header("“G‚Ìİ’è")]
    [SerializeField] private string enemyTag = "Enemy";

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireInterval)
        {
            ShootBash();
            timer = 0f;
        }
    }

    void ShootBash()
    {
        // ‹ß‚­‚Ì“G‚ğ’T‚·
        GameObject nearestEnemy = FindNearestEnemy();

        // “G‚ª‚¢‚È‚¯‚ê‚ÎŒ‚‚½‚È‚¢
        if (nearestEnemy == null)
        {
            return;
        }

        // “G‚Ì•ûŒü‚ğŒü‚¢‚Ä‚©‚çƒoƒbƒVƒ…‚ğ”­¶‚³‚¹‚éi‚Ü‚½‚Í©g‚Ì³–Êj
        if (firePoint != null)
        {
            Vector3 direction = (nearestEnemy.transform.position - firePoint.position).normalized;
            direction.y = 0; // ‚‚³•ûŒü‚Í–³‹‚µ‚Ä…•½•ûŒü‚ÉŒü‚«‚ğ‡‚í‚¹‚é
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                Instantiate(bashPrefab, firePoint.position, lookRotation);
            }
        }
        else
        {
            Instantiate(bashPrefab, transform.position, transform.rotation);
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject nearestEnemy = null;
        float nearestDistance = searchRange;

        foreach (GameObject enemy in enemies)
        {
            Vector3 basePos = firePoint != null ? firePoint.position : transform.position;
            float distance = Vector3.Distance(basePos, enemy.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
}