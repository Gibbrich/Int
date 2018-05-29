using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Game.Scripts.Managers;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Scripts.Quests
{
    /// <summary>
    /// For now code in <see cref="PickUpQuest"/> and <see cref="KillEnemiesQuest"/> is similar.
    /// Think on potential implementation in future and if there won't be any crucial remarks unite in common abstract class.
    /// </summary>
    [CreateAssetMenu(menuName = "RPG/Quests/PickUpQuest")]
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    public class PickUpQuest : AbstractQuest
    {
        #region Editor tweakable fields

        [NotNull]
        [SerializeField]
        private StringIntDictionary targets = StringIntDictionary.New<StringIntDictionary>();
        
        #endregion
        
        #region Private fields

        [NotNull]
        private readonly Dictionary<string, int> pickedTargets = new Dictionary<string, int>();

        #endregion
        
        
        #region Public methods
        
        public void UpdatePickedItemsQuantity(string itemName)
        {
            if (pickedTargets[itemName] < targets.dictionary[itemName])
            {
                pickedTargets[itemName] += 1;
            }
            
            CheckQuestComplete();
        }

        public override void Init()
        {
            if (PlayerPreferencesManager.ShouldInitQuests())
            {
                ProgressState = State.AVAILABLE_TO_PICK;
                foreach (string type in targets.dictionary.Keys)
                {
                    pickedTargets[type] = 0;
                }
            }
        }

        #endregion
        
        #region Private methods
        
        public override string GetObjectiveDescription()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Collect these items:");            
            foreach (string targetName in targets.dictionary.Keys)
            {
                sb.AppendLine(string.Format("{0} - {1}/{2}",
                                            targetName,
                                            pickedTargets[targetName],
                                            targets.dictionary[targetName]));
            }

            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        private void CheckQuestComplete()
        {
            foreach (string enemyName in targets.dictionary.Keys)
            {
                if (targets.dictionary[enemyName] != pickedTargets[enemyName])
                {
                    return;
                }
            }

            ProgressState = State.AVAILABLE_TO_COMPLETE;
        }

        #endregion
    }
}