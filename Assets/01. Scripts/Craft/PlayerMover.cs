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
    private GameObject joystickOb;

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
            joystickOb.SetActive(value);
        } 
    }   

    private Vector2 inputDir;

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
        if (inputDir == Vector2.zero)
        {
            anim.SetBool("IsWalk", false);
            return;
        }

        anim.SetBool("IsWalk", true);
        float moveSpeed = isRun ? runSpeed : walkSpeed;
        transform.forward = new Vector3(inputDir.x, 0, inputDir.y);
        controller.Move(transform.forward * moveSpeed * Time.deltaTime);
    }
}
