using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class QuestDescriptionPanelController : BaseWindow
{
    #region Editor tweakable fields

    [SerializeField]
    private Text title;

    [SerializeField]
    private Text description;

    [SerializeField]
    private Button acceptButton;

    [SerializeField]
    private Button closeButton;

    #endregion

    #region Private methods

    [Inject]
    private Player player;

    private AbstractQuest quest;

    #endregion

    #region Unity callbacks

    private void Start()
    {
        acceptButton.onClick.AddListener(() =>
        {
            player.AcceptQuest(quest);
            IsPanelOpened = false;
        });
        closeButton.onClick.AddListener(() => IsPanelOpened = false);
    }

    #endregion

    #region Public methods

    public void Open(AbstractQuest quest)
    {
        this.quest = quest;
        title.text = quest.Title;
        description.text = quest.Description;
        IsPanelOpened = true;
    }

    #endregion
}