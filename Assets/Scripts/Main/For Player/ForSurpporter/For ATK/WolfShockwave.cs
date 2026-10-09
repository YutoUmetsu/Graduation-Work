using UnityEngine;

public class WolfShockwaveMove : MonoBehaviour
{
    [Header("移動・射程設定")]
    [SerializeField] private float moveSpeed = 10.0f; // 飛ぶスピード
    [SerializeField] private float maxDistance = 2.0f;  // 最大射程（2m）

    private Vector3 startPosition;
    private ProjectileDamage projectileDamage;

    void Start()
    {
        // 生成された位置を記録
        startPosition = transform.position;
        projectileDamage = GetComponent<ProjectileDamage>();
    }

    void Update()
    {
        // 前方へまっすぐ移動
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // 生成位置からの距離を計算し、2mを超えたら消滅する
        float distanceTraveled = Vector3.Distance(startPosition, transform.position);
        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    // 貫通するため、敵に当たってもこの弾自身は消滅させない
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // TestEnemyHP側でダメージ処理が行われるため、ここでは弾を消さない（貫通）
            // 必要であれば、すでにダメージを与えた敵を記録して二重ヒットを防ぐ処理をここに挟むこともできます
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 衝突判定（ColliderがIsTriggerでない場合）でも消滅させない
        }
    }
}