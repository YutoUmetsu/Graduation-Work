using UnityEngine;
using UnityEngine.InputSystem; // 新Input Systemを使うための名前空間

public class AllySingleSpecial : MonoBehaviour
{
    [Header("発射地点")]
    [SerializeField] private Transform firePoint;

    [Header("敵の設定")]
    [SerializeField] private string enemyTag = "Enemy";
    [SerializeField] private float searchRange = 20f;

    private AllyStatus allyStatus;
    private AllySpecialGauge specialGauge;

    private void Start()
    {
        allyStatus = GetComponent<AllyStatus>();

        // シーンに1つだけある共通ゲージを取得
        specialGauge = FindFirstObjectByType<AllySpecialGauge>();
    }

    private void Update()
    {
        if (allyStatus == null || specialGauge == null)
            return;

        // ★修正: 新Input SystemでXキーが押されたかチェック
        if (Keyboard.current != null && Keyboard.current.xKey.wasPressedThisFrame)
        {
            UseSpecial();
        }
    }

    /// <summary>
    /// *ひっさつわざを つかう
    /// </summary>
    private void UseSpecial()
    {
        // ゲージMAXかつクールタイム終了でなければ発動しない
        if (!specialGauge.CanUseSpecial())
            return;

        if (allyStatus.SpecialAttackPrefab == null || firePoint == null)
            return;

        GameObject nearestEnemy = FindNearestEnemy();

        if (nearestEnemy == null)
            return;

        Vector3 direction =
            nearestEnemy.transform.position - firePoint.position;

        Quaternion rotation =
            Quaternion.LookRotation(direction);

        // 必殺技中にする
        allyStatus.StartSpecial();

        // 必殺技Prefabを生成
        Instantiate(
            allyStatus.SpecialAttackPrefab,
            firePoint.position,
            rotation
        );

        // 共通ゲージをリセット
        specialGauge.ResetGauge();
    }

    /// <summary>
    /// *ちかくのてきを さがす
    /// </summary>
    /// <returns></returns>
    private GameObject FindNearestEnemy()
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