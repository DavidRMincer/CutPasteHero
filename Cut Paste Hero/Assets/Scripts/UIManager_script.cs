using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_script : MonoBehaviour
{
    public Image[] inventory,
                    slots;
    public Color selectedSlotHighlight;
    public Image crosshair;
    public float crosshairScale,
                    crosshairPulseSpeed;
    public AnimationCurve pulseCurve;
    public Text healthText;

    private Color _visibleColour = new Color(1f, 1f, 1f, 1f),
        _invisibleColour = new Color(1f, 1f, 1f, 0f);
    private bool _isPulsing = false;
    private float _crosshairDefaultScale;

    private void Start()
    {
        foreach (var item in inventory)
        {
            item.color = _invisibleColour;
        }

        SelectSlot(0);
        _crosshairDefaultScale = crosshair.transform.localScale.x;

        StartCoroutine(CrosshairPulseIEnum());
    }

    private IEnumerator CrosshairPulseIEnum()
    {
        while (true)
        {
            if (_isPulsing)
            {
                crosshair.transform.localScale 
                    = Vector3.Lerp(new Vector3(_crosshairDefaultScale, _crosshairDefaultScale, _crosshairDefaultScale), new Vector3(crosshairScale, crosshairScale, crosshairScale), pulseCurve.Evaluate(Time.time * crosshairPulseSpeed));
            }
            else
            {
                crosshair.transform.localScale = new Vector3(_crosshairDefaultScale, _crosshairDefaultScale, _crosshairDefaultScale);
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public void SelectSlot(int slot)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i == slot)
            {
                slots[i].color = selectedSlotHighlight;
            }
            else
            {
                slots[i].color = _visibleColour;
            }
        }
    }

    public void AddtoInventory(int slot, Sprite icon)
    {
        inventory[slot].color = _visibleColour;
        inventory[slot].sprite = icon;
    }

    public void RemoveFromInventory(int slot)
    {
        inventory[slot].color = _invisibleColour;
    }

    public void SetCrosshairPulsing(bool pulsing)
    {
        _isPulsing = pulsing;
    }

    public void UpdateHealth(int health)
    {
        healthText.text = health.ToString();
    }
}
