using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Cache;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class GameData
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public string Country { get; set; }
    [JsonProperty("work_experience")]
    public List<WorkExperience> WorkExperience { get; set; }
    [JsonProperty("terminal_entries")]
    public object TerminalEntries { get; set; }
}

public class WorkExperience
{
    public string Title { get; set; }
    public string Company { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string Description { get; set; }
}

public class TerminalEntry
{
    public string Location { get; set; }
    public string Data { get; set; }
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
    }

    private void Start()
    {
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://nazcs2qrqff6cmdjxw4ub5ehje0aczbw.lambda-url.ap-southeast-1.on.aws/");
        yield return request.SendWebRequest();

        if(request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            string data = request.downloadHandler.text;
            print(data);
            GameData x = JsonConvert.DeserializeObject<GameData>(data);

            //var x = JsonUtility.FromJson<GameData>(data);
            gameData = x;
            print(x.WorkExperience[0].Title);

            //Debug.Log("Response: " + request.downloadHandler.text);
        }
    }
}
 