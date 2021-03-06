﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

[SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
public class HealthBarSystem : MonoBehaviour
{
    #region Editor tweakable fields

    [SerializeField]
    private bool shouldHideOnStart = true;
    
    #endregion
    
    #region Private fields

    [NotNull]
    private Slider healthSlider;

    [NotNull]
    private Text healthAmountLabel;

    #endregion

    #region Unity callbacks

    private void Awake()
    {
        healthSlider = GetComponent<Slider>();
        healthAmountLabel = GetComponentInChildren<Text>();

        if (shouldHideOnStart)
        {
            gameObject.SetActive(false);
        }
    }

    #endregion
    
    #region Public methods

    public void OnHealthChanged(float currentHealth, float maxHealth)
    {
        healthSlider.value = currentHealth / maxHealth;
        healthAmountLabel.text = string.Format("{0}/{1}", currentHealth, maxHealth);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public bool IsVisible()
    {
        return gameObject.activeSelf;
    }
    
    #endregion
}