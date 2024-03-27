using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoystickController : MonoBehaviour
{
    [Header("Components")]
    [Space(2)]
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Canvas joystickCanvas;
    [SerializeField]
    private Joystick joystick;

    [Space(3)]
    [Header("Specs")]
    [Space(2)]
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;

    [Space(3)]
    [Header("Balancing")]
    [Space(2)]
    [SerializeField]
    private bool isRun;

    private void Start()
    {
    }

    private void Update()
    {
        Move();
    }

    // ���콺 �Է� �� ���̽�ƽ ��ġ ����
    private void OnMouseClick(InputValue value)
    {
        if (value.isPressed)
        {
            Vector2 mousePos = Input.mousePosition / joystickCanvas.scaleFactor;
            // ȭ���� ������ �Ѿ ��� ����
            if (mousePos.x > Screen.width / 2) return;
            joystick.EnableJoystick(mousePos);
        }
        else
        {
            joystick.DisableJoystick();
        }
    }

    private void Move()
    {
        // �Է��� ���� ��� ����ó��
        if (joystick.MoveDir == Vector3.zero)
        {
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsRun", false);
            return;
        }

        isRun = joystick.IsRun;

        anim.SetBool("IsWalk", !isRun);
        anim.SetBool("IsRun", isRun);

        // ĳ���� ���� ���� ����
        Vector3 moveDir = new Vector3(joystick.MoveDir.x, 0, joystick.MoveDir.y);
        // �̵�Ÿ�Կ� ���� �ӵ�����
        float moveSpeed = isRun ? runSpeed : walkSpeed;
        // �̵��������� �ٷ� ȸ���ϱ����� ȸ�� �� ���� 
        transform.forward = moveDir;
        controller.Move(transform.forward * moveSpeed * Time.deltaTime);
    }
}
