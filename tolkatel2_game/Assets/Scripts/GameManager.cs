using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject pointer;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Camera mainCamera;
    [SerializeField] Transform transf;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] Transform directionArrowTransform;
    [SerializeField] private float jumpPower;
    [SerializeField] private float brakePower;
    public float moveForce;
    public static GameManager Singleton;
    private Transform pointerTransform;

    private void Awake()
    {
        if (Singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        Singleton = this;

    }

    private void Start()
    {
        pointer.SetActive(false);
        pointerTransform = pointer.GetComponent<Transform>();
    }

    private void Update()
    {
        RaycastHit rayHit;
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, groundLayer))
        {
            pointer.SetActive(true);
            MovePointer(rayHit.point);
            SetArrowDirection(pointerTransform);
        }
        else
        {
            pointer.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            MovePlayer();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpPlayer();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Brake();
        }
    }

    public void MovePointer(Vector3 pos)
    {
        pointerTransform.position = pos;
    }

    public void SetArrowDirection(Transform pointToLook)
    {
        var lookPos = new Vector3(pointToLook.position.x, transf.position.y, pointToLook.position.z);
        directionArrowTransform.LookAt(lookPos);
    }

    private void MovePlayer()
    {
        var direction = new Vector3(pointerTransform.position.x - transf.position.x, 0f, pointerTransform.position.z - transf.position.z).normalized;
        playerRb.AddForce(direction * moveForce, ForceMode.Impulse);
        direction = Vector3.zero;

    }
    private void JumpPlayer()
    {
        playerRb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    private void Brake()
    {
        var currentDir = playerRb.velocity.normalized;
        currentDir.y = 0f;
        playerRb.AddForce(-currentDir * brakePower, ForceMode.Impulse);
        Debug.Log("brake");
    }
}
