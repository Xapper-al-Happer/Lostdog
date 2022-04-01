using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Inventory : ScriptableObject {

    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfKeys;
    public int coins;
    public float maxMagic = 10;
    public float bonusdamage;
    public float bonusmagic;
    public float currentMagic;

    public void OnEnable()
    {
        //currentMagic = maxMagic;
    }

    public void ReduceMagic(float magicCost)
    {
        currentMagic -= magicCost;
    }

    public bool CheckForItem(Item item)
    { 
        if(items.Contains(item))
        {
            return true;
        }
        return false;
    }

    public void AddItem(Item itemToAdd)
    {
        // Is the item a key?
        if(itemToAdd.isKey)
        {
            numberOfKeys++;
        }
        else if (itemToAdd.id == 1)
        {
            //Magic increase
            bonusmagic =+ 5;
            maxMagic += bonusmagic;
        }
        else if (itemToAdd.id == 3)
        {
            //Sword damage
            bonusdamage += 1;
        }
        else
        {
            if(!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }
    }

}
