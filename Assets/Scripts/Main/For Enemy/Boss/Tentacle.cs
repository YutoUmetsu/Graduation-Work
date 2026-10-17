using UnityEngine;

public class Tentacle : MonoBehaviour
{

    [SerializeField]
    private float AttackTime;
    private float AttackCount;

    [SerializeField]
    private int AttackPow;

    [SerializeField]
    private GameObject AttackObject;

    void Start()
    {
        Destroy(gameObject, 3f);    
    }

    // Update is called once per frame
    void Update()
    {
        AttackCount += Time.deltaTime;
        if (AttackCount > AttackTime)
        {
            AttackObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //ƒvƒŒƒCƒ„[‚ÌHP‚ÌÅ‘åHP‚ğ‚P‚T“Œ¸­‚³‚¹‚éŒø‰Ê‚ğ‚R‚O•bŠÔ•t—^
            other.gameObject.GetComponent<PlayerHp>().TakeDamage(AttackPow, gameObject);
        }
    }
}
