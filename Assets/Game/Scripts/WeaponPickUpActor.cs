using System.Collections;
using System.Collections.Generic;
using Game.Characters.Player.Scripts;
using Game.Characters.Scripts;
using UnityEngine;

public class WeaponPickUpActor : MonoBehaviour, IActor, IInteractable<object, WeaponConfig>
{
    #region Editor tweakable fields

    [SerializeField]
    [Tooltip("How fast weapon rotates, degree/sec")]
    private float rotationSpeed = 20;

    [SerializeField]
    [Tooltip("Rotating weapon previev")]
    private GameObject weaponObject;

    [SerializeField]
    private WeaponConfig weaponConfig;

    #endregion

    #region Unity callbacks

    private void Update()
    {
        weaponObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }

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