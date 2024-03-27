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
    private float maxWalkSpeed;

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
            Debug.Log(joystickCanvas.scaleFactor);
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
            anim.SetFloat("LeverLength", 0);
            return;
        }

        float leverRatio = joystick.LeverDistance / joystick.LeverRange;
        anim.SetFloat("LeverLength", leverRatio);

        // ĳ���� ���� ���� ����
        Vector3 moveDir = new Vector3(joystick.MoveDir.x, 0, joystick.MoveDir.y);
                
        // �̵��������� �ٷ� ȸ���ϱ����� ȸ�� �� ���� 
        transform.forward = moveDir;
        controller.Move(transform.forward * maxWalkSpeed * leverRatio * Time.deltaTime);
    }
}
