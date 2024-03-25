using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    protected PlayerState state;
    
    protected virtual void Awake()
    {
        state = transform.AddComponent<PlayerState>();
    }
}
