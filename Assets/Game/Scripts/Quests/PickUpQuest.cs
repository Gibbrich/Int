using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Quests
{
    public class PickUpQuest : AbstractQuest
    {
        #region Editor tweakable fields

        [SerializeField]
        private PickUpObjective objective;
        
        #endregion
        
        protected override string GetObjectiveDescription()
        {
            throw new System.NotImplementedException();
        }
    }

    /* todo    - current implementation is 2 separate lists, containing enemy name and enemy quantity.
     * Refactor for using dictionary
     * @author - Артур
     * @date   - 20.05.2018
     * @time   - 14:35
    */
    [Serializable]
    public class PickUpObjective
    {
        [SerializeField]
        private List<string> targetNames;

        [SerializeField]
        private List<int> targetQuantity;
    }
}