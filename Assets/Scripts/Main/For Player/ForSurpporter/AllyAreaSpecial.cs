using UnityEngine;
using UnityEngine.InputSystem; // 新Input Systemを使うための名前空間

public class AllyAreaSpecial : MonoBehaviour
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

        // 範囲内に敵がいなければ発動しない
        if (!IsEnemyInRange())
            return;

        // 必殺技中にする
        allyStatus.StartSpecial();

        // 必殺技Prefabを生成
        Instantiate(
            allyStatus.SpecialAttackPrefab,
            firePoint.position,
            Quaternion.identity
        );

        // 共通ゲージをリセット
        specialGauge.ResetGauge();
    }

    /// <summary>
    /// *ちかくのてきを さがす はんい
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