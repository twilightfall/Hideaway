using JetBrains.Annotations;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TerminalDisplay : InteractionHandler
{
    [SerializeField]
    Canvas canvas;
    [SerializeField]
    TMP_Text displayText;
    [SerializeField]
    Canvas screen;

    [SerializeField]
    int TerminalNumber;
    [SerializeField]
    Location location;
    [SerializeField]
    TerminalEntry terminalEntry;

    public string textToDisplay;

    bool isTerminalActive = false;
    bool isDataLoaded = false;

    private void Start()
    {
        StartCoroutine(InitializeTerminalData());
    }

    IEnumerator InitializeTerminalData()
    {
        while (GameManager.Instance.gameData.TerminalEntries == null)
        {
            yield return null; // Wait until the data is loaded
        }

        terminalEntry = GameManager.Instance.gameData.TerminalEntries.Find(entry => entry.ID == TerminalNumber && entry.Location == location.ToString());
        
        isDataLoaded = true;

        if (terminalEntry != null)
        {
            displayText.text = terminalEntry.Data;
            yield break;
        }
        else
        {
            Debug.LogError($"Terminal entry not found for ID: {TerminalNumber} and Location: {location}");
            yield break;
        }
    }

    override public string SetInteractionText()
    {
        if (!isTerminalActive)
            return "Press 'E' to access terminal";
        else
            return base.SetInteractionText();
    }

    public override void Interact()
    {
        if(!isDataLoaded)
        {
            Debug.LogWarning("Terminal data is not loaded yet.");
            return;
        }

        print("Interacted with terminal");

        isTerminalActive = true;

        canvas.gameObject.SetActive(true);

        if (screen != null)
            screen.gameObject.SetActive(true);
    }
}
