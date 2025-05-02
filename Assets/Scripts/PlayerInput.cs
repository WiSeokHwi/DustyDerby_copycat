using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PlayerController playerController;

    public Vector2 moveInput;
    public float rotateInput;
    public bool mouseDown;
    
    void Awake()
    {
        if (playerController == null)
        {
            playerController = GetComponent<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        rotateInput = Input.GetAxis("Mouse X");
        mouseDown = Input.GetMouseButtonDown(0);
    }
}
