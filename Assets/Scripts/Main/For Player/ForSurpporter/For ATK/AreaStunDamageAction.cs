using UnityEngine;

public class AreaStunDamageAction : MonoBehaviour
{
    [Header("範囲・持続設定")]
    [SerializeField] private float radius = 5.0f;     // 周囲5m
    [SerializeField] private float duration = 5.0f;   // 5秒間の持続
    [SerializeField] private string enemyTag = "Enemy";

    private AllyStatus allyStatus;

    private void Start()
    {
        // 他のオブジェクトに追従せず、発動した場所にその場固定で展開
        transform.SetParent(null);

        // 発動者を特定して保持
        allyStatus = Object.FindFirstObjectByType<AllyStatus>();

        // 1. スタン処理（未実装のためコメントアウト）
        // TODO: 周囲5mの敵を3秒間「スタン状態」にする処理をここに実装予定
        // ApplyStunToEnemies();

        // 2. 範囲内の敵へのダメージ処理（5秒間持続の範囲ダメージ）
        ApplyAreaDamage();

        // 3. 一定時間（5秒間）経過した後に消去
        Destroy(gameObject, duration);
    }

    private void ApplyStunToEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag(enemyTag))
            {
                // 例: col.GetComponent<EnemyStatus>()?.SetStun(3.0f);
            }
        }
    }

    private void ApplyAreaDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        // プレイヤーのレベル X を取得（PlayerEXPコンポーネントから）
        int playerLevel = 1;
        PlayerEXP playerEXP = Object.FindFirstObjectByType<PlayerEXP>();
        if (playerEXP != null)
        {
            playerLevel = playerEXP.Level;
        }

        // 必殺技のダメージ計算: 10 * X
        int damage = 10 * playerLevel;

        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag(enemyTag))
            {
                TestEnemyHP enemyHP = col.GetComponent<TestEnemyHP>();
                if (enemyHP != null)
                {
                    // ★追加：TestEnemyHP側にダメージを直接与える！
                    enemyHP.TakeDamage(damage);
                    Debug.Log($"[Area Special] 敵に {damage} のダメージを与えました！");
                }
            }
        }
    }

    private void OnDestroy()
    {
        // 5秒間の効果時間が終了し、オブジェクトが消えるタイミングで必殺技フラグを解除
        if (allyStatus != null)
        {
            allyStatus.EndSpecial();
        }
    }

    // エディタ上で範囲（5m）を視覚的に確認するためのGizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}