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

    public static T LoadData<T>(string path)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, path);

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
}
