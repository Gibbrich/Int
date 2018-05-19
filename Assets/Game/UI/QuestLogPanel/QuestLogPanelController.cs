using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

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
            ShowQuests();
            gameObject.SetActive(value);
        }
    }

    #endregion

    #region Private fields

    [Inject]
    private Player player;

    private Pool<QuestTitleController> pool;

    #endregion

    #region Private methods

    [Inject]
    private void Init(QuestTitleController.Factory factory)
    {
        Func<QuestTitleController> create = () =>
        {
            QuestTitleController questTitleController = factory.Create();

            /* todo    - setup parent for instatiated QuestTitleController in Installer
           * @author - Артур
           * @date   - 19.05.2018
           * @time   - 12:32
          */
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
        player.Quests.ForEach(quest =>
        {
            Vector2 offset = GetPosition();
            var position = new QuestTitleController.Position(OFFSET_LEFT, OFFSET_RIGHT, offset.x, offset.y);
            pool.GetNewObject().Init(quest, position);
        });
    }

    #endregion
}