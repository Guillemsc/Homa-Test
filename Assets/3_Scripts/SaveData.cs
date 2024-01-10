using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

public static class SaveData
{
    static readonly JsonSerializerSettings SerializeSettings = new JsonSerializerSettings()
    {
        NullValueHandling = NullValueHandling.Ignore,
        TypeNameHandling = TypeNameHandling.Auto,
    };
    
    public static int CurrentLevel
    {
        get {
            return PlayerPrefs.GetInt("CurrentLevel", 1);
        }
        set {
            PlayerPrefs.SetInt("CurrentLevel", value);
        }
    }

    public static float PreviousHighscore
    {
        get {
            return PlayerPrefs.GetFloat("PreviousHighscore", 0);
        }
        set {
            PlayerPrefs.SetFloat("PreviousHighscore", value);
        }
    }

    public static int CurrentColorList
    {
        get {
            return PlayerPrefs.GetInt("CurrentColorList", 0);
        }
        set {
            PlayerPrefs.SetInt("CurrentColorList", value);
        }
    }

    public static int VibrationEnabled
    {
        get {
            return PlayerPrefs.GetInt("VibrationEnabled", 1);
        }
        set {
            PlayerPrefs.SetInt("VibrationEnabled", value);
        }
    }
    
    public static void Save<T>(string key, T value)
    {
        string serialized = JsonConvert.SerializeObject(value, SerializeSettings);
        PlayerPrefs.SetString(key, serialized);
    }
    
    public static T Load<T>(string key, T defaultValue)
    {
        bool exists = PlayerPrefs.HasKey(key);

        if (!exists)
        {
            return defaultValue;
        }
        
        string serialized = PlayerPrefs.GetString(key);
        T item = JsonConvert.DeserializeObject<T>(serialized, SerializeSettings);
        return item;
    }
}
