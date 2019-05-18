using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public static class SaveDataManager
{
    public static void SaveData<T>(T data, string path)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, path);
        string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);

        using (StreamWriter streamWriter = File.CreateText(fullPath))
        {
            streamWriter.Write(jsonString);
        }
    }

    private static T DoLoad<T>(string fullPath)
    {
        if (!File.Exists(fullPath))
        {
            using (StreamWriter streamWriter = File.CreateText(fullPath))
            {
                streamWriter.Write("[]");
            }
        }

        using (StreamReader streamReader = File.OpenText(fullPath))
        {
            string jsonString = streamReader.ReadToEnd();
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }

    public static T LoadData<T>(string path)
    {
        return DoLoad<T>(Path.Combine(Application.persistentDataPath, path));
    }
    
    public static T LoadAssetData<T>(string path)
    {
        // TODO: Make sure not to rely on resource folder, it should be avoided in production.
        Debug.LogWarning("Loading from resources isn't a good idea in production!");
        Debug.LogWarning(path);
        string jsonString = Resources.Load<TextAsset>(path).text;
        return JsonConvert.DeserializeObject<T>(jsonString);
    }
}
