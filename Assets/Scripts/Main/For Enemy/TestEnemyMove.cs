using UnityEngine;

public class TestEnemyMove : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 3f;

    void Update()
    {
        // ƒvƒŒƒCƒ„[‚Ö‚Ì•ûŒü
        Vector3 direction = player.position - transform.position;

        // ’·‚³‚ğ1‚É‚·‚é
        direction.Normalize();

        // ˆÚ“®
        transform.position += direction * speed * Time.deltaTime;
    }
}
