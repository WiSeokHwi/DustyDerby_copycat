using System;
using System.Collections;
using Benjathemaker;
using UnityEngine;

public class PlayerController : MonoBehaviour, IsHit
{
    PlayerInput playerInput;
    public float moveSpeed = 10f;
    public float lookSpeed = 120f;
    public float equipRange = 1.5f;
    public float equipAngle = 45f;
    public bool isSturn = false;
    public GameObject sturnParticles;
    
    
    Collider[] item = new Collider[10];
    
    public Transform equipPosition;
    
    private GameObject equipItem;

    private Rigidbody rb;
    Vector3 velocity;
    private void Awake()
    {
        if (playerInput == null)
        {
            playerInput = GetComponent<PlayerInput>();
        }
        rb = GetComponent<Rigidbody>();
    }



    // Update is called once per frame
    void Update()
    {
        if (isSturn) return;
        
        if (!equipItem)
        {
            EquipItem();
        }

        if (equipItem && Input.GetMouseButtonDown(0))
        {
            ThrowItem();
        }
        
        
        if (playerInput.rotateInput != 0)
        {
            HandleCameraLook();
        }
    }

    private void FixedUpdate()
    {
        if (isSturn) return;
        if (playerInput.moveInput.magnitude != 0)
        {
            MovePlayer();
        }
        
    }

    public void OnHit(Vector3 hitDirection)
    {
        StartCoroutine(IsSturn(hitDirection));
    }

    private IEnumerator IsSturn(Vector3 hitDirection)
    {
        isSturn = true;
        sturnParticles.SetActive(true);
        hitDirection.y = 0;
        rb.AddForce(hitDirection.normalized * 10f, ForceMode.Impulse);
        yield return new WaitForSecondsRealtime(2f);
        sturnParticles.SetActive(false);
        isSturn = false;
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
        Vector3 newVelocity = moveDirection * moveSpeed;
        newVelocity.y = rb.linearVelocity.y; // 기존 y축 속도 유지

        rb.linearVelocity = newVelocity;
        
        
        /*rb.MovePosition(rb.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));*/
    }
    void HandleCameraLook()
    {
        // 마우스 이동 값 가져오기
        // "Mouse X"는 좌우, "Mouse Y"는 상하 움직임입니다.
        float mouseX = playerInput.rotateInput * lookSpeed * Time.deltaTime;
        

        Quaternion deltaRotation = Quaternion.Euler(0f, mouseX, 0f);
        rb.MoveRotation(rb.rotation * deltaRotation);
        
        
    }

    void EquipItem()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, equipRange, item);

        for (int i = 0; i < hitCount; i++)
        {
            Collider col = item[i];
            if (col && col.gameObject.layer == LayerMask.NameToLayer("Item") && !equipItem)
            {
                Vector3 directionToTarget = (col.transform.position - transform.position).normalized;
                float angle = Vector3.Angle(transform.forward, directionToTarget);
                if (angle < equipAngle)
                {
                    if (playerInput.mouseDown)
                    {
                        equipItem = col.gameObject;
                        equipItem.GetComponent<SimpleGemsAnim>().enabled = false;
                        Rigidbody itemRb = equipItem.GetComponent<Rigidbody>();
                        itemRb.isKinematic = true;
                        equipItem.transform.position = equipPosition.position;
                        equipItem.transform.SetParent(equipPosition);
                        Debug.Log(equipItem.name);
                    }
                }
            }
        }
    }

    void ThrowItem()
    {
        equipItem.GetComponent<Rigidbody>().isKinematic = false;
        equipItem.GetComponent<Rigidbody>().useGravity = true;
        equipItem.GetComponent<Collider>().isTrigger = false;
        equipItem.GetComponent<Rigidbody>().AddForce(transform.forward * 1000f);
        equipItem = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, equipRange);
        
        // 부채꼴의 왼쪽과 오른쪽 경계선을 그립니다
        Vector3 leftEdge = Quaternion.Euler(0, -equipAngle / 2, 0) * transform.forward * equipRange;
        Vector3 rightEdge = Quaternion.Euler(0, equipAngle / 2, 0) * transform.forward * equipRange;

        // 원점에서 부채꼴의 왼쪽 끝과 오른쪽 끝으로 선을 그립니다
        Gizmos.DrawLine(transform.position, transform.position + leftEdge);
        Gizmos.DrawLine(transform.position, transform.position + rightEdge);
    }
}
