using UnityEngine;

public class TestEnemyMove : MonoBehaviour
{
    private Transform player;

    [SerializeField] private float speed = 3f;

    [Header("プレイヤーに与えるダメージ")]
    [SerializeField] private int Edamage = 10;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Playerタグのオブジェクトが見つかりません。");
        }
    }

    void Update()
    {
        if (player == null)
            return;

        // プレイヤーへの方向
        Vector3 direction = player.position - transform.position;

        // 長さを1にする
        direction.Normalize();

        // 移動
        transform.position += direction * speed * Time.deltaTime;
    }

    //Playerダメージ処理（TakeDamage(ダメージ量)）の呼び出し【仮】
    //ダメージスクリプトが別にでき次第、そっちに移植
    private void OnCollisionEnter(Collision collision)
    {
        // ぶつかった相手がPlayerタグか確認
        if (collision.gameObject.CompareTag("Player"))
        {
            // プレイヤーのHPスクリプトを取得
            TestPlayerHp playerHP =
                collision.gameObject.GetComponent<TestPlayerHp>();

            // HPスクリプトが存在する場合だけダメージを与える
            if (playerHP != null)
            {
                playerHP.TakeDamage(Edamage);
            }
        }
    }
}