using UnityEngine;
using UnityEngine.InputSystem;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private DialogueNode rootDialogue;
    private bool _playerInRange;

    private void Update()
    {
        if (_playerInRange && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            DialogueManager.Instance.StartDialogue(rootDialogue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) _playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) _playerInRange = false;
    }
}