using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDigger : MonoBehaviour
{
    // Ķ �� �ִ� ��� ������Ʈ�� ������ ���͸���
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
