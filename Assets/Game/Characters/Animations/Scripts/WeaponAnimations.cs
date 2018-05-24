using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Animations.Scripts
{
    [CreateAssetMenu(fileName = "AnimationsInstaller", menuName = "Installers/AnimationsInstaller")]
    public class WeaponAnimations : ScriptableObject
    {
        #region Editor tweakable fields

        [SerializeField]
        private List<AnimationClip> attackAnimations;
    
        [SerializeField]
        private List<AnimationClip> takeHitAnimations;

        [SerializeField]
        private List<AnimationClip> interactAnimations;

        [SerializeField]
        private List<AnimationClip> deathAnimations;

        #endregion
    
        #region Properties
    
        public List<AnimationClip> AttackAnimations
        {
            get { return attackAnimations; }
        }

        public List<AnimationClip> TakeHitAnimations
        {
            get { return takeHitAnimations; }
        }

        public List<AnimationClip> InteractAnimations
        {
            get { return interactAnimations; }
        }

        public List<AnimationClip> DeathAnimations
        {
            get { return deathAnimations; }
        }    
    
        #endregion
    }
}