using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggableObject : MonoBehaviour
{
    [SerializeField]
    private Material diggableMT;

    private MeshRenderer meshRenderer;
    private Material[] originMT;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originMT = meshRenderer.materials;
    }

    public void OnTargeted()
    {
        meshRenderer.material = diggableMT;
    }
    public void OffTargeted()
    {
        meshRenderer.materials = originMT;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    OnTargeted();
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    OffTargeted();
    //}
}
