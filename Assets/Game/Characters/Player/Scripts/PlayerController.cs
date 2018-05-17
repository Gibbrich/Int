using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    #region Editor tweakable fields

    [SerializeField] [Tooltip("World units per second")]
    private float movementSpeed = 30f;

    [Tooltip("Degrees per second")]
    [SerializeField] private float rotationSpeed = 90f;

    #endregion
    
    #region Private fields

    [Inject]
    private GameController gameController;

    [Inject] 
    private UIController uiController;
    
    #endregion

    #region Unity callbacks

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovingButtonsPressed();
        HandleRotationButtonsPressed();
        HandleEscapeButtonClicked();
        HandleQuestButtonClicked();
    }
    
    #endregion
    
    #region Private methods
    
    private void HandleMovingButtonsPressed()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.Rotate(-1 * Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }    
    
    private void HandleRotationButtonsPressed()
    {
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            transform.position -= transform.forward * Time.deltaTime * movementSpeed;
        }
    }
    
    private void HandleEscapeButtonClicked()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (uiController.IsAnyUIPanelOpened())
            {
                uiController.CloseAllUIPanels();
            }
            else
            {
                gameController.IsGamePaused = !gameController.IsGamePaused;
            }
        }
    }

    private void HandleQuestButtonClicked()
    {
        if (!gameController.IsGamePaused && Input.GetKeyDown(KeyCode.Q))
        {
            uiController.RevertQuestLogPanelOpenedState();

            if (uiController.IsQuestDescriptionPanelOpened())
            {
                uiController.CloseQuestQuestDescriptionPanel();
            }
        }
    }
    
    #endregion
}