using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using RPG.UI;
using UnityEngine.UI;

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

        SaveInventory(inventory, filePath);
        /*if (inventory != null)
        {
            foreach (Item item in inventory.items)
            {
                GameObject go = Instantiate(itemContainer, transform);
                go.GetComponent<BagItem>().image.GetComponent<Image>().sprite = item.itemSprite;
            }
            SaveInventory(inventory, filePath);

        }*/
    }

    private void OnDestroy()
    {
     //   SaveInventory(inventory, filePath);
    }

    public static void SaveInventory(Inventory inventory,string filePath)
    {
        foreach (var item in inventory.items)
        {
            Debug.Log(item);
        }
        Item[] items = inventory.items.ToArray();
        // 序列化数组
        string jsonData = JsonUtility.ToJson(items);

        Debug.Log(jsonData);
        // 保存到文件
        System.IO.File.WriteAllText(filePath, jsonData);
    }


    public static Item[] LoadInventory(string filePath)
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            Item[] itemArray = JsonUtility.FromJson<Item[]>(jsonData);
            return itemArray;
        }
        else
        {
            return null;
        }
    }
}
