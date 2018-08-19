using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class AudioWindowEditor : EditorWindow {
    [MenuItem("Manager/AudioManager")]
    static void CreatWindow()
    {
        AudioWindowEditor window = EditorWindow.GetWindow<AudioWindowEditor>("音效管理");
        window.Show();
    }

    void Awake()
    {
        LoadAudioList();
    }

    private string audioName;
    private string audioPath;
    private Dictionary<string, string> audioDict = new Dictionary<string, string>();

    int width = 200;
    int height = 20;
    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("音效名称",GUILayout.Width(width), GUILayout.Height(height));
        GUILayout.Label("音效路径", GUILayout.Width(width), GUILayout.Height(height));
        GUILayout.Label("操作", GUILayout.Width(width), GUILayout.Height(height));
        GUILayout.EndHorizontal();

        foreach(string key in audioDict.Keys)
        {
            string value;
            audioDict.TryGetValue(key,out value);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(key, GUILayout.Width(width), GUILayout.Height(height));
            GUILayout.Label(value, GUILayout.Width(width), GUILayout.Height(height));
            if (GUILayout.Button("删除", GUILayout.Width(width/4), GUILayout.Height(height)))
            {
                audioDict.Remove(key);
                SaveAudioList();
                return;
            }
            EditorGUILayout.EndHorizontal();
        }

        audioName = EditorGUILayout.TextField("音效名称：", audioName, GUILayout.Width(width*3), GUILayout.Height(height));
        audioPath = EditorGUILayout.TextField("音效路径：", audioPath, GUILayout.Width(width*3), GUILayout.Height(height));
        if (GUILayout.Button("添加"))
        {
            object o = Resources.Load(audioPath);
            if (o == null)
            {
                Debug.LogWarning("音效不存在于"+audioPath+",添加不成功");
                audioPath = "";
            }
            else
            {
                if (audioDict.ContainsKey(audioName))
                {
                    Debug.LogWarning("音效：'"+audioName+"' 已经存在于字典，请修改");
                }
                else if(audioDict.ContainsValue(audioPath))
                {
                    Debug.LogWarning("路径："+audioPath+" 已经有一个对应的音效存在于字典中，请修改");
                }
                else
                {
                    audioDict.Add(audioName, audioPath);
                    SaveAudioList();
                }
            }
        }
    }
    //窗口面板更新的时候被调用
    void OnInspectorUpdate()
    {
        LoadAudioList();
    }
    
    /// <summary>
    /// 保存音效列表文件
    /// </summary>
    private void SaveAudioList()
    {
        StringBuilder sb = new StringBuilder();
        foreach (string key in audioDict.Keys)
        {
            string value;
            audioDict.TryGetValue(key, out value);
            sb.Append(key + ":" +value+"\n");
        }
        if (!File.Exists(AudioManager.AudioTextPath)) File.Create(AudioManager.AudioTextPath);
        else File.WriteAllText(AudioManager.AudioTextPath, sb.ToString());
    }
    /// <summary>
    /// 加载音效列表文件
    /// </summary>
    private void LoadAudioList()
    {
        audioDict = new Dictionary<string, string>();
        if (!File.Exists(AudioManager.AudioTextPath))
        {
            Debug.LogWarning("路径:"+AudioManager.AudioTextPath+"  不存在，请检查！");
            return;
        }
        else
        {
            string[] lines = File.ReadAllLines(AudioManager.AudioTextPath);
            foreach(string line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                string[] keyvalue = line.Split(':');
                audioDict.Add(keyvalue[0], keyvalue[1]);
            }
        }
    }
}
