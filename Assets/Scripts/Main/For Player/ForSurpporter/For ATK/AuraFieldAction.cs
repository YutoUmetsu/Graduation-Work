using UnityEngine;

public class AuraFieldAction : MonoBehaviour
{
    [Header("フィールド設定")]
    [SerializeField] private float radius = 4.0f;   // 直径8mなので半径は4m
    [SerializeField] private float duration = 5.0f; // 5秒間展開
    [SerializeField] private string enemyTag = "Enemy";

    private AllyStatus allyStatus;

    private void Start()
    {
        // 発動者を特定
        allyStatus = Object.FindFirstObjectByType<AllyStatus>();

        // ★発動直後に通常攻撃のロックを解除（フィールドを纏いながら通常攻撃も撃てるようにする）
        if (allyStatus != null)
        {
            allyStatus.EndSpecial();
        }

        // 5秒後にフィールド（プレハブ）を消去
        Destroy(gameObject, duration);
    }

    private void Update()
    {
        // 毎フレーム、範囲内の敵に秒間ダメージを継続的に与える
        ApplyAuraDamage();
    }

    private void ApplyAuraDamage()
    {
        int playerLevel = 1;
        PlayerEXP playerEXP = Object.FindFirstObjectByType<PlayerEXP>();
        if (playerEXP != null)
        {
            playerLevel = playerEXP.Level;
        }

        float damagePerSecond = 4f * playerLevel;
        float frameDamage = damagePerSecond * Time.deltaTime;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag(enemyTag))
            {
                TestEnemyHP enemyHP = col.GetComponent<TestEnemyHP>();
                if (enemyHP != null)
                {
                    // ★これでバッチリ外部からHPを削れます！
                    // 小数点を切り上げるか、あるいは整数化して渡す
                    int finalDamage = Mathf.CeilToInt(frameDamage);
                    if (finalDamage > 0)
                    {
                        enemyHP.TakeDamage(finalDamage);
                    }
                }
            }
        }
    }

    // エディタ上で直径8m（半径4m）の範囲を水色で可視化
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}