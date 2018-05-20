using System.Collections.Generic;
using Game.Scripts.Quests;
using JetBrains.Annotations;

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

        #endregion
    }
}