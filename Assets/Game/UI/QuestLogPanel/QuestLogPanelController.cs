using System;
using System.Diagnostics.CodeAnalysis;
using Game.Characters.Player.Scripts;
using Game.Scripts;
using Game.Scripts.Quests;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

[SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
public class QuestLogPanelController : BaseWindow
{
    /* todo    - hardcoded values. Change to parametrized
     * @author - Артур
     * @date   - 19.05.2018
     * @time   - 16:13
    */
    private const float OFFSET_LEFT = 10;
    private const float OFFSET_RIGHT = -10;

    #region Editor tweakable fields
    
    [NotNull]
    [SerializeField]
    [Tooltip("List item")]
    private QuestTitleController listItemPrefab;

    [Tooltip("Host for quest tittle items")]
    [SerializeField]
    private GameObject content;

    [SerializeField]
    private float distanceBetweenQUestTitles = 40f;
    
    #endregion

    #region Properties

    public override bool IsPanelOpened
    {
        get { return gameObject.activeSelf; }
        set
        {
            gameObject.SetActive(value);
            
            if (value)
            {
                ShowQuests();
            }
            else
            {
                HideQuests();
            }
        }
    }

    #endregion

    #region Private fields

    [NotNull]
    private QuestSystem questSystem;

    [NotNull]
    private Pool<QuestTitleController> pool;

    [NotNull]
    private Button closeButton;

    #endregion

    #region Unity callbacks

    private void Start()
    {
        questSystem = FindObjectOfType<QuestSystem>();
        closeButton = GetComponentInChildren<Button>();
        closeButton.onClick.AddListener(() => IsPanelOpened = false);
        
        Func<QuestTitleController> create = () =>
        {
            QuestTitleController questTitleController = Instantiate(listItemPrefab);
            questTitleController.gameObject.transform.SetParent(content.transform);
            return questTitleController;
        };

        pool = new Pool<QuestTitleController>(
            10,
            create,
            controller => Destroy(controller.gameObject),
            WakeUp,
            SetToSleep);
        
        gameObject.SetActive(false);
    }

    #endregion

    #region Public methods

    public void RemoveQuestTitle([NotNull] AbstractQuest quest)
    {
        QuestTitleController resultController = pool.getObjects().Find(controller => controller.Quest == quest);
        if (resultController != null)
        {
            pool.Release(resultController);
        }
    }

    #endregion

    #region Private methods

    private void SetToSleep(QuestTitleController controller)
    {
        controller.gameObject.SetActive(false);
    }

    private void WakeUp(QuestTitleController controller)
    {
        controller.gameObject.SetActive(true);
    }

    /* todo    - change name, return type and make parametrized
     * @author - Артур
     * @date   - 19.05.2018
     * @time   - 16:26
    */
    private Vector2 GetPosition(int id)
    {
        return new Vector2(-5, -35) + id * distanceBetweenQUestTitles * new Vector2(-1, -1);
    }

    private void ShowQuests()
    {       
        for (int i = 0; i < questSystem.GetQuests().Count; i++)
        {
            Vector2 offset = GetPosition(i);
            var position = new QuestTitleController.Position(OFFSET_LEFT, OFFSET_RIGHT, offset.x, offset.y);
            pool.GetNewObject().Init(questSystem.GetQuests()[i], position);
        }
    }

    private void HideQuests()
    {
        pool
            .getObjects()
            .ForEach(controller => pool.Release(controller));
    }

    #endregion
}