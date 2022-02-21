using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikchinManager : MonoBehaviour
{
    [SerializeField] pikchin[] selectedPikchins;
    Camera mainCamera;

    [HideInInspector] public Vector3 hitPoint = Vector3.zero;
    //[SerializeField] private Transform follow = default;
    [SerializeField] private int layerToCastTo = 6;
    //[SerializeField] private Vector3 followOffset = Vector3.zero;
    //public Transform target = default;
    //[SerializeField] private Vector3 targetOffset = Vector3.zero;
    //private LineRenderer line = default;
    //const int linePoints = 5;

    [Header("Visual")]
    public Transform visualPointer;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMousePosition();
        visualPointer.transform.position = hitPoint;

        if (Input.GetMouseButton(0))
        {
            SetPikchinDestination();
        }
    }

    void UpdateMousePosition()
    {
        RaycastHit hit;
        int layerMask = 1 << layerToCastTo;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 9999, layerMask))
        {
            hitPoint = hit.point;
           /* target.position = hit.point + targetOffset;
            target.up = Vector3.Lerp(target.up, hit.normal, .3f);
            for (int i = 0; i < linePoints; i++)
            {
                Vector3 linePos = Vector3.Lerp(follow.position + followOffset, target.position, (float)i / 5f);
                line.SetPosition(i, linePos);
            }*/
        }
    }

    void SetPikchinDestination()
    {
        foreach(pikchin pik in selectedPikchins)
        {
            pik.SetDestination(hitPoint);
        }
    }
}
