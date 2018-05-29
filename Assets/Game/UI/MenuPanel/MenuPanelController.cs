using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanelController : BaseWindow
{
    #region Editor tweakable fields

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button exitButton;
    
    #endregion
    
    #region Private fields

    private GameController gameController;

    #endregion
    
    #region Unity callbacks
    
    // Use this for initialization
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
        gameObject.SetActive(false);
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
        SceneManager.LoadScene("StartScene");
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }
    
    #endregion
}