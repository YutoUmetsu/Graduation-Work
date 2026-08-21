using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("プレイヤー")]
    [SerializeField] private Transform player;

    [Header("雑魚モンスター")]
    [SerializeField] private List<GameObject> normalMonsterPrefabs = new List<GameObject>();
    [SerializeField] private float spawnInterval = 1.0f;
    [SerializeField] private float spawnDistance = 10.0f;

    [Header("ボス")]
    [SerializeField] private GameObject middleBossPrefab;
    [SerializeField] private GameObject bossPrefab;

    [Header("雑魚モンスター上限")]
    [SerializeField] private int maxNormalMonsters = 150;

    private float spawnTimer;
    private int normalMonsterCount;

    public float SpawnInterval
    {
        get => spawnInterval;
        set => spawnInterval = value;
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnNormalMonster();
            spawnTimer = 0f;
        }
    }

    private void SpawnNormalMonster()
    {
        if (normalMonsterCount >= maxNormalMonsters)
            return;

        if (player == null || normalMonsterPrefabs.Count == 0)
            return;

        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        Vector3 spawnPosition = player.position;
        spawnPosition.x += randomDirection.x * spawnDistance;
        spawnPosition.z += randomDirection.y * spawnDistance;

        GameObject prefab = normalMonsterPrefabs[
            Random.Range(0, normalMonsterPrefabs.Count)
        ];

        if (prefab == null)
            return;

        Instantiate(prefab, spawnPosition, Quaternion.identity);

        normalMonsterCount++;
    }

    public void SpawnMiddleBoss()
    {
        if (middleBossPrefab == null || player == null)
            return;

        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        Vector3 spawnPosition = player.position;
        spawnPosition.x += randomDirection.x * spawnDistance;
        spawnPosition.z += randomDirection.y * spawnDistance;

        Instantiate(middleBossPrefab, spawnPosition, Quaternion.identity);
    }

    public void SpawnBoss()
    {
        if (bossPrefab == null || player == null)
            return;

        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        Vector3 spawnPosition = player.position;
        spawnPosition.x += randomDirection.x * spawnDistance;
        spawnPosition.z += randomDirection.y * spawnDistance;

        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
    }

    public void DecreaseNormalMonsterCount()
    {
        normalMonsterCount = Mathf.Max(0, normalMonsterCount - 1);
    }
}