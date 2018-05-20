using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Player
{
    #region Properties
    
    [NotNull]
    public List<AbstractQuest> Quests
    {
        get { return quests; }
    }    
    
    #endregion
    
    #region Private fields
    
    private readonly List<AbstractQuest> quests = new List<AbstractQuest>();

    #endregion
    
    #region Public methods

    public void AcceptQuest([NotNull] AbstractQuest quest)
    {
        quest.ProgressState = AbstractQuest.State.IN_PROGRESS;
        Quests.Add(quest);
    }

    public void CompleteQuest([NotNull] AbstractQuest quest)
    {
        quest.ProgressState = AbstractQuest.State.COMPLETED;
        Quests.Remove(quest);
    }

    public void CancelQuest([NotNull] AbstractQuest quest)
    {
        quest.ProgressState = AbstractQuest.State.AVAILABLE_TO_PICK;
        Quests.Remove(quest);
    }
    
    #endregion
}