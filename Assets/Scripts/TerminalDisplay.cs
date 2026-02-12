using TMPro;
using UnityEngine;

public class TerminalDisplay : InteractionHandler
{
    [SerializeField]
    Canvas canvas;
    [SerializeField]
    TMP_Text displayText;

    override public string SetInteractionText()
    {
        return "Press 'E' to access terminal";
    }

    public override void Interact()
    {
        print("Interacted with terminal");
        canvas.gameObject.SetActive(true);
    }
}
