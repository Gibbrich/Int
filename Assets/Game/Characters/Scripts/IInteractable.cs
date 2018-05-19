using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable<in TParam, out TResult>
{
    TResult Interact(TParam param);
}