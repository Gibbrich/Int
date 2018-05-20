using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

// ReSharper disable NotNullMemberIsNotInitialized

namespace Game.Scripts.Quests
{
    [CreateAssetMenu(menuName = "RPG/Quests/KillEnemiesQuest")]
    public class KillEnemiesQuest : AbstractQuest
    {
        #region Editor tweakable fields

        [NotNull]
        [SerializeField]
        private KillEnemiesObjective objective;

        #endregion

        protected override string GetObjectiveDescription()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Kill these mosters:");
            for (var i = 0; i < objective.EnemyNames.Count; i++)
            {
                sb.AppendLine(string.Format("{0} - {1}/{2} units",
                                            objective.EnemyNames[i],
                                            objective.EnemyKilledQuantity[i],
                                            objective.EnemyQuantity[i]));
            }

            return sb.ToString();
        }
    }

    /* todo    - current implementation is 2 separate lists, containing enemy name and enemy quantity.
     * Refactor for using dictionary
     * @author - Артур
     * @date   - 20.05.2018
     * @time   - 14:35
    */
    /// <summary>
    /// For now, enemyNames count must be equal enemyQuantity count
    /// </summary>
    [Serializable]
    public class KillEnemiesObjective
    {
        #region Private fields

        [NotNull]
        [SerializeField]
        private List<string> enemyNames;

        [NotNull]
        [SerializeField]
        private List<int> enemyQuantity;

        [NotNull]
        private readonly List<int> enemyKilledQuantity = new List<int>();

        #endregion

        #region Properties

        [NotNull]
        public List<string> EnemyNames
        {
            get { return enemyNames; }
        }

        [NotNull]
        public List<int> EnemyQuantity
        {
            get { return enemyQuantity; }
        }

        [NotNull]
        public List<int> EnemyKilledQuantity
        {
            get { return enemyKilledQuantity; }
        }

        #endregion
    }
}