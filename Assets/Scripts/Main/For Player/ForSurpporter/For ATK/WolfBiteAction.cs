using UnityEngine;

public class WolfBiteAction : MonoBehaviour
{
    [Header("ブリンク設定")]
    [SerializeField] private float dashDistance = 5.0f;

    [Header("判定サイズ (幅X, 高さY, 奥行きZ = 3×5m)")]
    [SerializeField] private Vector3 boxSize = new Vector3(3f, 2f, 5f);
    [SerializeField] private float lifeTime = 0.5f;

    private AllyStatus targetAllyStatus; // フラグを戻すために発動者を保持

    private void Start()
    {
        // 他のオブジェクトに追従しないようワールド座標に固定
        transform.SetParent(null);

        // シーン内から発動したオオカミ（AllyStatus）を特定して保持する
        // ※もし複数キャラがいる場合は、一番近くにいる、またはステータスが特殊状態中のキャラを特定します
        targetAllyStatus = Object.FindFirstObjectByType<AllyStatus>();

        if (targetAllyStatus != null)
        {
            // 1. オオカミ本体を前方に5mブリンク
            targetAllyStatus.transform.position += transform.forward * dashDistance;
        }

        // 2. 生成された場所（後ろの軌跡）に3×5mの判定を出す
        ExecuteBiteHit();

        // 3. 一定時間後に判定オブジェクトを消去（同時にフラグを戻す）
        Destroy(gameObject, lifeTime);
    }

    private void ExecuteBiteHit()
    {
        // 後ろ側に展開されるボックスの中心座標
        Vector3 boxCenter = transform.position - (transform.forward * (boxSize.z / 2f));
        Collider[] hitColliders = Physics.OverlapBox(boxCenter, boxSize / 2f, transform.rotation);

        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Enemy"))
            {
                ProjectileDamage projectileDamage = GetComponent<ProjectileDamage>();
                TestEnemyHP enemyHP = col.GetComponent<TestEnemyHP>();

                if (enemyHP != null && projectileDamage != null)
                {
                    // 1ヒット固定チェック
                    if (projectileDamage.CanDamage(col.gameObject))
                    {
                        int damage = projectileDamage.GetDamage(); // 10 * X

                        // ※TestEnemyHPへのダメージ適用方法に合わせて記述
                        // 例としてログを表示
                        Debug.Log($"[Wolf Special] 敵に {damage} のダメージ！");
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        // 4. 判定オブジェクトが消滅するタイミングで、オオカミの必殺技中フラグを解除する
        if (targetAllyStatus != null)
        {
            targetAllyStatus.EndSpecial();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(-Vector3.forward * (boxSize.z / 2f), boxSize);
    }
}
