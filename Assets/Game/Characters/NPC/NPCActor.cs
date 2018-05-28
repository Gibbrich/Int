using System.Diagnostics.CodeAnalysis;
using Game.Characters.Player.Scripts;
using Game.Characters.Scripts;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

/// <summary>
/// This class should be used as interface for interactions with any NPC. 
/// </summary>
[SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
public class NPCActor : MonoBehaviour, IActor, IPointerClickHandler
{
    #region Private fields

    [CanBeNull]
    private QuestGiverSystem questGiverSystem;

    [NotNull]
    [Inject]
    private PlayerActor player;

    #endregion

    #region Unity callbacks

    // Use this for initialization
    void Start()
    {
        questGiverSystem = GetComponent<QuestGiverSystem>();
    }

    #endregion

    #region Public methods

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && questGiverSystem != null)
        {
            player.OnInteraction(questGiverSystem.GetQuests());
        }
    }

    #endregion
}