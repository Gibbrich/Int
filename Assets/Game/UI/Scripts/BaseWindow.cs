﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWindow : MonoBehaviour
{
    #region Properties

    public bool IsPanelOpened
    {
        get { return gameObject.activeSelf; }
        set { gameObject.SetActive(value); }
    }

    #endregion
}