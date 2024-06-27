using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class ContributionsDataRepository
{
    private const string SAVE_FILE_NAME = "contributions_data.json";

    public static void SaveContributionsData(ContributionsData data)
    {
        string json = JsonConvert.SerializeObject(data);
        string path = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
        File.WriteAllText(path, json);
    }

    public static ContributionsData LoadContributionsData()
    {
        string path = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<ContributionsData>(json);
        }
        return null;
    }

    public static void ResetContributionsData()
    {
        string path = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}