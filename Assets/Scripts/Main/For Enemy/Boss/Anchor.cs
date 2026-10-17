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
    
    void Start()
    {
        MoveEnd = transform.position;

        parent = GetComponentInParent<Boss_01>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveCount < MoveTime)
        {
            MoveCount += Time.deltaTime;
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }
        else 
        {
            if (!parent.AnchorReverse)
            {
                parent.AnchorReverse = true;
            }

            if (!ReverseStart)
            {
                ReverseStart = true;
                StartCoroutine(Reverse());
            }


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
            transform.position = Vector3.Lerp(StartPos, MoveEnd, count / step);
            yield return null;
        }

        Destroy(gameObject);

    }
}
