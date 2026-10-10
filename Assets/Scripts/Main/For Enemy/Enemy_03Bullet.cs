using UnityEngine;

public class Enemy_03Bullet : MonoBehaviour
{
    [SerializeField]
    private int AttackPow;
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHp>().TakeDamage(AttackPow);
            //Player‚É“Å‚ğ—^‚¦‚éˆ—
        }
    }
}
