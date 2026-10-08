using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueChoice
{
    [TextArea(1, 3)]
    public string ChoiceText;      // klik gracza
    public DialogueNode NextNode;   // Dokad prowadzi wybor
    public string RequiredFlag;     // Opcjonalna flaga (np. "has_key")
}

[CreateAssetMenu(fileName = "NewDialogueNode", menuName = "Dialogue/Node")]
public class DialogueNode : ScriptableObject
{
    public string SpeakerName;
    [TextArea(3, 6)]
    public string DialogueText;    // Wypowiedz NPC
    public List<DialogueChoice> Choices = new();
    public string TriggerFlagOnEnter; // Opcjonalnie: ustawia stan w grze po dotarciu tutaj
}
