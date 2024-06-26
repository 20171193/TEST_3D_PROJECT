using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [Header("Balacing")]
    [SerializeField]
    private bool isGround;
    public bool IsGround { get { return isGround; } }

    private void GroundCheck()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hitInfo, 1.2f, LayerMask.NameToLayer("Ground")))
        {
            Debug.Log(hitInfo.collider.gameObject.name);
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }

    private void DebugDraw()
    {
        Debug.DrawRay(transform.position,-transform.up * 1.2f, Color.red, 0.5f);
    }


    private void Update()
    {
        GroundCheck();
        DebugDraw();
    }
}
