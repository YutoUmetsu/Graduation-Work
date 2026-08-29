using Unity.VisualScripting;
using UnityEngine;

public class BulletTest : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    void Update()
    {
        // ’e‚ð‘O•ûŒü‚ÉˆÚ“®
        transform.position += transform.forward * speed * Time.deltaTime;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
