using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PikchinManager : MonoBehaviour
{
    [SerializeField] List<pikchin> selectedPikchins;
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

    //Selection
    public Image selectionImage;
    public CanvasScaler canvasScaler;
    Vector2 selectionStartPos;
    Vector2 selectionEndPos;
    bool selecting = false;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMousePosition();
        visualPointer.transform.position = hitPoint;

        //Select Dest
        if (Input.GetMouseButton(0))
        {
            SetPikchinDestination();
        }
        
        //Right Click drag to form box to select pikchins
        if (Input.GetMouseButtonDown(1))
        {
            StartSelection();
        }
        if (selecting && Input.GetMouseButtonUp(1))
        {
            EndSelection();
        }

        if(selecting)
        {
            DrawSelectionBox();
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

    void StartSelection()
    {
        selecting = true;
        selectionImage.enabled = true;
        selectionStartPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        print("Start: " + selectionStartPos);
    }

    void EndSelection()
    {
        selecting = false;
        selectionImage.enabled = false;
        selectionEndPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        print("End: " + selectionEndPos);

        //Unselect current pikchins
        foreach(pikchin pik in selectedPikchins)
        {
            pik.SetSelected(false);
        }
        selectedPikchins.Clear();

        //Select new pikchin in area
        pikchin[] pikchins = FindObjectsOfType<pikchin>(); //Can improve but eh, game jam
        foreach(pikchin pik in pikchins)
        {
            Vector2 pikScreenPos = mainCamera.WorldToViewportPoint(pik.transform.position);
            //Check if within a drawn box
            if((pikScreenPos.x > selectionStartPos.x && pikScreenPos.x < selectionEndPos.x && pikScreenPos.y > selectionStartPos.y && pikScreenPos.y < selectionEndPos.y)
                || (pikScreenPos.x > selectionEndPos.x && pikScreenPos.x < selectionStartPos.x && pikScreenPos.y > selectionStartPos.y && pikScreenPos.y < selectionEndPos.y)
                || (pikScreenPos.x > selectionStartPos.x && pikScreenPos.x < selectionEndPos.x && pikScreenPos.y > selectionEndPos.y && pikScreenPos.y < selectionStartPos.y)
                || (pikScreenPos.x > selectionEndPos.x && pikScreenPos.x < selectionStartPos.x && pikScreenPos.y > selectionEndPos.y && pikScreenPos.y < selectionStartPos.y))
            {
                selectedPikchins.Add(pik);
                pik.SetSelected(true);
            }
        }
    }

    void DrawSelectionBox()
    {
        Vector2 currentMousePos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        float width = Mathf.Abs(selectionStartPos.x - currentMousePos.x);
        float length = Mathf.Abs(selectionStartPos.y - currentMousePos.y);
        selectionImage.rectTransform.sizeDelta = new Vector2(width * 1920, length * 1080);

        float centerx = (selectionStartPos.x + currentMousePos.x) / 2;
        centerx = (centerx * canvasScaler.referenceResolution.x) - (canvasScaler.referenceResolution.x / 2);
        float centery = (selectionStartPos.y + currentMousePos.y) / 2;
        centery = (centery * canvasScaler.referenceResolution.y) - (canvasScaler.referenceResolution.y / 2);
        selectionImage.rectTransform.anchoredPosition = new Vector3(centerx, centery, 0);
    }

    void SetPikchinDestination()
    {
        foreach(pikchin pik in selectedPikchins)
        {
            pik.SetDestination(hitPoint);
        }
    }

    private void OnDrawGizmos()
    {

    }
}
