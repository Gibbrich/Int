using System.Collections;
using System.Collections.Generic;
using Game.Characters.Player.Scripts;
using Game.Characters.Scripts;
using UnityEngine;

public class WeaponPickUpActor : MonoBehaviour, IActor, IInteractable<object, WeaponConfig>
{
    #region Editor tweakable fields

    [SerializeField]
    private WeaponConfig weaponConfig;
    
    #endregion
    
    #region Unity callbacks

    private void OnTriggerEnter(Collider other)
    {
        PlayerActor player = other.GetComponent<PlayerActor>();
        if (player != null)
        {
            player.OnInteraction(this);
        }
    }

    #endregion
    
    #region Public methods
    
    public WeaponConfig Interact(object param = null)
    {
        return weaponConfig;
    }    
    
    #endregion
}