using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class GroundSet : MonoBehaviour
{
    [SerializeField]
    private GameObject groundPrefab;

    [SerializeField]
    private MeshRenderer dummyPlaneMr;

    // ������ ũ��
    [SerializeField]
    private float prefabXscale;
    [SerializeField]
    private float prefabZscale;

    // x�࿡ ������ ������ ���� 
    private int xCount;
    // z�࿡ ������ ������ ����
    private int zCount;

    // x �� ���� (ù �������� ������ x��ǥ)
    private float xStartPos;
    // z �� ���� (ù �������� ������ z��ǥ)
    private float zStartPos;

    


    [ContextMenu("DrawGround")]
    public void DrawGround()
    {
        dummyPlaneMr.enabled = false;

        prefabXscale = groundPrefab.transform.localScale.x;
        prefabZscale = groundPrefab.transform.localScale.z;

        // ���� ���� ���� (���� ����)
        // ������ �������� 1 �Ǵ� 2�϶��� ���������� ����
        xStartPos = 4.5f * transform.localScale.x - 0.5f * (prefabXscale - 1);
        zStartPos = 4.5f * transform.localScale.z - 0.5f * (prefabZscale - 1);

        xCount = (int)(transform.localScale.x * 10 / prefabXscale);
        zCount = (int)(transform.localScale.z * 10 / prefabZscale);


        for (int z = 0; z < zCount; z++)
        {
            for(int x = 0; x < xCount; x++)
            {
                Vector3 groundPos = new Vector3(
                    transform.position.x - xStartPos + x * prefabXscale,
                    transform.position.y,
                    transform.position.z - zStartPos + z * prefabZscale);
                GameObject inst = Instantiate(groundPrefab, groundPos, Quaternion.identity);
                inst.transform.parent = transform;
            }
        }
    }

    [ContextMenu("ResetGround")]
    public void ResetGround()
    {
        dummyPlaneMr.enabled = true;
        while (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);
    }
}
