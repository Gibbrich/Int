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
    
    /* todo    - for debug purposes added SerializeField attr. Remove
     * @author - Артур
     * @date   - 20.05.2018
     * @time   - 12:50
    */    
    [SerializeField]
    private State progressState = State.AVAILABLE_TO_PICK;

    #endregion

    #region Properties

    public string Title
    {
        get { return title; }
    }

    public string Description
    {
        get { return description; }
    }

    public State ProgressState
    {
        get { return progressState; }
        set
        {
            progressState = value; 
            ProgressStateChanged.Invoke(this);
        }
    }

    public event Action<AbstractQuest> ProgressStateChanged = quest => { };

    #endregion

    #region Public methods

    #endregion

    public enum State
    {
        AVAILABLE_TO_PICK,
        IN_PROGRESS,
        AVAILABLE_TO_COMPLETE,
        COMPLETED
    }
}