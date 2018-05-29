using System;
using System.Diagnostics.CodeAnalysis;
using Game.Characters.Player.Scripts;
using Game.Scripts.Quests;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
public class QuestDescriptionPanelController : BaseWindow
{
    #region Editor tweakable fields

    [NotNull]
    [SerializeField]
    private Text title;

    [NotNull]
    [SerializeField]
    private Text description;

    [NotNull]
    [SerializeField]
    private Text goals;

    [NotNull]
    [SerializeField]
    private Button positiveButton;

    [NotNull]
    [SerializeField]
    private Button negativeButton;

    [NotNull]
    [SerializeField]
    private Button closeButton;

    #endregion

    #region Private fields

    [NotNull]
    private QuestSystem questSystem;

    [NotNull]
    [SerializeField]
    private QuestLogPanelController questLogPanelController;

    [NotNull]
    [SerializeField]
    private QuestGiverListController questGiverListController;

    [NotNull]
    private Text positiveButtonText;

    [NotNull]
    private Text negativeButtonText;

    [CanBeNull]
    private RectTransform rectTransform;

    #endregion
    
    #region Unity callbacks

    private void Start()
    {
        questSystem = FindObjectOfType<QuestSystem>();
        closeButton.onClick.AddListener(() => IsPanelOpened = false);
        rectTransform = GetComponent<RectTransform>();
        
        positiveButtonText = positiveButton.GetComponentInChildren<Text>();
        negativeButtonText = negativeButton.GetComponentInChildren<Text>();
        
        gameObject.SetActive(false);
    }

    #endregion

    #region Public methods

    public void Open([NotNull] AbstractQuest quest)
    {        
        if (questLogPanelController.IsPanelOpened || questGiverListController.IsPanelOpened)
        {
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 298, 300);
        }
        else
        {
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 300);
        }
        
        title.text = quest.Title;
        description.text = quest.Description;
        goals.text = quest.GetObjectiveDescription();

        positiveButton.onClick.RemoveAllListeners();
        negativeButton.onClick.RemoveAllListeners();

        switch (quest.ProgressState)
        {
            case AbstractQuest.State.AVAILABLE_TO_PICK:
                UpdateButtonState(positiveButton, positiveButtonText, "Accept", () =>
                {
                    questSystem.AcceptQuest(quest);
                    IsPanelOpened = false;
                });
                UpdateButtonState(negativeButton, negativeButtonText, "Decline", () => IsPanelOpened = false);
                break;
            case AbstractQuest.State.IN_PROGRESS:
                UpdateButtonState(positiveButton, positiveButtonText, "Complete", isInteractable: false);
                UpdateButtonState(negativeButton, negativeButtonText, "Cancel", () =>
                {
                    questSystem.CancelQuest(quest);
                    questLogPanelController.RemoveQuestTitle(quest);
                    IsPanelOpened = false;
                });
                break;
            case AbstractQuest.State.AVAILABLE_TO_COMPLETE:
                UpdateButtonState(positiveButton, positiveButtonText, "Complete", () =>
                {
                    questSystem.CompleteQuest(quest);
                    questLogPanelController.RemoveQuestTitle(quest);
                    IsPanelOpened = false;
                });
                UpdateButtonState(negativeButton, negativeButtonText, "Cancel", () =>
                {
                    questSystem.CancelQuest(quest);
                    questLogPanelController.RemoveQuestTitle(quest);
                    IsPanelOpened = false;
                });
                break;
            case AbstractQuest.State.COMPLETED:
                throw new ArgumentException("After quest completion QuestDescriptionPanel must not open.");
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        IsPanelOpened = true;
    }

    #endregion

    #region Private methods

    private void UpdateButtonState([NotNull] Button button,
                                   [NotNull] Text buttonText,
                                   [NotNull] string title,
                                   [CanBeNull] UnityAction onClickListener = null,
                                   bool isInteractable = true)
    {
        buttonText.text = title;
        if (onClickListener != null)
        {
            button.onClick.AddListener(onClickListener);
        }
        button.interactable = isInteractable;
    }

    #endregion
}