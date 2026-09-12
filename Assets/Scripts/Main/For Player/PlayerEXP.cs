using UnityEngine;

public class PlayerEXP : MonoBehaviour
{
    [Header("レベル")]
    [SerializeField] private int level = 1;

    [Header("経験値")]
    [SerializeField] private int currentExp = 0;
    [SerializeField] private int requiredExp = 100000;

    public int Level => level;
    public int CurrentExp => currentExp;
    public int RequiredExp => requiredExp;

    /// <summary>
    /// *EXPを かさん
    /// </summary>
    /// <param name="amount"></param>
    public void AddExp(int amount)
    {
        currentExp += amount;

        while (currentExp >= requiredExp)
        {
            currentExp -= requiredExp;
            LevelUp();
        }
    }
    /// <summary>
    /// *EXPがたまったら LOVEが あがる(あふれたEXPは つぎのレベルアップに つかえるよ)
    /// </summary>
    private void LevelUp()
    {
        level++;
        //*ひつようEXPは しょうすうてんきりあげで 1.2ばいになる
        requiredExp = Mathf.CeilToInt(requiredExp * 1.2f);
    }
}