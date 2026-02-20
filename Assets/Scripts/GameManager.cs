using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Cache;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class GameData
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public string Country { get; set; }
    [JsonProperty("work_experience")]
    public List<WorkExperience> WorkExperience { get; set; }
    [JsonProperty("education")]
    List<Education> Education { get; set; }
    [JsonProperty("terminal_entries")]
    public List<TerminalEntry> TerminalEntries { get; set; }

    public void ReplaceNewline()
    {
        foreach (var entry in TerminalEntries)
        {
            entry.Data = entry.Data.Replace("\\n", "\n");
        }

        foreach (var experience in WorkExperience)
        {
            experience.Description = experience.Description.Replace("\\n", "\n");
        }
    }
}

[Serializable]
public class WorkExperience
{
    public string Title { get; set; }
    public string Company { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string Description { get; set; }
}

[Serializable]
public class Education
{
    public string Degree { get; set; }
    public string Institution { get; set; }
    public string GraduationDate { get; set; }
}

[Serializable]
public class TerminalEntry
{
    public int ID { get; set; }
    public string Location { get; set; }
    public string Data { get; set; }
}

public enum Location
{
    entrance, 
    dev, 
    teaching
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    GameObject menu;

    [SerializeField]
    TMP_Text debug;

    public GameData gameData;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://nazcs2qrqff6cmdjxw4ub5ehje0aczbw.lambda-url.ap-southeast-1.on.aws/");
        yield return request.SendWebRequest();

        if(request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
            debug.text = "Error: " + request.error;
            yield break;
        }
        else
        {
            string data = request.downloadHandler.text;
            print(data);

            GameData x = JsonConvert.DeserializeObject<GameData>(data);
            gameData = x;
            gameData.ReplaceNewline();
            debug.text = "Data loaded successfully!";
            yield break;
        }
    }

    public void OpenMenu()
    {
        if (!menu.activeInHierarchy)
        {
            Time.timeScale = 0f;
            menu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            menu.SetActive(false);
        }
    }
}
 