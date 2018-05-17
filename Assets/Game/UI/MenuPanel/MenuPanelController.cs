using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuPanelController : BaseWindow
{
    #region Editor tweakable fields

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button exitButton;
    
    #endregion
    
    #region Private fields

    [Inject]
    private GameController gameController;

    #endregion
    
    #region Unity callbacks
    
    // Use this for initialization
    void Start()
    {
        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    #endregion
    
    #region Private methods

    private void OnResumeButtonClicked()
    {
        gameController.IsGamePaused = false;
    }

    private void OnMainMenuButtonClicked()
    {
        Debug.Log("OnMainMenuButtonClicked");
    }

    private void OnExitButtonClicked()
    {
        Debug.Log("OnExitButtonClicked");
    }
    
    #endregion
}