using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutPaste_script : MonoBehaviour
{
    public float reach;
    public bool cuttingEnabled;
    public GameObject pasteMarker;

    private GameObject[] inventory = new GameObject[5];

    private void Start()
    {
        pasteMarker.SetActive(false);
    }

    private void Update()
    {
        if (cuttingEnabled)
        {
            //Left click to cut
            if (Input.GetButtonDown("Fire1"))
            {
                RaycastHit hit = GetHit(Camera.main.transform.position, Camera.main.transform.forward.normalized, reach);

                if (hit.collider.gameObject && hit.collider.GetComponent<CuttableObj_script>() && hit.collider.GetComponent<CuttableObj_script>().canCut)
                {
                    Cut(hit.collider.gameObject);
                }
            }
            //Right click to paste
            if (Input.GetButton("Fire2"))
            {
                AdjustPastePoint(GetHit(Camera.main.transform.position, Camera.main.transform.forward.normalized, reach));
            }
            else if (Input.GetButtonUp("Fire2"))
            {
                Paste();
                pasteMarker.SetActive(false);
            }
        }
    }

    private void LateUpdate()
    {
        Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.position + (Camera.main.transform.forward.normalized * reach), Color.blue);
    }

    private RaycastHit GetHit(Vector3 start, Vector3 dir, float length)
    {
        RaycastHit hit;
        RaycastHit[] hitArray = Physics.RaycastAll(start, dir, length);

        foreach (var item in hitArray)
        {
            if (!item.collider.CompareTag("Player"))
            {
                return item;
            }
        }

        Physics.Raycast(start, dir, out hit, length);

        return hit;
    }

    private void Cut(GameObject obj)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (!inventory[i])
            {
                inventory[i] = obj;
                obj.SetActive(false);
                break;
            }
        }

        foreach (var item in inventory)
        {
            Debug.Log(item);
        }
    }

    private void AdjustPastePoint(RaycastHit hit)
    {
        pasteMarker.SetActive(true);
        Debug.Log(hit.collider);

        if (hit.collider)
        {
            pasteMarker.transform.position = hit.point;
            pasteMarker.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            //pasteMarker.transform.rotation = Quaternion.Euler(hit.normal);
            Debug.DrawLine(pasteMarker.transform.position, pasteMarker.transform.position + (hit.normal * 2), Color.red);
        }
        else
        {
            pasteMarker.SetActive(false);
        }
    }

    private void Paste()
    {
        if (inventory[0] && pasteMarker.activeInHierarchy)
        {
            inventory[0].transform.position = pasteMarker.transform.position;
            inventory[0].transform.rotation = pasteMarker.transform.rotation;
            inventory[0].SetActive(true);

            inventory[0] = null;
        }
    }

    public void SetCutting(bool enabled)
    {
        cuttingEnabled = enabled;
    }
}
