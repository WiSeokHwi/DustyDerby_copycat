using System;
using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    public GameObject particles;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.instance.Score += 1;
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
