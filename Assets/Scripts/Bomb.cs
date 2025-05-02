using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject particle;
    private void OnCollisionEnter(Collision other)
    {
        Instantiate(particle, transform.position, Quaternion.identity);
        
        IsHit isHit = other.gameObject.GetComponent<IsHit>();
        
        if (isHit != null)
        {
            isHit.OnHit(other.transform.position - transform.position);
        }
        Destroy(gameObject);
    }
}
