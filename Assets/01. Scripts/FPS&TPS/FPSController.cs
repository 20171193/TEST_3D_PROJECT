using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System;

public class FPSController : PlayerController
{
    [Header("Components")]
    [SerializeField]
    private CharacterController controller;

    [SerializeField] 
    private CinemachineVirtualCamera fpsCamera;

    [SerializeField]
    private Transform aimTr;

    [Header("Specs")]
    [SerializeField]
    private float maxMovePower;
    [SerializeField]
    private float walkPower;
    [SerializeField]
    private float movePower;
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private float mouseSensitivity;

    [Header("Balancing")]
    [SerializeField]
    private Vector3 inputDir;
    [SerializeField]
    private Vector3 moveDir;
    [SerializeField]
    private Vector2 inputMouseDir;

    [SerializeField]
    private float ySpeed = 0f;
    [SerializeField]
    private bool isWalk = false;

    protected override void Awake()
    {
        base.Awake();

        // 플레이어 렌더러를 컬링
        // FPS는 자신을 카메라에 노출할 필요가 없음.
        // 총, 팔 등은 Player Layer에서 제외하기.
        Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("Player"));
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Move();
        Jump();
        Rotation();
        Aiming();
    }

    #region Input Action
    private void OnMove(InputValue value)
    {
        inputDir = value.Get<Vector2>();
        moveDir = new Vector3(inputDir.x, 0, inputDir.y);
    }
    private void Move()
    {
        anim.SetFloat("xSpeed", inputDir.x);
        anim.SetFloat("zSpeed", inputDir.z);

        if (moveDir == Vector3.zero)
        {
            anim.SetBool("IsMoving", false);
            return;
        }
        else
            anim.SetBool("IsMoving", true);

        float curMovePower = isWalk ? walkPower : movePower;
        controller.Move(transform.forward * curMovePower * moveDir.z * Time.deltaTime);
        controller.Move(transform.right * curMovePower * moveDir.x * Time.deltaTime);
    }

    private void OnWalk(InputValue value)
    {
        if (value.isPressed && state.IsGround)
            isWalk = true;
        else
            isWalk = false;

        anim.SetBool("IsWalking", isWalk);
    }

    private void OnJump(InputValue value)
    {
        if (!state.IsGround) return;

        ySpeed = jumpPower;
        Jump();
    }
    private void Jump()
    {
        if (state.IsGround) return;

        ySpeed += Physics.gravity.y * Time.deltaTime;
        controller.Move(Vector3.up * ySpeed * Time.deltaTime);
    }

    private void OnLook(InputValue value)
    {
       inputMouseDir = value.Get<Vector2>();
    }
    private void Aiming()
    {
        aimTr.Rotate(Vector3.right, -inputMouseDir.y * mouseSensitivity * Time.deltaTime);
    }
    private void Rotation()
    {
        transform.Rotate(Vector3.up, inputMouseDir.x * mouseSensitivity * Time.deltaTime);
    }

    #endregion
}
