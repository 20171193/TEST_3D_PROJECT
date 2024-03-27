using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [Header("Components")]
    [Space(2)]
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private Joystick joystick;

    [Space(3)]
    [Header("Specs")]
    [Space(2)]
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float jumpPower;


    [Space(3)]
    [Header("Balancing")]
    [Space(2)]
    [SerializeField]
    private bool isRun;

    [SerializeField]
    private bool isJoystickMove;
    public bool IsJoystickMove {
        get { return isJoystickMove; }
        set 
        { 
            isJoystickMove = value;
            joystick.gameObject.SetActive(value);
        } 
    }   

    private Vector2 inputDir;
    private Vector3 moveDir;

    private void Start()
    {
    }

    private void Update()
    {
        Move();
    }
    private void OnMove(InputValue value)
    {
        inputDir = value.Get<Vector2>();
    }
    private void Move()
    {
        // �Է�Ÿ�Կ� ���� �̵����� ����
        moveDir = isJoystickMove ?
            new Vector3(joystick.MoveDir.x, 0, joystick.MoveDir.y):
            new Vector3(inputDir.x, 0, inputDir.y);

        // �Է��� ���� ��� ����ó��
        if (moveDir == Vector3.zero)
        {
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsRun", false);
            return;
        }

        anim.SetBool("IsWalk", !isRun);
        anim.SetBool("IsRun", isRun);

        // �̵�Ÿ�Կ� ���� �ӵ�����
        float moveSpeed = isRun ? runSpeed : walkSpeed;

        // �̵��������� �ٷ� ȸ���ϱ����� ȸ�� �� ���� 
        transform.forward = moveDir;
        controller.Move(transform.forward * moveSpeed * Time.deltaTime);
    }

    private void OnMouseClick(InputValue value)
    {
        Vector2 mousePos = Input.mousePosition;
        Debug.Log(mousePos);
        if (mousePos.x > Screen.width / 2) return;
        joystick.EnableJoystick(mousePos);
    }
}
