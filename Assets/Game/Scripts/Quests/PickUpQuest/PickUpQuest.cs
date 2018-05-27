using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using JetBrains.Annotations;
using ModestTree;
using UnityEngine;

namespace Game.Scripts.Quests
{
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
        private Dictionary<string, int> pickedTargets;

        #endregion
        
        
        #region Public methods
        
        public void UpdatePickedItemsQuantity(string itemName)
        {
            if (pickedTargets[itemName] < targets.dictionary[itemName])
            {
                pickedTargets[itemName] += 1;
            }
        }

        public override void Init()
        {
            pickedTargets = new Dictionary<string, int>();
            targets.dictionary.Keys.ForEach(type => pickedTargets[type] = 0);
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
        
        #endregion
    }
}