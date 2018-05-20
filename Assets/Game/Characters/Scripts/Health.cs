using System;
using UnityEngine;

namespace Game.Characters.Scripts
{
    [Serializable]
    public class Health
    {
        #region Editor tweakable fields
        
        [SerializeField]
        private float currentValue;
        
        [SerializeField]
        private float maxValue;
        
        #endregion
        
        #region Properties
        
        public float CurrentValue
        {
            get { return currentValue; }
            set { currentValue = value; }
        }

        public float MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }
        
        #endregion

    }
}