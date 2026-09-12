using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHPUI : MonoBehaviour
{
    [SerializeField] private TestPlayerHp playerHp;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private TMP_Text hpText;

    private void Update()
    {
        if (playerHp == null)//*ないなら おわり
            return;

        hpSlider.maxValue = playerHp.MaxHP;
        hpSlider.value = playerHp.CurrentHp;
       
        //テキストでも ひょうじ
        hpText.text = $"{playerHp.CurrentHp} / {playerHp.MaxHP}";
    }
}