using System.Collections.Generic;
using UnityEngine;

public class StoryStateManager : MonoBehaviour
{
    public static StoryStateManager Instance { get; private set; }
    private readonly HashSet<string> _activeFlags = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetFlag(string flag)
    {
        if (!string.IsNullOrEmpty(flag)) _activeFlags.Add(flag);
    }

    public bool HasFlag(string flag)
    {
        if (string.IsNullOrEmpty(flag)) return true;
        return _activeFlags.Contains(flag);
    }
}