using UnityEngine;
using System.Collections;

public class Trick2to1Bullet : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxDistance = 5f;

    [Header("生成設定")]
    [SerializeField] private float waitTime = 2f;

    [Header("サイズ設定")]
    [SerializeField] private float diameterIncrease = 0.5f;

    private Vector2 moveDirection;
    private float movedDistance = 0f;
    private bool isMoving = false;

    private GameObject playerPos;
    private PlayerMove playerMovement;

    public void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player");
        playerMovement = playerPos.GetComponent<PlayerMove>();

        StartCoroutine(WaitAndMove());
    }

    // 2秒待機してから移動開始
    private IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(waitTime);

        isMoving = true;
    }

    private void Update()
    {

        if (!isMoving)
        {
            // プレイヤーの移動方向を取得
            Vector2 direction = playerMovement.MoveDirection;

            if (direction != Vector2.zero)
            {
                moveDirection = direction;
            }

            // X・Z平面の移動方向に変換
            Vector3 direction3d = new Vector3(
                moveDirection.x,
                0,
                moveDirection.y
            );

            // プレイヤーの前方1mに配置
            transform.position = playerPos.transform.position + direction3d.normalized;

            return;
        }
            

        // 今回の移動量
        float moveAmount = speed * Time.deltaTime;

        // 残りの移動距離を超えないようにする
        moveAmount = Mathf.Min(
            moveAmount,
            maxDistance - movedDistance
        );

        // 移動
        Vector3 direction3D = new Vector3(moveDirection.x, 0, moveDirection.y);

        transform.position +=
            direction3D.normalized * speed * Time.deltaTime;

        movedDistance += moveAmount;

        // 移動時間に応じてサイズを拡大
        float moveTime = moveAmount / speed;

        float sizeIncrease = diameterIncrease * moveTime;

        transform.localScale +=
            new Vector3(sizeIncrease, sizeIncrease, sizeIncrease);

        // 5m移動したら消滅
        if (movedDistance >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    // ぶつかった相手がEnemyタグか確認
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        Destroy(gameObject);
    //    }
    //}

}
