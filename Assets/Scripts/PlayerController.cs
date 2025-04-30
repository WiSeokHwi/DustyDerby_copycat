using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerInput playerInput;
    private Rigidbody rb;
    
    public Transform cam;
    
    public float moveSpeed = 1f;
    public float lookSpeed = 120f;
    
    private void Awake()
    {
        if (playerInput == null)
        {
            playerInput = GetComponent<PlayerInput>();
        }
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (playerInput.rotateInput != 0)
        {
            HandleCameraLook();
        }
    }

    private void FixedUpdate()
    {
        if (playerInput.moveInput.magnitude != 0)
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        float moveX = playerInput.moveInput.x;
        float moveZ = playerInput.moveInput.y;
        Vector3 rbForward = rb.transform.forward;
        rbForward.y = 0;
        rbForward.Normalize();
        Vector3 rbRight = rb.transform.right;
        rbRight.y = 0;
        rbRight.Normalize();
        
        
        Vector3 moveDirection = (moveX * rbRight) + (moveZ * rbForward);
        
        rb.MovePosition(rb.position + moveDirection * (moveSpeed * Time.deltaTime));
    }
    void HandleCameraLook()
    {
        // 마우스 이동 값 가져오기
        // "Mouse X"는 좌우, "Mouse Y"는 상하 움직임입니다.
        float mouseX = playerInput.rotateInput * lookSpeed * Time.deltaTime;
        

        // 1. 수평 회전: 플레이어 오브젝트 자체를 Y축 기준으로 회전시킵니다.
        // 이렇게 하면 플레이어의 transform.forward 방향이 마우스 수평 움직임에 따라 바뀝니다.
        transform.Rotate(Vector3.up * mouseX);
        
        
    }
}
