using UnityEngine;
using System.IO;
using RPG.UI;
using UnityEngine.UI;
using UnityEditor;

public class StoreManager : MonoBehaviour
{
    public static StoreManager Instance;
    public Inventory store;
    public GameObject itemContainer;
    private const string filePath = "Assets/SaveData/store.json";
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    private void Start()
    {

        LoadInventory(filePath);
        InitBag();
    }

    private void InitBag()
    {
        foreach (var item in store.items)
        {
            Transform child = transform.Find(item.name);
            if (child != null)
            {
                int number = int.Parse(child.GetComponent<StoreItem>().numberText.text);
                number++;
                child.GetComponent<StoreItem>().numberText.text = number.ToString();
            }
            else
            {
                GameObject go = Instantiate(itemContainer, transform);
                go.GetComponent<StoreItem>().image.GetComponent<Image>().sprite = item.itemSprite;
                go.name = item.name;
                go.GetComponent<StoreItem>().item = item;
            }

        }
    }

    public void Buy(Item item)
    {
        store.items.Remove(item);
        BagManager.Instance.BuyToStory(item);
    }

    [MenuItem("CMD/SaveStore")]
    public static void SaveInventory()
    {
        var jsonData = JsonUtility.ToJson(Instance.store);
        // 保存到文件
        System.IO.File.WriteAllText(filePath, jsonData);
    }


    public static void SaveInventory(Inventory inventory, string filePath)
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
            JsonUtility.FromJsonOverwrite(jsonData, store);
            Debug.Log(jsonData);
        }

    }
}
