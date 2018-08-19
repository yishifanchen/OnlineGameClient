using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class PoolManagerEditor {
    /// <summary>
    /// 创建资源池列表配置文件
    /// </summary>
    [MenuItem("Manager/Creat GameobjectPoolConfig")]
    static void CreatGameobjectPoolList()
    {
        GameObjectPoolList poolList = ScriptableObject.CreateInstance<GameObjectPoolList>();
        string path = PoolManager.PoolConfigPath;
        if (File.Exists(path))
        {
            if(EditorUtility.DisplayDialog("提示", path+"资源池文件已存在，是否覆盖！", "是", "否"))
            {
                AssetDatabase.CreateAsset(poolList, path);
                AssetDatabase.SaveAssets();
                Debug.Log("资源池列表配置文件覆盖成功");
            }
        }
        else
        {
            AssetDatabase.CreateAsset(poolList, path);
            AssetDatabase.SaveAssets();
            Debug.Log("资源池列表配置文件创建成功");
        }
    }
}
