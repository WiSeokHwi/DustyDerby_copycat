using System;
using UnityEngine;

public class ItemRotate : MonoBehaviour
{
    Transform transform;
    // Update is called once per frame

    private void Start()
    {
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * 50f);
    }
}
