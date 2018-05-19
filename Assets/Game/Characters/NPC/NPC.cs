using System;
using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// This class should be used as interface for interactions with any NPC. 
/// </summary>
public class NPC : MonoBehaviour, IInteractable<object, AbstractQuest>
{
    #region Private fields

    [CanBeNull] private QuestGiverController questGiverController;
    
    #endregion
    
    #region Unity callbacks
    
    // Use this for initialization
    void Start()
    {
        questGiverController = GetComponent<QuestGiverController>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    #endregion
    
    #region Public methods
    
    /// <summary>
    /// </summary>
    /// <param name="param">Should alway be null</param>
    /// <returns></returns>
    [CanBeNull]
    public AbstractQuest Interact([CanBeNull] object param = null)
    {
        return questGiverController != null ? questGiverController.GetQuest() : null;
    }
    
    #endregion
}