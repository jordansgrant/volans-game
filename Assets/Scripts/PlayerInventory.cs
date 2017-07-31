using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerInventory : MonoBehaviour
{
    public Image[] itemImages;
    public Item[] items;
    

    public const int numSlots = 6;    

    public void AddItem(Item newItem)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if(items[i] == null)
            {
                items[i] = newItem;
                itemImages[i].sprite = newItem.sprite;
                itemImages[i].enabled = true;
                return;
            }
        }
    }

    //Function to load inventory from memory
    public void loadPossibleItems()
    {
        items[0] = (Resources.Load(@"Items\projectileGun") as Item);
        return;
    }

    public void RemoveItem(Item removeItem)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == removeItem)
            {
                items[i] = null;
                itemImages[i].sprite = null;
                itemImages[i].enabled = false;
                return;
            }
        }
    }

    void awake()
    {
        itemImages = gameObject.GetComponents<Image>();
        items = gameObject.GetComponents<Item>();
    }

    void start()
    {
        loadPossibleItems();
        Debug.Log(items[0].name);
    }
}
