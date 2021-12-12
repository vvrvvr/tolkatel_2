using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement playerMovement;

    //�������
    [SerializeField] public GameObject pointer;
    private Transform pointerTransform;
    //������
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Camera mainCamera;
    [SerializeField] Transform directionArrowTransform;
    //����������
    [SerializeField] private float jumpPower;
    [SerializeField] private float brakePower;
    public float moveForce;
    //
    private Transform transf;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Start()
    {
        pointer.SetActive(false);
        pointerTransform = pointer.GetComponent<Transform>();
        transf = GetComponent<Transform>();
    }

    private void Update()
    {

        RaycastHit rayHit;
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, groundLayer))
        {
            pointer.SetActive(true);
            MovePointer(rayHit.point); //��������� ������� �����
            SetArrowDirection(pointerTransform); //�������� ��������� ������� �����������
        }
        else
        {
            pointer.SetActive(false);
        }

        //�����
        var weakMove = Input.GetButtonDown(GlobalStringVars.DASH);
        var jump = Input.GetButtonDown(GlobalStringVars.JUMP);
        var brake = Input.GetButtonDown(GlobalStringVars.BRAKE);


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
}
