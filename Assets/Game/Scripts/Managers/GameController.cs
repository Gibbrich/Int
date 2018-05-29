using Game.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private const string Level_01 = "Level_01";
    private const string Level_02 = "Level_02";
    
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

    private UIController uiController;

    #endregion

    #region Unity callbacks

    private void Awake()
    {
        uiController = FindObjectOfType<UIController>();
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    #endregion
    
    #region Private methods

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals(Level_01))
        {
            PlayerPreferencesManager.SetInitQuest(true);
        }
        else if (scene.name.Equals(Level_02))
        {
            PlayerPreferencesManager.SetInitQuest(false);
        }
    }
    
    #endregion
}