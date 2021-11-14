using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject pointer;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Camera mainCamera;
    [SerializeField] Transform transf;
    [SerializeField] Transform directionArrowTransform;
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
            //player.directionArrow.SetActive(true);
            pointer.SetActive(true);
            MovePointer(rayHit.point);
            SetArrowDirection(pointerTransform);
            //player.SetArrowDirection(pointer.GetComponent<Transform>());
        }
        else
        {
            pointer.SetActive(false);
            //player.directionArrow.SetActive(false);
        }

    }

    public void MovePointer(Vector3 pos)
    {
        pointer.GetComponent<Transform>().position = pos;
    }

    public void SetArrowDirection(Transform pointToLook)
    {
        var lookPos = new Vector3(pointToLook.position.x, transf.position.y, pointToLook.position.z);
        directionArrowTransform.LookAt(lookPos);
    }
}
