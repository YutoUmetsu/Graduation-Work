using UnityEngine;

public class AllyAreaAttack : MonoBehaviour
{
    [Header("”­Ë’n“_")]
    [SerializeField] private Transform firePoint;

    [Header("UŒ‚İ’è")]
    [SerializeField] private bool useAttack1 = true;
    [SerializeField] private bool useAttack2 = true;

    [Header("“G‚Ìİ’è")]
    [SerializeField] private string enemyTag = "Enemy";
    [SerializeField] private float searchRange = 20f;

    private AllyStatus allyStatus;

    private float attack1Timer;
    private float attack2Timer;

    private void Start()
    {
        allyStatus = GetComponent<AllyStatus>();
    }

    private void Update()
    {
        if (allyStatus == null)
            return;

        if (useAttack1)
        {
            Attack1();
        }

        if (useAttack2)
        {
            Attack2();
        }
    }
    /// <summary>
    /// *1‚Â‚ß‚Ì ‚±‚¤‚°‚«‚µ‚å‚è
    /// </summary>
    private void Attack1()
    {
        attack1Timer += Time.deltaTime;

        if (attack1Timer < allyStatus.Attack1Cooldown)
            return;

        Shoot(allyStatus.Attack1Prefab);

        attack1Timer = 0f;
    }
    /// <summary>
    /// *2‚Â‚ß‚Ì ‚±‚¤‚°‚«‚µ‚å‚è
    /// </summary>
    private void Attack2()
    {
        attack2Timer += Time.deltaTime;

        if (attack2Timer < allyStatus.Attack2Cooldown)
            return;

        Shoot(allyStatus.Attack2Prefab);

        attack2Timer = 0f;
    }
    /// <summary>
    /// *‚¤‚Â ‚µ‚å‚è
    /// </summary>
    /// <param name="attackPrefab"></param>
    private void Shoot(GameObject attackPrefab)
    {
        if (attackPrefab == null || firePoint == null)
            return;

        if (!IsEnemyInRange())
            return;//*‚¿‚©‚­‚É‚Ä‚«‚ª‚¢‚È‚¢‚È‚ç ‚µ‚È‚¢

        //*‚½‚Ü‚ğ ‚Â‚­‚é
        Instantiate(
            attackPrefab,
            firePoint.position,
            Quaternion.identity
        );
    }
    /// <summary>
    /// *‚¿‚©‚­‚Ì‚Ä‚«‚ğ ‚³‚ª‚· ‚Í‚ñ‚¢
    /// </summary>
    /// <returns></returns>
    private bool IsEnemyInRange()
    {
        GameObject[] enemies =
            GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (GameObject enemy in enemies)
        {
            float distance =
                Vector3.Distance(
                    firePoint.position,
                    enemy.transform.position
                );

            if (distance < searchRange)
            {
                return true;
            }
        }

        return false;
    }
}