using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Quests;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Characters.Player.Scripts
{
    public class QuestSystem: MonoBehaviour
    {
        #region Private fields

        private readonly List<AbstractQuest> quests = new List<AbstractQuest>();

        #endregion

        #region Public methods

        public void AcceptQuest(AbstractQuest quest)
        {
            quest.ProgressState = AbstractQuest.State.IN_PROGRESS;
            quests.Add(quest);
        }

        public void CompleteQuest(AbstractQuest quest)
        {
            quest.ProgressState = AbstractQuest.State.COMPLETED;
            quests.Remove(quest);
        }

        public void CancelQuest(AbstractQuest quest)
        {
            quest.ProgressState = AbstractQuest.State.AVAILABLE_TO_PICK;
            quests.Remove(quest);
        }

        public List<AbstractQuest> GetQuests()
        {
            return quests;
        }

        public void UpdateKillEnemiesQuestsState(string enemyName)
        {
            foreach (KillEnemiesQuest quest in quests.OfType<KillEnemiesQuest>())
            {
                quest.UpdateEnemyKilledQuantity(enemyName);
            }
        }

        public void UpdatePickUpQuestsState(string itemName)
        {
            foreach (PickUpQuest quest in quests.OfType<PickUpQuest>())
            {
                quest.UpdatePickedItemsQuantity(itemName);
            }
        }

        #endregion
    }
}