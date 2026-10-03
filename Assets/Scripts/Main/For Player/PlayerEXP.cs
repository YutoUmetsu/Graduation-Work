using UnityEngine;

public class PlayerEXP : MonoBehaviour
{
    [Header("レベル")]
    [SerializeField] private int level = 1;

    [Header("経験値")]
    [SerializeField] private int currentExp = 0;
    [SerializeField] private int requiredExp = 100000;

    private PlayerBuffStatus playerBuffStatus;

    public int Level => level;
    public int CurrentExp => currentExp;
    public int RequiredExp => requiredExp;

    private void Start()
    {
        playerBuffStatus = GetComponent<PlayerBuffStatus>();
    }

    /// <summary>
    /// *EXPを かさん
    /// </summary>
    /// <param name="amount">もとのEXP</param>
    public void AddExp(int amount)
    {
        // EXPバフの倍率をかける
        float expMultiplier = 1.0f;

        if (playerBuffStatus != null)
        {
            expMultiplier = playerBuffStatus.GetExpMultiplier();
        }

        int increasedExp =
            Mathf.RoundToInt(amount * expMultiplier);

        currentExp += increasedExp;

        while (currentExp >= requiredExp)
        {
            currentExp -= requiredExp;
            LevelUp();
        }
    }

    /// <summary>
    /// *EXPがたまったら LEVELが あがる
    /// *あふれたEXPは つぎのレベルアップに つかえるよ
    /// </summary>
    private void LevelUp()
    {
        level++;

        // 必要EXPは小数点切り上げで1.2倍
        requiredExp = Mathf.CeilToInt(requiredExp * 1.2f);
    }
}