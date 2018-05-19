using System;
using UnityEngine;

/// <summary>
/// In current version all data for quests should be kept in DB.
/// Game designers should manage this data using special interface (todo):
/// * Title
/// * Description
/// * Objectives (kill N enemies, escort, pick N items etc)
/// * Rewards
/// * Dependencies (which quest giver belongs, preconditions etc.).
/// 
/// After game initializing all quests should be 
/// 
/// Possibly switch to SO.
/// </summary>
[Serializable]
public class AbstractQuest
{
    #region Editor tweakable fields

    [SerializeField]
    private string title;

    [SerializeField]
    private string description;

    #endregion
    
    #region Properties

    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    #endregion
}