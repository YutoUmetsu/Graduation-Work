using System.Collections;
using UnityEngine;

public class ShieldBashAction : MonoBehaviour
{
    [Header("バッシュ設定")]
    [SerializeField] private float dashDistance = 2.0f;   // 2mダッシュ
    [SerializeField] private float dashDuration = 0.2f;   // ダッシュにかかる時間（秒）
    [SerializeField] private float angleRange = 90.0f;    // 90度範囲（前方 ±45度）
    [SerializeField] private float checkRadius = 2.5f;    // 索敵・判定の半径
    [SerializeField] private string enemyTag = "Enemy";

    private void Start()
    {
        // ダッシュとダメージ判定のコルーチンを開始
        StartCoroutine(ExecuteBash());
    }

    IEnumerator ExecuteBash()
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + transform.forward * dashDistance;
        float elapsed = 0f;

        // 1. 2mダッシュしながら判定を行う
        while (elapsed < dashDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / dashDuration);
            transform.position = Vector3.Lerp(startPos, targetPos, t);

            // ダッシュ中の毎フレーム、範囲内の敵をチェックしてダメージを与える
            CheckAndDamageEnemies();

            yield return null;
        }

        transform.position = targetPos;

        // 2. 処理が終わったらプレハブ自体を消去
        Destroy(gameObject);
    }

    void CheckAndDamageEnemies()
    {
        // 周囲の敵を一度コライダーで一括取得
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);

        // プレイヤーのレベル X を取得
        int playerLevel = 1;
        PlayerEXP playerEXP = Object.FindFirstObjectByType<PlayerEXP>();
        if (playerEXP != null)
        {
            playerLevel = playerEXP.Level;
        }

        // ダメージ計算: 5 * X
        int damage = 5 * playerLevel;

        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag(enemyTag))
            {
                // 敵の方向ベクトルと自身の向いている方向（forward）の角度を計算
                Vector3 directionToEnemy = (col.transform.position - transform.position).normalized;
                directionToEnemy.y = 0;
                float angle = Vector3.Angle(transform.forward, directionToEnemy);

                // 前方 90度（±45度以内）の範囲にいる場合のみヒット
                if (angle <= angleRange / 2f)
                {
                    TestEnemyHP enemyHP = col.GetComponent<TestEnemyHP>();
                    if (enemyHP != null)
                    {
                        // 敵にダメージを与える（※二重ヒットを防ぎたい場合は敵側でヒット管理するか、1回きりの判定に調整できます）
                        enemyHP.TakeDamage(damage);
                    }
                }
            }
        }
    }

    // エディタ上で判定範囲（前方90度・半径）を可視化
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}