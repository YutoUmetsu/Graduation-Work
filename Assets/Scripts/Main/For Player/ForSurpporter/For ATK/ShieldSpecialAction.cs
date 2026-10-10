using UnityEngine;

public class ShieldSpecialAction : MonoBehaviour
{
    [Header("シールド設定")]
    [SerializeField] private float shieldDuration = 15.0f; // シールド持続時間
    [SerializeField] private int monsterRankZ = 1;   // ランク値 Z

    private PlayerHp targetPlayerHp;
    private int addedShieldAmount = 0;

    private void Start()
    {
        // ★追従させるため、SetParent(null) は削除。
        // （AllyDirectSpecial で生成された時点で、すでに発動者の子供になっている想定）

        targetPlayerHp = Object.FindFirstObjectByType<PlayerHp>();
        AllyStatus allyStatus = Object.FindFirstObjectByType<AllyStatus>();

        if (targetPlayerHp != null)
        {
            // HP5Z% のシールド量を計算
            float percent = (5f * monsterRankZ) / 100f;
            addedShieldAmount = Mathf.CeilToInt(targetPlayerHp.MaxHP * percent);

            // シールド付与
            targetPlayerHp.AddShield(addedShieldAmount);
        }

        // ★通常攻撃を即座に解放
        if (allyStatus != null)
        {
            allyStatus.EndSpecial();
        }

        // 15秒後にシールド解除（OnDestroyを呼ぶ）
        Destroy(gameObject, shieldDuration);
    }

    private void OnDestroy()
    {
        // 15秒経過してエフェクトが消えるタイミングでシールドをクリア
        if (targetPlayerHp != null)
        {
            targetPlayerHp.ClearShield();
        }
    }
}