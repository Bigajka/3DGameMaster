using System;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private DialogueUI uiController;
    private DialogueNode _currentNode;

    public event Action OnDialogueEnded;
    public bool IsDialogueActive { get; private set; } // Flaga informuj?ca, czy trwa dialog

    private void Awake()
    {
        Instance = this;
    }

    public void StartDialogue(DialogueNode startingNode)
    {
        _currentNode = startingNode;
        IsDialogueActive = true;

        // Odblokowanie i pokazanie kursora myszy do klikania w UI
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        DisplayCurrentNode();
    }

    private void DisplayCurrentNode()
    {
        if (_currentNode == null)
        {
            EndDialogue();
            return;
        }

        if (!string.IsNullOrEmpty(_currentNode.TriggerFlagOnEnter))
        {
            StoryStateManager.Instance.SetFlag(_currentNode.TriggerFlagOnEnter);
        }

        uiController.RenderNode(_currentNode, SelectChoice);
    }

    public void SelectChoice(DialogueChoice choice)
    {
        _currentNode = choice.NextNode;
        DisplayCurrentNode();
    }

    private void EndDialogue()
    {
        IsDialogueActive = false;
        uiController.Hide();

        // Ponowne zablokowanie i ukrycie kursora po zako?czeniu dialogu
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        OnDialogueEnded?.Invoke();
    }
}