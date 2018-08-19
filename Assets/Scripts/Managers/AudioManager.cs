using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 音效管理器
/// </summary>
public class AudioManager:BaseManager
{
    //static 静态  const是常量
    private static string audioTextPathPrefix = Application.dataPath + "\\Resources\\";
    private const string audioTextPathMiddle = "Audio\\AudioList";
    private const string audioTextPathPostfix = ".txt";
    public static string AudioTextPath
    {
        get
        {
            return audioTextPathPrefix + audioTextPathMiddle + audioTextPathPostfix;
        }
    }

    public const string Sound_ButtonClick = "ButtonClick";
    public const string Sound_Sound_Alert = "Alert";
    public const string Sound_Bg_Fast = "Bg(fast)";
    public const string Sound_Bg_Moderate = "Bg(moderate)";
    public const string Sound_Miss = "Miss";
    public const string Sound_ShootPerson = "ShootPerson";
    public const string Sound_Timer = "Timer";

    private Dictionary<string, AudioClip> audioClipDict = new Dictionary<string, AudioClip>();
    /// <summary>
    /// 是否静音
    /// </summary>
    public bool isMute = false;

    private AudioSource bgAudioSource;
    private AudioSource normalAudioSource;



    public AudioManager(GameFacade facade) : base(facade) { }
    public override void OnInit()
    {
        LoadAudioClip();
        GameObject audioSourceGO = new GameObject("AudioSource(GameObject)");
        bgAudioSource = audioSourceGO.AddComponent<AudioSource>();
        normalAudioSource = audioSourceGO.AddComponent<AudioSource>();
    }
    void LoadAudioClip()
    {
        audioClipDict = new Dictionary<string, AudioClip>();
        TextAsset ta = Resources.Load<TextAsset>(audioTextPathMiddle);
        string[] lines = ta.ToString().Split('\n');
        foreach(string line in lines)
        {
            if (string.IsNullOrEmpty(line)) continue;
            string[] keyvalues = line.Split(':');
            string key = keyvalues[0];
            AudioClip value = Resources.Load<AudioClip>(keyvalues[1]);
            audioClipDict.Add(key,value);
        }
    }
    public void PlayBgSound(string soundName)
    {
        if (isMute) return;
        AudioClip ac;
        audioClipDict.TryGetValue(soundName, out ac);
        if (ac != null)
        {
            PlaySound(bgAudioSource,ac,0.5f,true);
        }
    }
    public void PlayNormalSound(string soundName,float volume=1)
    {
        if (isMute) return;
        AudioClip ac;
        audioClipDict.TryGetValue(soundName, out ac);
        if (ac != null)
        {
            PlaySound(normalAudioSource,ac, volume);
        }
    }
    private void PlaySound(AudioSource audioSource,AudioClip clip,float volume,bool loop=false)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = loop;
        audioSource.Play();
    }
}
