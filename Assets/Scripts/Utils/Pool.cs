using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PoolItem
{
    public string name;
    public int count;
    public GameObject perfab;
}

public class Pool : SingletonMono<Pool>
{

   [SerializeField][Header("预制体")]private List<PoolItem> pools = new List<PoolItem>();

    //用于存每个不同类型的实例
    private Dictionary<string, Queue<GameObject>> poolDic = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        Init();
    }

    //初始化对象池
    private void Init()
    {
        //常规判断
        if(pools.Count == 0)
        {
            return;
        }
        
        foreach (var pool in pools)
        {
            if (pool.perfab == null)
                continue;
            poolDic.Add(pool.name, new Queue<GameObject>());
            for (int i = 0; i < pool.count; i++)
            {
                poolDic[pool.name].Enqueue(pool.perfab); 
                GameObject go = Instantiate(pool.perfab,transform);
                go.name = pool.name;
                go.SetActive(false);
            }
        }
    }

    public GameObject Spawn(string name)
    {
        if(poolDic.ContainsKey(name))
        {
            if(poolDic[name].Peek().activeSelf)
            {
                GameObject newPool = Instantiate(poolDic[name].Peek(), transform);
                newPool.name = name;
                poolDic[name].Enqueue(newPool);
                newPool.SetActive(true);
                return newPool;
            }
            else
            {
                GameObject pool = poolDic[name].Dequeue();
                poolDic[name].Enqueue(pool);
                pool.SetActive(true);
                return pool;
            }
        }
        return null;
    }


    public void Recycle(GameObject pool)
    {
        pool.transform.position = Vector3.zero;
        pool.transform.rotation = Quaternion.identity;
        pool.SetActive(false);
    }
}
