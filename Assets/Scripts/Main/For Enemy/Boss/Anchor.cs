using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Anchor : MonoBehaviour
{
    Boss_01 parent;

    [SerializeField]
    private float MoveSpeed;

    [SerializeField]
    private float MoveTime;
    private float MoveCount;

    [SerializeField]
    private float ReverseTime;
    private float ReverseCount;

    private bool ReverseStart;

    private Vector3 MoveEnd;

    [SerializeField]
    private int AttackPow;
    [SerializeField]
    private int RoteAttackPow;

    void Start()
    {

        parent = GetComponentInParent<Boss_01>();
        MoveEnd = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!parent.AnchorReverse)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }
        else if (!ReverseStart)
        {

            ReverseStart = true;
            StartCoroutine(Reverse());
        }
    }

    IEnumerator Reverse()
    {
       Vector3 StartPos =  transform.position;

        float step = 2;
        float count = 0;
        while(count < step)
        {
            count += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(StartPos, MoveEnd, count / step);
            yield return null;
        }

        Destroy(gameObject);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!ReverseStart)
                other.gameObject.GetComponent<PlayerHp>().TakeDamage(AttackPow, gameObject);
            else
                other.gameObject.GetComponent<PlayerHp>().TakeDamage(RoteAttackPow, gameObject);
          
        }
    }
}
