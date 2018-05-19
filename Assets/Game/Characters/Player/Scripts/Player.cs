using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    #region Properties
    
    public List<AbstractQuest> Quests
    {
        get { return quests; }
    }    
    
    #endregion
    
    #region Private fields
    
    private readonly List<AbstractQuest> quests = new List<AbstractQuest>();

    #endregion
    
    #region Public methods

    public void AcceptQuest(AbstractQuest quest)
    {
        Quests.Add(quest);
    }
    
    #endregion
}