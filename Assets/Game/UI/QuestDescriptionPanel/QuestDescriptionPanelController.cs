using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class QuestDescriptionPanelController : BaseWindow
{
    #region Editor tweakable fields

    [NotNull]
    [SerializeField]
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    private Text title;

    [NotNull]
    [SerializeField]
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    private Text description;

    [NotNull]
    [SerializeField]
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    private Button positiveButton;

    [NotNull]
    [SerializeField]
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    private Button negativeButton;

    #endregion

    #region Private methods

    [NotNull]
    [Inject]
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    private Player player;

    [NotNull]
    [Inject]
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    private QuestLogPanelController questLogPanelController;

    [NotNull]
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    private Text positiveButtonText;

    [NotNull]
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    private Text negativeButtonText;

    #endregion

    #region Public methods

    public void Open([NotNull] AbstractQuest quest)
    {
        title.text = quest.Title;
        description.text = quest.Description;

        positiveButton.onClick.RemoveAllListeners();
        negativeButton.onClick.RemoveAllListeners();

        switch (quest.ProgressState)
        {
            case AbstractQuest.State.AVAILABLE_TO_PICK:
                positiveButtonText.text = "Accept";
                positiveButton.interactable = true;
                positiveButton.onClick.AddListener(() =>
                {
                    player.AcceptQuest(quest);
                    IsPanelOpened = false;
                });

                negativeButtonText.text = "Decline";
                negativeButton.onClick.AddListener(() => IsPanelOpened = false);
                break;
            case AbstractQuest.State.IN_PROGRESS:
                positiveButtonText.text = "Complete";
                positiveButton.interactable = false;

                negativeButtonText.text = "Cancel";
                negativeButton.onClick.AddListener(() =>
                {
                    player.CancelQuest(quest);
                    questLogPanelController.RemoveQuestTitle(quest);
                    IsPanelOpened = false;
                });
                break;
            case AbstractQuest.State.AVAILABLE_TO_COMPLETE:
                positiveButtonText.text = "Complete";
                positiveButton.interactable = true;
                positiveButton.onClick.AddListener(() =>
                {
                    player.CompleteQuest(quest);
                    questLogPanelController.RemoveQuestTitle(quest);
                    IsPanelOpened = false;
                });

                negativeButtonText.text = "Cancel";
                negativeButton.onClick.AddListener(() =>
                {
                    player.CancelQuest(quest);
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

    [Inject]
    private void Init()
    {
        positiveButtonText = positiveButton.GetComponentInChildren<Text>();
        negativeButtonText = negativeButton.GetComponentInChildren<Text>();
    }

    #endregion
}