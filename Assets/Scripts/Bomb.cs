using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject particle;
    private void OnCollisionEnter(Collision other)
    {
        
        
        IsHit isHit = other.gameObject.GetComponent<IsHit>();
        if (isHit != null)
        {
            
            isHit.OnHit(other.transform.position - transform.position);
            
        }
        Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
