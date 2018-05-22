using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Quests;
using JetBrains.Annotations;
using ModestTree;
using UnityEngine;

public class QuestGiverSystem : MonoBehaviour
{
    #region Editor tweakable fields
    
    [NotNull]
    [SerializeField]
    private List<AbstractQuest> quests;
    
    #endregion
    
    #region Unity callbacks

    private void Start()
    {
        quests.ForEach(quest => quest.ProgressStateChanged += OnQuestProgressStateChanged);
    }

    private void OnDestroy()
    {
        quests.ForEach(quest => quest.ProgressStateChanged -= OnQuestProgressStateChanged);
    }

    #endregion
    
    #region Public methods

    [CanBeNull]
    public AbstractQuest GetQuest()
    {
        return quests.IsEmpty() ? null : quests[0];
    }
    
    #endregion
    
    #region Private methods

    private void OnQuestProgressStateChanged(AbstractQuest quest)
    {
        if (quest.ProgressState == AbstractQuest.State.COMPLETED)
        {
            quests.Remove(quest);
            quest.ProgressStateChanged -= OnQuestProgressStateChanged;
        }
    }
    
    #endregion
}