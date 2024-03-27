using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDigger : MonoBehaviour
{
    // 캘 수 있는 모든 오브젝트에 적용할 머터리얼
    [SerializeField]
    private DiggableObject targetObject;
    public DiggableObject TargetObject { get { return targetObject; } }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Diggable"))
        {
            // 오브젝트를 할당
            targetObject = other.gameObject.GetComponent<DiggableObject>();
            if (targetObject == null)
            {

            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            targetObject.GetComponent<DiggableObject>()?.OffTargeted();
            targetObject = null;
        }
    }

}
