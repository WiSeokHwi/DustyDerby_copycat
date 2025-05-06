using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour, IsHit
{
    private NavMeshAgent agent;
    public float speed = 3f;
    public float posRange = 20f;
    public float chaseRange = 5f;
    private bool isChasing = false;
    private Vector3 target;
    private LayerMask playerLayerMask;
    public ParticleSystem sturnParticle;
    

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        isChasing = false;
        agent.speed = speed;
        playerLayerMask = LayerMask.GetMask("Player");
    }

    
    void Start()
    {
        InvokeRepeating("RandomPos", 0f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        IsChasing();
    }

    void RandomPos()
    {
        if (isChasing) return;
        
        
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * posRange;
        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, posRange, NavMesh.AllAreas))
        {
            target = hit.position;
            agent.destination = target;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    public void OnHit(Vector3 hitDirection)
    {
        StartCoroutine(IsSturn());
    }

    IEnumerator IsSturn()
    {
        Debug.Log("공격받은");
        agent.isStopped = true;
        sturnParticle.gameObject.SetActive(true);
        sturnParticle.Play();
        yield return new WaitForSeconds(2f);
        sturnParticle.gameObject.SetActive(false);
        sturnParticle.Stop();
        agent.isStopped = false;
        RandomPos();
    }

    void IsChasing()
    {
    
        Collider[] colliders = Physics.OverlapSphere(transform.position, chaseRange, playerLayerMask);
        Debug.Log(colliders.Length);
        foreach (Collider col in colliders)
        {
            target = col.gameObject.transform.position;
            agent.destination = target;
            isChasing = true;
            break; // 플레이어 찾으면 더 이상 체크 안 함
        }
        if (colliders.Length <= 0 && agent.remainingDistance <= agent.stoppingDistance && isChasing)
        {
            isChasing = false; // 추적 끝
            RandomPos(); // 바로 랜덤 이동 재개
        }
       
    }

    private void OnCollisionEnter(Collision other)
    {
        IsHit Hit = other.gameObject.GetComponent<IsHit>();

        if (Hit != null)
        {
            Hit.OnHit(other.transform.position - transform.position);
        }
        else
        {
            return;
        }
    }
}
