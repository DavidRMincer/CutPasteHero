using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutPaste_script : MonoBehaviour
{
    public float reach;
    public bool cuttingEnabled;
    public GameObject pasteMarker;
    public UIManager_script ui;

    private GameObject[] inventory = new GameObject[5];
    private int _invetoryIndex = 0;
    private float _markerRotation = 0;

    private void Start()
    {
        pasteMarker.SetActive(false);
    }

    private void Update()
    {
        if (cuttingEnabled)
        {
            //Set crosshair pulsing
            RaycastHit hit = GetHit(Camera.main.transform.position, Camera.main.transform.forward.normalized, reach);
            //Debug.Log(hit.collider.gameObject);

            if (hit.collider && hit.collider.GetComponent<CuttableObj_script>() && hit.collider.GetComponent<CuttableObj_script>().canCut)
            {
                ui.SetCrosshairPulsing(true);
                //Left click to cut
                if (Input.GetButtonDown("Fire1"))
                {
                    Cut(hit.collider.gameObject);
                }
            }
            else
            {
                ui.SetCrosshairPulsing(false);
            }
            //Right click to paste
            if (Input.GetButton("Fire2"))
            {
                if (inventory[_invetoryIndex] != null)
                {
                    AdjustPastePoint(hit);
                }
            }
            else if (Input.GetButtonUp("Fire2"))
            {
                Paste();
                pasteMarker.SetActive(false);
            }
            //Scroll
            else
            {
                if (Input.mouseScrollDelta.y != 0f)
                {
                    _invetoryIndex += -Mathf.FloorToInt(Input.mouseScrollDelta.y);
                    if (_invetoryIndex < 0)
                    {
                        _invetoryIndex = inventory.Length - 1;
                    }
                    else if (_invetoryIndex > (inventory.Length - 1))
                    {
                        _invetoryIndex = 0;
                    }

                    ui.SelectSlot(_invetoryIndex);
                }
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    _invetoryIndex = 0;
                    ui.SelectSlot(_invetoryIndex);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    _invetoryIndex = 1;
                    ui.SelectSlot(_invetoryIndex);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    _invetoryIndex = 2;
                    ui.SelectSlot(_invetoryIndex);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    _invetoryIndex = 3;
                    ui.SelectSlot(_invetoryIndex);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    _invetoryIndex = 4;
                    ui.SelectSlot(_invetoryIndex);
                }
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
        if (inventory[_invetoryIndex] == null)
        {
            AssignObjToInventory(obj, _invetoryIndex);
        }
        else
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                {
                    AssignObjToInventory(obj, i);
                    break;
                }
            }
        }

        foreach (var item in inventory)
        {
            Debug.Log(item);
        }
    }

    private void AssignObjToInventory(GameObject obj, int slot)
    {
        inventory[slot] = obj;
        obj.SetActive(false);
        //Destroy(obj);

        ui.AddtoInventory(slot, inventory[slot].GetComponent<CuttableObj_script>().inventoryIcon);
    }

    private void AdjustPastePoint(RaycastHit hit)
    {
        Debug.Log("Adjust Pasting");
        pasteMarker.SetActive(true);
        Debug.Log(hit.collider);
        _markerRotation += Input.mouseScrollDelta.y * 10;

        if (hit.collider)
        {
            pasteMarker.transform.position = hit.point;

            pasteMarker.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            pasteMarker.transform.RotateAround(hit.point, hit.normal, _markerRotation);
            
            Debug.DrawLine(pasteMarker.transform.position, pasteMarker.transform.position + (hit.normal * 2), Color.red);
        }
        else
        {
            pasteMarker.SetActive(false);
        }
    }

    private void Paste()
    {
        if (inventory[_invetoryIndex] && pasteMarker.activeInHierarchy)
        {
            Quaternion pasteRotation = pasteMarker.transform.rotation;

            GameObject pastedObj = Instantiate(inventory[_invetoryIndex], pasteMarker.transform.position, pasteRotation);
            //pastedObj.transform.localRotation
            //    = Quaternion.LookRotation(new Vector3(pastedObj.transform.position.x - transform.position.x, 0f, pastedObj.transform.position.z - transform.position.z).normalized, pasteMarker.transform.up);
            pastedObj.SetActive(true);
            Destroy(inventory[_invetoryIndex]);

            ui.RemoveFromInventory(_invetoryIndex);

            inventory[_invetoryIndex] = null;
        }
    }

    public void SetCutting(bool enabled)
    {
        cuttingEnabled = enabled;

        for (int i = 0; i < inventory.Length; i++)
        {
            ui.inventory[i].gameObject.SetActive(enabled);
            ui.slots[i].gameObject.SetActive(enabled);
        }
    }
}
