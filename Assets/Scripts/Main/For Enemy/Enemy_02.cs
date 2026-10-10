using UnityEngine;

public class Enemy_02 : MonoBehaviour
{
    PlayerHp player;

    [SerializeField]
    private float AttackChargeTime = 3;
    private float AttackChargeCount = 0;

    public int HP;

    [SerializeField]
    private int AttackPow;

    [SerializeField]
    private float MoveTime = 1;
    private float MoveCount = 0;

    private Vector3 PlayerPos;
    private Vector3 MyPos;

    void Start()
    {
        player = FindObjectOfType<PlayerHp>();
        MyPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0)
        {
            Destroy(gameObject);
        }


        if (AttackChargeCount < AttackChargeTime)
        {
            AttackChargeCount += Time.deltaTime;
            PlayerPos = player.transform.position;
        }
        else
        {
            if (MoveCount < MoveTime)
            {
                MoveCount += Time.deltaTime;
                if (MoveCount > MoveTime)
                    MoveCount = MoveTime;

                transform.position = Vector3.Lerp(MyPos, PlayerPos, MoveCount / MoveTime);

            }
            else
            {
                AttackChargeCount = 0;
                MoveCount = 0;
                MyPos = transform.position;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHp>().TakeDamage(AttackPow,gameObject);
            //Player‚É“Å‚ð—^‚¦‚éˆ—
        }
    }
        

}
