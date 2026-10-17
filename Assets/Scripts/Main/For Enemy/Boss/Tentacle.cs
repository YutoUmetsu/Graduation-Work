using UnityEngine;

public class Tentacle : MonoBehaviour
{

    [SerializeField]
    private float AttackTime;
    private float AttackCount;

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
            Instantiate(AttackObject, transform.position, Quaternion.identity);
        }
    }
}
