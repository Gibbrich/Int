using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using ModestTree;
using UnityEngine;

public class QuestGiverController : MonoBehaviour
{
    #region Editor tweakable fields
    
    [SerializeField]
    private List<AbstractQuest> quests;
    
    #endregion
    
    #region Public methods

    [CanBeNull]
    public AbstractQuest GetQuest()
    {
        return quests.IsEmpty() ? null : quests[0];
    }
    
    #endregion
}