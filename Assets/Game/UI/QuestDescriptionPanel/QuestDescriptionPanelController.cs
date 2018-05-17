using UnityEngine;
using UnityEngine.UI;

public class QuestDescriptionPanelController : BaseWindow
{
    #region Editor tweakable fields

    [SerializeField] private Text title;
    [SerializeField] private Text description;
    
    #endregion
    
    #region Public methods
    
    public void Open(Quest quest)
    {
        title.text = quest.Title;
        description.text = quest.Description;
        IsPanelOpened = true;
    }
    
    #endregion
}