using UnityEngine;

public class Enemy_03 : MonoBehaviour
{
    PlayerHp player;

    [SerializeField]
    private GameObject Bullet;

    [SerializeField]
    private float AttackChargeTime = 3;
    private float AttackChargeCount = 0;

    public int HP;

    [SerializeField]
    private int AttackPow = 1;



    private Vector3 PlayerPos;

    void Start()
    {
        player = FindObjectOfType<PlayerHp>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }

        transform.LookAt(player.transform.position);

        if (Vector3.Distance(player.transform.position, transform.position) > 3)
        {
            transform.position += transform.forward * Time.deltaTime;
        }
        else
        {

            if (AttackChargeCount > 0)
            {
                AttackChargeCount -= Time.deltaTime;

            }
            else
            {
                AttackChargeCount = AttackChargeTime;
                GameObject CloneBullet = Instantiate(Bullet, transform.position, transform.rotation);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHp>().TakeDamage(AttackPow,gameObject);
            //PlayerÇ…ì≈Çó^Ç¶ÇÈèàóù
        }
    }

}
