using System.Collections;
using System.Collections.Generic;
using Game.Characters.Player.Scripts;
using Game.Characters.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class QuestItem : MonoBehaviour, IActor, IPointerClickHandler
{
    #region Editor tweakable fields

    [SerializeField]
    private string itemName;
    
    #endregion
    
    #region Properties
    
    public string ItemName
    {
        get { return itemName; }
    }    
    
    #endregion
    
    #region Private fields

    [Inject]
    private PlayerActor player;

    #endregion
    
    #region Public methods
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            player.OnInteraction(this);
            gameObject.SetActive(false);
        }
    }    
    
    #endregion

}