using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 资源池管理器
/// </summary>
public class PoolManager :BaseManager{
    //private static PoolManager _instance;
    //public static PoolManager Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = new PoolManager();
    //        }
    //        return _instance;
    //    }
    //}
    private static string poolConfigPathPrefix= "Assets/Resources/";
    private const string poolConfigPathMiddle = "Pool/GameobjectPool";
    private const string poolConfigPathPostfix = ".asset";
    public static string PoolConfigPath
    {
        get { return poolConfigPathPrefix + poolConfigPathMiddle + poolConfigPathPostfix; }
    }
    private Dictionary<string, GameObjectPool> poolDict;
    /// <summary>
    /// 构造函数
    /// </summary>
    public PoolManager(GameFacade facade) : base(facade) { }
    public override void OnInit()
    {
        GameObjectPoolList poolList = Resources.Load<GameObjectPoolList>(poolConfigPathMiddle);
        poolDict = new Dictionary<string, GameObjectPool>();
        foreach (GameObjectPool pool in poolList.poolList)
        {
            poolDict.Add(pool.name, pool);
        }
    }
    /// <summary>
    /// 获得实例
    /// </summary>
    /// <param name="poolName"></param>
    /// <returns></returns>
    public GameObject GetInst(string poolName)
    {
        GameObjectPool pool;
        if (poolDict.TryGetValue(poolName,out pool))
        {
            return pool.GetInst();
        }
        Debug.LogWarning("Pool:" + poolName + "is not exists");
        return null;
    }
}
