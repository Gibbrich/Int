using Game.Characters.Scripts;
using Game.Scripts.Quests;
using JetBrains.Annotations;
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

    [Inject(Id = Installer.Identifiers.PLAYER_HEALTH_BAR)]
    private HealthBarSystem playerHealthBarSystem;

    [Inject(Id = Installer.Identifiers.TARGET_HEALTH_BAR)]
    private HealthBarSystem targetHealthBarSystem;

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
        return questLogPanelController.IsPanelOpened || questDescriptionPanelController.IsPanelOpened;
    }

    public void CloseAllUIPanels()
    {
        questLogPanelController.IsPanelOpened = false;
        questDescriptionPanelController.IsPanelOpened = false;
    }

    public void OpenQuestDescriptionPanel([NotNull] AbstractQuest abstractQuest)
    {
        questDescriptionPanelController.Open(abstractQuest);
    }

    public bool IsQuestDescriptionPanelOpened()
    {
        return questDescriptionPanelController.IsPanelOpened;
    }

    public void CloseQuestQuestDescriptionPanel()
    {
        questDescriptionPanelController.IsPanelOpened = false;
    }

    public HealthBarSystem GetTargetHealthBarSystem()
    {
        return targetHealthBarSystem;
    }

    public void ShowTargetHealthBar()
    {
        targetHealthBarSystem.Show();
    }

    public void HideTargetHealthBar()
    {
        targetHealthBarSystem.Hide();
    }
    
    public void UpdateTargetHealthBarValues(float currentHealth, float maxHealth)
    {
        targetHealthBarSystem.OnHealthChanged(currentHealth, maxHealth);
    }

    public void UpdatePlayerHealthBarValues(float currentHealth, float maxHealth)
    {
        playerHealthBarSystem.OnHealthChanged(currentHealth, maxHealth);
    }

    #endregion
}