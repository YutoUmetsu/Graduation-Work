using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    [Header("ダメージ倍率 (例: 5 * X なら 5)")]
    [SerializeField] private int damageMultiplier = 5;

    [Header("レベル依存にするかどうか")]
    [SerializeField] private bool useLevelScaling = true;

    private int finalDamage;

    void Start()
    {
        if (useLevelScaling)
        {
            // プレイヤーのレベル（X）を取得して計算
            int currentLevel = 1;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                PlayerEXP playerEXP = player.GetComponent<PlayerEXP>();
                if (playerEXP != null)
                {
                    currentLevel = playerEXP.Level;
                }
            }
            finalDamage = damageMultiplier * currentLevel;
        }
        else
        {
            // レベルに依存しない固定ダメージの場合
            finalDamage = damageMultiplier;
        }
    }

    // 敵がこのメソッドを呼んでダメージを受け取る
    public int GetDamage()
    {
        return finalDamage;
    }
}