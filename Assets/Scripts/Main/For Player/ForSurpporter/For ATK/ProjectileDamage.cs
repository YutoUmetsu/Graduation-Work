using UnityEngine;
using System.Collections.Generic;

public class ProjectileDamage : MonoBehaviour
{
    [Header("ダメージ倍率 (ダメージ = 2 * X)")]
    [SerializeField] private int damageMultiplier = 2;

    // すでにダメージを与えた敵を記憶するセット（1ヒット固定用）
    private HashSet<GameObject> hitTargets = new HashSet<GameObject>();

    /// <summary>
    /// ダメージ計算を行う（2 * プレイヤーのレベルX）
    /// </summary>
    public int GetDamage()
    {
        int playerLevel = 1; // デフォルト値（プレイヤーが見つからない場合）

        // シーン内から PlayerEXP を自動で取得し、レベルプロパティを参照する
        PlayerEXP playerEXP = Object.FindFirstObjectByType<PlayerEXP>();
        if (playerEXP != null)
        {
            playerLevel = playerEXP.Level;
        }

        return damageMultiplier * playerLevel;
    }

    /// <summary>
    /// この弾からまだダメージを受けていないターゲットか判定する
    /// </summary>
    public bool CanDamage(GameObject target)
    {
        if (!hitTargets.Contains(target))
        {
            hitTargets.Add(target);
            return true;
        }
        return false;
    }
}