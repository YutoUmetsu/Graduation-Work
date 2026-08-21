using UnityEngine;

public class TestEnemyMove : MonoBehaviour
{
    private Transform player;

    [SerializeField] private float speed = 3f;

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
}