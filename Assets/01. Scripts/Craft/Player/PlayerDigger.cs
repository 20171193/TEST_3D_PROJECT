using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDigger : MonoBehaviour
{
    // Ķ �� �ִ� ��� ������Ʈ�� ������ ���͸���
    [SerializeField]
    private DiggableObject targetObject;
    public DiggableObject TargetObject { get { return targetObject; } }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Diggable"))
        {
            // ������Ʈ�� �Ҵ�
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
