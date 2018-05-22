using Game.Characters.Scripts;
using Game.Scripts.Quests;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

/// <summary>
/// This class should be used as interface for interactions with any NPC. 
/// </summary>
public class NPCActor : MonoBehaviour, IActor, IInteractable<object, AbstractQuest>, IPointerClickHandler
{
    #region Private fields

    [CanBeNull]
    private QuestGiverSystem questGiverSystem;

    [NotNull]
    [Inject]
    private UIController uiController;

    #endregion

    #region Unity callbacks

    // Use this for initialization
    void Start()
    {
        questGiverSystem = GetComponent<QuestGiverSystem>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    #endregion

    #region Public methods

    /// <summary>
    /// </summary>
    /// <param name="param">Should alway be null</param>
    /// <returns></returns>
    [CanBeNull]
    public AbstractQuest Interact(object param = null)
    {
        return questGiverSystem != null ? questGiverSystem.GetQuest() : null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && questGiverSystem != null)
        {
            AbstractQuest quest = questGiverSystem.GetQuest();
            if (quest != null)
            {
                uiController.OpenQuestDescriptionPanel(quest);
            }
        }
    }

    #endregion
}