using Game.Scripts.Quests;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public static class PlayerPreferencesManager
    {
        private const string SHOULD_INIT_QUESTS = "SHOULD_INIT_QUESTS";
        
        /// <summary>
        /// For now after loading scene <see cref="QuestGiverSystem"/> re-initialize 
        /// <see cref="KillEnemiesQuest"/> and <see cref="PickUpQuest"/>. It clear quest progress.
        /// This flag is used for saving progress while loading another levels.
        /// todo - temp decision, think on better one
        /// </summary>
        /// <returns></returns>
        public static bool ShouldInitQuests()
        {
            return PlayerPrefs.GetInt(SHOULD_INIT_QUESTS, 0) == 1;
        }

        public static void SetInitQuest(bool shouldInitQuest)
        {
            PlayerPrefs.SetInt(SHOULD_INIT_QUESTS, shouldInitQuest? 1 : 0);
        }
    }
}