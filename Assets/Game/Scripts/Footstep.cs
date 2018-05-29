using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    #region Editor tweakable fields
    
    [EventRef]
    [SerializeField]
    private string inputSound;

    [SerializeField]
    private float walkingSpeed = 0.5f;
    
    #endregion
    
    #region Private fields
    
    private float lastPlayTime;    
    
    #endregion

    #region Public methods
    
    public void PlayFootsteps()
    {
        lastPlayTime = Time.time;
        RuntimeManager.PlayOneShot(inputSound);
    }

    public bool IsTimeToPlay()
    {
        return Time.time - lastPlayTime >= walkingSpeed;
    }    
    
    #endregion
}