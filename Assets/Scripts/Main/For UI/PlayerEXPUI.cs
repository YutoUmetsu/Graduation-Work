using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerEXPUI : MonoBehaviour
{
    [SerializeField] private PlayerEXP playerEXP;
    [SerializeField] private Slider expSlider;
    [SerializeField] private TMP_Text expText;
    [SerializeField] private TMP_Text levelText;

    private void Update()
    {
        if (playerEXP == null)
            return;

        expSlider.maxValue = playerEXP.RequiredExp;
        expSlider.value = playerEXP.CurrentExp;

        int remainingExp =
            playerEXP.RequiredExp - playerEXP.CurrentExp;
        //*expText‚É‚Í ‚Ì‚±‚è‚Ð‚Â‚æ‚¤EXP‚ð
        expText.text = $"{remainingExp}";
        //*levelText‚É‚Í ‚¢‚Ü‚ÌLOVE‚ð ‚Ð‚å‚¤‚¶
        levelText.text = $"Lv. {playerEXP.Level}";
    }
}