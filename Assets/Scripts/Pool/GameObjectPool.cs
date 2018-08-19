using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 资源池、对象池
/// </summary>
[Serializable]
public class GameObjectPool {
    [SerializeField]
    public string name;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private int maxAmount;
    [NonSerialized]
    private List<GameObject> goList = new List<GameObject>();

    public GameObject GetInst()
    {
        foreach (GameObject go in goList)
        {
            if (go.activeInHierarchy == false)
            {
                go.SetActive(true);
                return go;
            }
        }
        if (goList.Count >= maxAmount)
        {
            GameObject.Destroy(goList[0]);
            goList.RemoveAt(0);
        }
        GameObject temp = GameObject.Instantiate(prefab) as GameObject;
        goList.Add(temp);
        return temp;
    }

}
