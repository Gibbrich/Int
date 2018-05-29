using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneCanvasController : MonoBehaviour
{
    #region Editor tweakable fields

    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button exitButton;

    #endregion
    
    #region Unity callbacks

    private void Start()
    {
        playButton.onClick.AddListener(() => SceneManager.LoadScene("Level_01"));
        exitButton.onClick.AddListener(Application.Quit);
    }

    #endregion
}