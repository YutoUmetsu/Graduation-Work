using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Attack1stBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private void Start()
    {
        StartCoroutine(BulletDelete());
    }

    void Update()
    {
        // ’e‚ð‘O•ûŒü‚ÉˆÚ“®
        transform.position += transform.forward * speed * Time.deltaTime;

    }

    IEnumerator BulletDelete()
    {
        yield return new
        WaitForSeconds(2f);
        Destroy(gameObject);

    }

}
