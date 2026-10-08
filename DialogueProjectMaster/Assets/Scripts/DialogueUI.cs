using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private TextMeshProUGUI bodyText;
    [SerializeField] private Transform choicesContainer;
    [SerializeField] private Button choiceButtonPrefab;

    public void RenderNode(DialogueNode node, Action<DialogueChoice> onChoiceSelected)
    {
        dialogPanel.SetActive(true);
        speakerText.text = node.SpeakerName;
        bodyText.text = node.DialogueText;

        // Czyszczenie poprzednich opcji wyboru
        foreach (Transform child in choicesContainer)
        {
            Destroy(child.gameObject);
        }

        // Generowanie nowych przycisków
        foreach (var choice in node.Choices)
        {
            // Sprawdzenie flagi fabularnej
            if (!StoryStateManager.Instance.HasFlag(choice.RequiredFlag))
                continue;

            Button btn = Instantiate(choiceButtonPrefab, choicesContainer);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = choice.ChoiceText;
            btn.onClick.AddListener(() => onChoiceSelected(choice));
        }

        // Opcja zako?czenia rozmowy, je?li w?ze? nie ma ju? wyborów
        if (node.Choices.Count == 0)
        {
            Button endBtn = Instantiate(choiceButtonPrefab, choicesContainer);
            endBtn.GetComponentInChildren<TextMeshProUGUI>().text = "[Zako?cz rozmow?]";
            endBtn.onClick.AddListener(() => DialogueManager.Instance.SelectChoice(new DialogueChoice { NextNode = null }));
        }
    }

    public void Hide()
    {
        dialogPanel.SetActive(false);
    }
}