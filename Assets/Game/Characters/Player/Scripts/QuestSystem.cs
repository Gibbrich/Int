using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Quests;
using JetBrains.Annotations;
using ModestTree;

namespace Game.Characters.Player.Scripts
{
    public class QuestSystem
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
            quests
                .OfType<KillEnemiesQuest>()
                .ForEach(quest => quest.UpdateEnemyKilledQuantity(enemyName));
        }

        public void UpdatePickUpQuestsState(string itemName)
        {
            quests
                .OfType<PickUpQuest>()
                .ForEach(quest => quest.UpdatePickedItemsQuantity(itemName));
        }

        #endregion
    }
}