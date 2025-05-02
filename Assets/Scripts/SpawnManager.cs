using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject spawnPrefab;  // 몬스터나 아이템 프리팹
    public int maxSpawnCount = 10;     // 몇 개 소환할지
    public float spawnRadius = 20f; // 소환 반경
    private List<GameObject> spawnInstances = new List<GameObject>(10);


    private void Start()
    {
        InvokeRepeating("SpawnItems", 3f, Random.Range(3f, 8f));
    }


    public void SpawnItems()
    {
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * spawnRadius;
        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 30f, NavMesh.AllAreas))
        {
            GameObject item = Instantiate(spawnPrefab, hit.position + new Vector3(0, 1f, 1), Quaternion.identity);
            Debug.Log("Spawned item: " + item.name);
            spawnInstances.Add(item);

            if (spawnInstances.Count > maxSpawnCount)
            {
                // 오래된 거 삭제
                Destroy(spawnInstances[0]);
                spawnInstances.RemoveAt(0);
            }
        }
    }
}
