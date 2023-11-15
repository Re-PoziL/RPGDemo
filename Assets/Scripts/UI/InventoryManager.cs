using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using RPG.UI;
using UnityEngine.UI;
using UnityEditor;

public class InventoryManager:MonoBehaviour
{
    public static InventoryManager Instance;
    public Inventory inventory;
    public GameObject itemContainer;
    private const string filePath = "Assets/SaveData/inventory.json";
    private void Awake()
    {
        if(Instance!=null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    private void Start()
    {
        LoadInventory(filePath);
        foreach (var item in inventory.items)
        {
            GameObject go = Instantiate(itemContainer, transform);
            go.GetComponent<BagItem>().image.GetComponent<Image>().sprite = item.itemSprite;
            
        }
    }



    [MenuItem("CMD/SaveInventory")]
    public static void SaveInventory()
    {
        var jsonData = JsonUtility.ToJson(Instance.inventory);
        // 保存到文件
        System.IO.File.WriteAllText(filePath, jsonData);
    }

    
    public static void SaveInventory(Inventory inventory,string filePath)
    {
        var jsonData = JsonUtility.ToJson(inventory);
        // 保存到文件
        System.IO.File.WriteAllText(filePath, jsonData);
    }


    public void LoadInventory(string filePath)
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(jsonData, inventory);
            Debug.Log(jsonData);
        }
        
    }
}
