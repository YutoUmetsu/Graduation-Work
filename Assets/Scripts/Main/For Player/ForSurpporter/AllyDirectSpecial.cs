using UnityEngine;
using UnityEngine.InputSystem; // 新Input Systemを使うための名前空間

public class AllyDirectSpecial : MonoBehaviour
{
    [Header("生成設定")]
    [SerializeField] private bool attachToSelf = true; // trueなら発動者に追従（バフなど）、falseならその場に設置

    private AllyStatus allyStatus;
    private AllySpecialGauge specialGauge;

    private void Start()
    {
        allyStatus = GetComponent<AllyStatus>();
        specialGauge = FindFirstObjectByType<AllySpecialGauge>();
    }

    private void Update()
    {
        if (allyStatus == null || specialGauge == null)
            return;

        // 新Input SystemでXキーが押されたかチェック
        if (Keyboard.current != null && Keyboard.current.xKey.wasPressedThisFrame)
        {
            UseSpecial();
        }
    }

    private void UseSpecial()
    {
        // ゲージチェック
        if (!specialGauge.CanUseSpecial())
            return;

        if (allyStatus.SpecialAttackPrefab == null)
            return;

        // ★修正: 必殺技状態にする（AllyStatusの正しいメソッド名を指定）
        allyStatus.StartSpecial();

        // bool の値によって親（自身）に追従させるか、ワールド座標に独立生成するか切り替え
        if (attachToSelf)
        {
            // 自身（transform）を親にして生成 ＝ 移動してもエフェクトがついてくる
            Instantiate(allyStatus.SpecialAttackPrefab, transform.position, transform.rotation, transform);
        }
        else
        {
            // 親を指定せずに生成 ＝ ワールド座標に独立して配置される
            Instantiate(allyStatus.SpecialAttackPrefab, transform.position, transform.rotation);
        }

        // 共通ゲージをリセット
        specialGauge.ResetGauge();
    }
}