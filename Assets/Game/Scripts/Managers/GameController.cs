using UnityEngine;
using Zenject;

public class GameController
{
    #region Properties

    public bool IsGamePaused
    {
        get { return isGamePaused; }
        set
        {
            if (value)
            {
                Time.timeScale = 0;
                uiController.SetMenuPanelOpened(true);
            }
            else
            {
                Time.timeScale = 1;
                uiController.SetMenuPanelOpened(false);
            }
            isGamePaused = value;
        }
    }

    #endregion
    
    #region Private fields

    private bool isGamePaused;

    [Inject]
    private UIController uiController;

    #endregion

}