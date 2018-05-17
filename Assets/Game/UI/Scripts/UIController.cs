using Zenject;

/// <summary>
/// This class should be used for all interactions with UI.
/// </summary>
public class UIController
{
    #region Private fields

    [Inject]
    private MenuPanelController menuPanelController;

    [Inject] 
    private QuestLogPanelController questLogPanelController;

    [Inject]
    private QuestDescriptionPanelController questDescriptionPanelController;

    #endregion
    
    #region Public methods

    public void SetMenuPanelOpened(bool isOpened)
    {
        menuPanelController.IsPanelOpened = isOpened;
    }

    public void RevertQuestLogPanelOpenedState()
    {
        questLogPanelController.IsPanelOpened = !questLogPanelController.IsPanelOpened;
    }

    /// <summary>
    /// Checks whether any UI panel, except menu panel opened.
    /// </summary>
    /// <returns></returns>
    public bool IsAnyUIPanelOpened()
    {
        return questLogPanelController.IsPanelOpened && questDescriptionPanelController.IsPanelOpened;
    }

    public void CloseAllUIPanels()
    {
        questLogPanelController.IsPanelOpened = false;
        questDescriptionPanelController.IsPanelOpened = false;
    }

    public void OpenQuestDescriptionPanel(Quest quest)
    {
        questDescriptionPanelController.Open(quest);
    }

    public bool IsQuestDescriptionPanelOpened()
    {
        return questDescriptionPanelController.IsPanelOpened;
    }

    public void CloseQuestQuestDescriptionPanel()
    {
        questDescriptionPanelController.IsPanelOpened = false;
    }

    #endregion
}