using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Game.Scripts.Managers;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Scripts.Quests
{
    [CreateAssetMenu(menuName = "RPG/Quests/KillEnemiesQuest")]
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    public class KillEnemiesQuest : AbstractQuest
    {
        #region Editor tweakable fields

        [NotNull]
        [SerializeField]
        private StringIntDictionary targets = StringIntDictionary.New<StringIntDictionary>();

        #endregion

        #region Private fields

        [NotNull]
        private readonly Dictionary<string, int> killedTargets = new Dictionary<string, int>();

        #endregion

        #region Public methods

        public void UpdateEnemyKilledQuantity(string enemyName)
        {
            if (killedTargets[enemyName] < targets.dictionary[enemyName])
            {
                killedTargets[enemyName] += 1;
                CheckQuestComplete();
            }
        }

        public override void Init()
        {
            if (PlayerPreferencesManager.ShouldInitQuests())
            {
                ProgressState = State.AVAILABLE_TO_PICK;
                foreach (string type in targets.dictionary.Keys)
                {
                    killedTargets[type] = 0;
                }
            }
        }

        #endregion

        #region Private methods

        public override string GetObjectiveDescription()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Kill these mosters:");
            foreach (string targetName in targets.dictionary.Keys)
            {
                sb.AppendLine(string.Format("{0} - {1}/{2}",
                                            targetName,
                                            killedTargets[targetName],
                                            targets.dictionary[targetName]));
            }

            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        private void CheckQuestComplete()
        {
            foreach (string enemyName in targets.dictionary.Keys)
            {
                if (targets.dictionary[enemyName] != killedTargets[enemyName])
                {
                    return;
                }
            }

            ProgressState = State.AVAILABLE_TO_COMPLETE;
        }

        #endregion
    }
}