﻿using System;
using System.Diagnostics.CodeAnalysis;
using Game.Scripts.Managers;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Scripts.Quests
{
    /// <summary>
    /// In current version:
    /// All data for quests kept as SO. In debug purposes, game designers should manually change quests state.
    /// 
    /// In future version:
    /// Possibly switch to records in DB.
    /// Game designers should manage this data using special interface (todo):
    /// * Title
    /// * Description
    /// * Objectives (kill N enemies, escort, pick N items etc)
    /// * Rewards
    /// * Dependencies (which quest giver belongs, preconditions etc.).
    /// 
    /// After game initializing all quests should be injected in <see cref="QuestGiverSystem"/>
    /// </summary> 
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    public abstract class AbstractQuest : ScriptableObject
    {
        #region Editor tweakable fields

        [NotNull]
        [SerializeField]
        private string title;

        [NotNull]
        [SerializeField]
        private string description;

        /* todo    - for debug purposes added SerializeField attr. Remove
         * @author - Артур
         * @date   - 20.05.2018
         * @time   - 12:50
        */
        [SerializeField]
        private State progressState = State.AVAILABLE_TO_PICK;

        #endregion

        #region Properties

        [NotNull]
        public string Title
        {
            get { return title; }
        }

        [NotNull]
        public string Description
        {
            get { return description; }
        }

        public State ProgressState
        {
            get { return progressState; }
            set
            {
                progressState = value;
                ProgressStateChanged.Invoke(this);
            }
        }

        public event Action<AbstractQuest> ProgressStateChanged = quest => { };

        #endregion
        
        #region Public methods

        /// <summary>
        /// Since <see cref="AbstractQuest"/> is <see cref="ScriptableObject"/>, <see cref="Awake"/> will not be called
        /// on scene load. We need to init quest data manually by calling this method from <see cref="QuestGiverSystem"/>.
        /// </summary>
        public virtual void Init()
        {
        }
        
        #endregion

        #region Private methods

        public abstract string GetObjectiveDescription();

        #endregion

        public enum State
        {
            AVAILABLE_TO_PICK,
            IN_PROGRESS,
            AVAILABLE_TO_COMPLETE,
            COMPLETED
        }
    }
}