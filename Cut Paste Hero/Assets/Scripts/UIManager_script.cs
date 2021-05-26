using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_script : MonoBehaviour
{
    public Image[] inventory,
                    slots;
    public Color selectedSlotHighlight;

    private Color _visibleColour = new Color(1f, 1f, 1f, 1f),
        _invisibleColour = new Color(1f, 1f, 1f, 0f);

    private void Start()
    {
        foreach (var item in inventory)
        {
            item.color = _invisibleColour;
        }

        SelectSlot(0);
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
}
