using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class Tank1stBullet : MonoBehaviour
{
    private GameObject PlayerPrefab;
    private void Start()
    {
        StartCoroutine(BulletDelete());
        PlayerPrefab = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        transform.position = PlayerPrefab.transform.position;
    }

    IEnumerator BulletDelete()
    {
        yield return new
        WaitForSeconds(2f);
        Destroy(gameObject);

    }
}
