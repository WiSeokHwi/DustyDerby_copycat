
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject spawnPrefab;  // 몬스터나 아이템 프리팹
    public GameObject enemyPrefab;
    public GameObject scorePrefab;
    public int maxSpawnCount = 10;     // 몇 개 소환할지
    public float spawnRadius = 20f; // 소환 반경
    private List<GameObject> spawnInstances = new List<GameObject>(10);
    private List<GameObject> spawnScoreInstances = new List<GameObject>(10);
    private List<GameObject> enemies = new List<GameObject>(10);
    private float randomeTime;


    private void Start()
    {
        randomeTime = Random.Range(5f, 10f);
        InvokeRepeating("SpawnItems", 3f, randomeTime);
        StartCoroutine(SpawnEnemiesCoroutine());
        InvokeRepeating("SpawnScoreItems", 5f, 15f);
        
    }


    public void SpawnItems()
    {
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * spawnRadius;
        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 30f, NavMesh.AllAreas))
        {
            GameObject item = Instantiate(spawnPrefab, hit.position + new Vector3(0, 1f, 1), Quaternion.identity);
            spawnInstances.Add(item);

            if (spawnInstances.Count > maxSpawnCount)
            {
                // 오래된 거 삭제
                Destroy(spawnInstances[0]);
                spawnInstances.RemoveAt(0);
            }
            randomeTime = Random.Range(5f, 10f);
        }
    }
    public void SpawnScoreItems()
    {
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * spawnRadius;
        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 30f, NavMesh.AllAreas))
        {
            GameObject item = Instantiate(scorePrefab, hit.position + new Vector3(0, 1f, 1), Quaternion.identity);
            spawnScoreInstances.Add(item);

            if (spawnScoreInstances.Count > maxSpawnCount)
            {
                // 오래된 거 삭제
                Destroy(spawnInstances[0]);
                spawnScoreInstances.RemoveAt(0);
            }
        }
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        while (true)
        {
            if (enemies.Count < maxSpawnCount)
            {
                yield return new WaitForSeconds(Random.Range(8f, 15f));
                SpawnEnemies();
            }
            else
            {
                yield return null;  // 최대치라면 한 프레임 쉬고 다시 확인
            }
        }
        
    }

    public void SpawnEnemies()
    {
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * spawnRadius;
        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 30f, NavMesh.AllAreas))
        {
            GameObject item = Instantiate(enemyPrefab, hit.position + new Vector3(0, 1f, 1), Quaternion.identity);
            enemies.Add(item);
        }
    }
}
