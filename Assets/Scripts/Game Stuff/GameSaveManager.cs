using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{

    public GameObject pausepanel;

    [Header("Objects to save/delete")]
    public List<ScriptableObject> objects = new List<ScriptableObject>();

    [Header("Reset chest runtime values")]
    public List<BoolValue> chests = new List<BoolValue>();

    [Header("Reset wait screen")]
    public float fadeWait;
    public GameObject panel;

    [Header("Reset player values")]
    public ScriptableObject playerInventory;
    public Inventory inventory;
    public FloatValue hearts;
    public VectorValue playerPosition;

    public void ResetScriptables()
    {
        Debug.Log(Application.persistentDataPath);
        for(int i = 0; i < objects.Count; i ++)
        {
            if(File.Exists(Application.persistentDataPath +
                string.Format("/{0}.dat", i)))
            {
                File.Delete(Application.persistentDataPath +
                    string.Format("/{0}.dat", i));
                Debug.Log("DELETE");
                Debug.Log(Application.persistentDataPath);
            }
        }

        if (File.Exists(Application.persistentDataPath + "/generalinventory.dat"))
        {
            File.Delete(Application.persistentDataPath + "/generalinventory.dat");
        }

        for (int i = 0; i < chests.Count; i++)
        {
            chests[i].RuntimeValue = false;
        }

        inventory.coins = 0;
        inventory.bonusdamage = 0;
        inventory.numberOfKeys = 0;

        inventory.bonusmagic = 0;
        inventory.maxMagic = 10;
        inventory.currentMagic = 10;

        inventory.items.Clear();

        hearts.RuntimeValue = 8;


        pausepanel.SetActive(false);
        Time.timeScale = 1f;
        StartCoroutine(FadeCo());
    }

    public IEnumerator FadeCo()
    {
        if (panel != null)
        {
            Instantiate(panel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            playerPosition.initialValue = playerPosition.defaultValue;
        }
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    private void OnEnable()
    {
        LoadScriptables();
        Debug.Log(Application.persistentDataPath);
    }

    private void OnDisable()
    {
        SaveScriptables();
    }

    public void SaveScriptables()
    {
        for (int i = 0; i < objects.Count; i ++)
        {
            FileStream file = File.Create(Application.persistentDataPath +
                string.Format("/{0}.dat", i));
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(objects[i]);
            binary.Serialize(file, json);
            file.Close();
        }

        FileStream generalinventoryfile = File.Create(Application.persistentDataPath + "/generalinventory.dat");
        BinaryFormatter binaryinventory = new BinaryFormatter();
        var jsoninventory = JsonUtility.ToJson(playerInventory);
        binaryinventory.Serialize(generalinventoryfile, jsoninventory);
        generalinventoryfile.Close();
    }

    public void LoadScriptables()
    {
        Debug.Log(Application.persistentDataPath);
        for(int i = 0; i < objects.Count; i ++)
        { 
            if(File.Exists(Application.persistentDataPath +
                string.Format("/{0}.dat", i)))
            {
                FileStream file = File.Open(Application.persistentDataPath +
                    string.Format("/{0}.dat", i), FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file),
                    objects[i]);
                file.Close();
            }
        }

        if (File.Exists(Application.persistentDataPath + "/generalinventory.dat"))
        {
            Debug.Log("FILE EXISTS /generalinventory.dat");
            FileStream generalinventoryfile = File.Open(Application.persistentDataPath + "/generalinventory.dat", FileMode.Open);
            BinaryFormatter binaryinventory = new BinaryFormatter();
            JsonUtility.FromJsonOverwrite((string)binaryinventory.Deserialize(generalinventoryfile), playerInventory);
            generalinventoryfile.Close();
        }
    }

}
