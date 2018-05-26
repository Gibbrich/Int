using System;
using System.Diagnostics.CodeAnalysis;
using Game.Characters.Player.Scripts;
using Game.Scripts;
using Game.Scripts.Quests;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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

    [Tooltip("Host for quest tittle items")]
    [SerializeField]
    private GameObject content;

    #endregion

    #region Properties

    public override bool IsPanelOpened
    {
        get { return gameObject.activeSelf; }
        set
        {
            if (value)
            {
                ShowQuests();
            }
            else
            {
                HideQuests();
            }

            gameObject.SetActive(value);
        }
    }

    #endregion

    #region Private fields

    [NotNull]
    [Inject]
    private QuestSystem questSystem;

    [NotNull]
    private Pool<QuestTitleController> pool;

    [NotNull]
    private Button closeButton;

    #endregion

    #region Unity callbacks

    private void Start()
    {
        closeButton = GetComponentInChildren<Button>();
        closeButton.onClick.AddListener(() => IsPanelOpened = false);
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

    [Inject]
    private void Init(QuestTitleController.Factory factory)
    {
        Func<QuestTitleController> create = () =>
        {
            QuestTitleController questTitleController = factory.Create();
            questTitleController.gameObject.transform.SetParent(content.transform);
            return questTitleController;
        };

        pool = new Pool<QuestTitleController>(
            10,
            create,
            controller => Destroy(controller.gameObject),
            WakeUp,
            SetToSleep);
    }

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
    private Vector2 GetPosition()
    {
        return new Vector2(-5, -35);
    }

    private void ShowQuests()
    {
        questSystem.GetQuests().ForEach(quest =>
        {
            Vector2 offset = GetPosition();
            var position = new QuestTitleController.Position(OFFSET_LEFT, OFFSET_RIGHT, offset.x, offset.y);
            pool.GetNewObject().Init(quest, position);
        });
    }

    private void HideQuests()
    {
        pool
            .getObjects()
            .ForEach(controller => pool.Release(controller));
    }

    #endregion
}