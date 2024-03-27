using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDigger : MonoBehaviour
{
    // 캘 수 있는 모든 오브젝트에 적용할 머터리얼
    [SerializeField]
    private Material diggableMT;

    [SerializeField]
    private GameObject targetObject;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Diggable"))
        {
            targetObject = other.gameObject;
            targetObject.GetComponent<DiggableObject>().OnTargeted();
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            targetObject.GetComponent<DiggableObject>().OffTargeted();
            targetObject = null;
        }
    }

}
