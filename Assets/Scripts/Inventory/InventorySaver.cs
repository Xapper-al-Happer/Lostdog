using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class InventorySaver : MonoBehaviour
{
    [SerializeField] private PlayerInventory myInventory;
    [SerializeField] private ScriptableObject myInventoryObject;

    [Header("Reset potion amount")]
    public List<InventoryItem> potions = new List<InventoryItem>();

    private void OnEnable()
    {
        myInventory.myInventory.Clear();
        LoadScriptables();
    }

    private void OnDisable()
    {
        SaveScriptables();
    }

    public void ResetScriptables()
    {
        myInventory.myInventory.Clear();

        for(int i = 0; i < potions.Count; i++)
        {
            potions[i].numberHeld = 0;
        }

        if(File.Exists(Application.persistentDataPath + "/inventory.inv"))
        {
            File.Delete(Application.persistentDataPath + "/invenotry.inv");
        }
    }

    public void SaveScriptables()
    {
        FileStream file = File.Create(Application.persistentDataPath + "/inventory.inv");
        BinaryFormatter binary = new BinaryFormatter();
        var json = JsonUtility.ToJson(myInventoryObject);
        Debug.Log(myInventoryObject);
        binary.Serialize(file, json);
        file.Close();
    }

    public void LoadScriptables()
    {

        if (File.Exists(Application.persistentDataPath + "/inventory.inv"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/inventory.inv", FileMode.Open);
            BinaryFormatter binary = new BinaryFormatter();
            JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file),
                myInventoryObject);
            file.Close();
        }
        else
        {
            myInventory.myInventory.Clear();
        }
    }
}
