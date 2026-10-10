using UnityEngine;

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

        // Xキーで必殺技を発動（敵の有無に関わらず実行）
        if (Input.GetKeyDown(KeyCode.X))
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

        // 必殺技状態にする
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