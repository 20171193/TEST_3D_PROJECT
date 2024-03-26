using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class GroundSet : MonoBehaviour
{
    [SerializeField]
    private GameObject groundPrefab;

    [SerializeField]
    private GameObject dummyPlane;

    // 너비 
    private int xWidth;
    private int zWidth;

    // 변의 길이
    private float xSideLength;
    private float zSideLength;

    private void Awake()
    {
        Destroy(dummyPlane);

        xSideLength = 4.5f * transform.localScale.x;
        zSideLength = 4.5f * transform.localScale.z;

        xWidth = (int)(transform.localScale.x * 10);
        zWidth = (int)(transform.localScale.z * 10);

        DrawGround();
    }

    private void DrawGround()
    {
        for(int z = 0; z < zWidth; z++)
        {
            for(int x = 0; x < xWidth; x++)
            {
                Vector3 groundPos = new Vector3(
                    transform.position.x - xSideLength + x,
                    transform.position.y,
                    transform.position.z - zSideLength + z);
                GameObject inst = Instantiate(groundPrefab, groundPos, Quaternion.identity);
                inst.transform.parent = transform;
            }
        }
    }
}
