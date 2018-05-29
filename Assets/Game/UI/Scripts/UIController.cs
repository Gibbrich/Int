using System.Collections.Generic;
using System.Linq;
using Game.Characters.Scripts;
using Game.Scripts.Quests;
using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// This class should be used for all interactions with UI.
/// </summary>
public class UIController : MonoBehaviour
{
    #region Private fields

    [SerializeField]
    private MenuPanelController menuPanelController;

    [SerializeField]
    private QuestLogPanelController questLogPanelController;

    [SerializeField]
    private QuestDescriptionPanelController questDescriptionPanelController;

    [SerializeField]
    private HealthBarSystem playerHealthBarSystem;

    [SerializeField]
    private HealthBarSystem targetHealthBarSystem;

    [SerializeField]
    private QuestGiverListController questGiverListController;

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
        return questLogPanelController.IsPanelOpened ||
               questDescriptionPanelController.IsPanelOpened ||
               questGiverListController.IsPanelOpened;
    }

    public void CloseAllUIPanels()
    {
        questLogPanelController.IsPanelOpened = false;
        questDescriptionPanelController.IsPanelOpened = false;
        questGiverListController.Hide();
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

    public bool IsTargetHealthBarVisible()
    {
        return targetHealthBarSystem.IsVisible();
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

    public void ShowQuestGiverList(List<AbstractQuest> quests)
    {
        questGiverListController.Show(quests);
    }

    public void HideQuestGiverList()
    {
        questGiverListController.Hide();
    }

    public void OpenQuestGiverWindows(List<AbstractQuest> quests)
    {
        if (quests.Count == 0)
        {
        }
        else if (quests.Count == 1)
        {
            if (!questDescriptionPanelController.IsPanelOpened)
            {
                OpenQuestDescriptionPanel(quests.First());
            }
        }
        else
        {
            if (!questGiverListController.IsPanelOpened)
            {
                ShowQuestGiverList(quests);
            }
        }
    }

    #endregion
}