using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private const int MOUSE_LEFT_BUTTON = 0;
    private const int INTERACTABLE_LAYER = 8;
    private const float MAX_RAYCAST_DEPTH = 100f;
    
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

    // Update is called once per frame
    void Update()
    {
        HandleMovingButtonsPressed();
        HandleRotationButtonsPressed();
        HandleEscapeButtonClicked();
        HandleQuestButtonClicked();
        HandleMouseLeftButtonClick();
    }

    #endregion
    
    #region Private methods
    
    private void HandleMouseLeftButtonClick()
    {
        if (Input.GetMouseButtonDown(MOUSE_LEFT_BUTTON))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask interactableLayerMask = 1 << INTERACTABLE_LAYER;
            RaycastHit hitInfo;
            bool wasHit = Physics.Raycast(ray, out hitInfo, MAX_RAYCAST_DEPTH, interactableLayerMask);
            if (wasHit)
            {
                NPC npc = hitInfo.collider.GetComponent<NPC>();
                if (npc)
                {
                    AbstractQuest abstractQuest = npc.Interact();
                    if (abstractQuest != null)
                    {
                        uiController.OpenQuestDescriptionPanel(abstractQuest);
                    }
                }
            }
        }
    }
    
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