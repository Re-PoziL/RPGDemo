using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using RPG.UI;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;

public class BagManager:MonoBehaviour
{
    public static BagManager Instance;
    public Inventory bag;
    public GameObject itemContainer;
    private const string filePath = "Assets/SaveData/bag.json";
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
        InitBag();
    }

    private void InitBag()
    {
        foreach (var item in bag.items)
        {
            Transform child = transform.Find(item.name);
            if(child!=null)
            {
                int number = int.Parse(child.GetComponent<BagItem>().numberText.text);
                number++;
                child.GetComponent<BagItem>().numberText.text = number.ToString();
            }
            else
            {
                GameObject go = Instantiate(itemContainer, transform);
                go.GetComponent<BagItem>().image.GetComponent<Image>().sprite = item.itemSprite;
                go.name = item.name;
            }

        }
    }

    public void BuyToStory(Item item)
    {
        AdditemToBag(item);
    }

    private void AddToBagInventory(Item item)
    {
        bag.items.Add(item);
    }

    private void AdditemToBag(Item item)
    {
        
        if (bag.items.Contains(item))
        {
            Transform child = transform.Find(item.name);
            int number = int.Parse(child.GetComponent<BagItem>().numberText.text);
            number++;
            child.GetComponent<BagItem>().numberText.text = number.ToString();
        }
        else
        {
            GameObject go = Instantiate(itemContainer, transform);
            go.GetComponent<BagItem>().image.GetComponent<Image>().sprite = item.itemSprite;
            go.name = item.name;
        }
        AddToBagInventory(item);
    }


    [MenuItem("CMD/SaveBag")]
    public static void SaveInventory()
    {
        var jsonData = JsonUtility.ToJson(Instance.bag);
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
            JsonUtility.FromJsonOverwrite(jsonData, bag);
            Debug.Log(jsonData);
        }
        
    }
}
