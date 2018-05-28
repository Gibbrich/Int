using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Game.Scripts;
using Game.Scripts.Quests;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
public class QuestGiverListController : MonoBehaviour
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
    [Tooltip("Host for quest tittle items")]
    private GameObject content;

    [NotNull]
    [SerializeField]
    private Button closeButton;

    [SerializeField]
    private float distanceBetweenQUestTitles = 40f;

    #endregion

    #region Private fields

    [NotNull]
    private Pool<QuestTitleController> pool;

    #endregion

    #region Unity callbacks

    private void Awake()
    {
        closeButton.onClick.AddListener(Hide);
    }

    #endregion

    #region Public methods

    public void Show(List<AbstractQuest> quests)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            Vector2 offset = GetPosition(i);
            var position = new QuestTitleController.Position(OFFSET_LEFT, OFFSET_RIGHT, offset.x, offset.y);
            pool.GetNewObject().Init(quests[i], position);
        }
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        pool
            .getObjects()
            .ForEach(controller => pool.Release(controller));
        gameObject.SetActive(false);
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

    /* todo    - change name, return type
     * @author - Артур
     * @date   - 19.05.2018
     * @time   - 16:26
    */
    private Vector2 GetPosition(int id)
    {
        return new Vector2(-5, -35) + id * distanceBetweenQUestTitles * new Vector2(-1, -1);
    }

    #endregion
}