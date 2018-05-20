using System.Diagnostics.CodeAnalysis;
using Game.Characters.Scripts;
using Game.Scripts.Quests;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

[SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
public class PlayerInputSystem : MonoBehaviour
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

    [NotNull]
    [Inject]
    private GameController gameController;

    [NotNull]
    [Inject] 
    private UIController uiController;

    private WeaponSystem weaponSystem;
    
    #endregion

    #region Unity callbacks

    private void Start()
    {
        weaponSystem = GetComponent<WeaponSystem>();
    }

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
        // only if mouse not over ui
        if (Input.GetMouseButtonDown(MOUSE_LEFT_BUTTON) && !EventSystem.current.IsPointerOverGameObject()) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask interactableLayerMask = 1 << INTERACTABLE_LAYER;
            RaycastHit hitInfo;
            bool wasHit = Physics.Raycast(ray, out hitInfo, MAX_RAYCAST_DEPTH, interactableLayerMask);
            if (wasHit)
            {
                NPCActor npcActor = hitInfo.collider.GetComponent<NPCActor>();
                if (npcActor)
                {
                    AbstractQuest abstractQuest = npcActor.Interact();
                    if (abstractQuest != null)
                    {
                        uiController.OpenQuestDescriptionPanel(abstractQuest);
                    }
                    return;
                }

                IDamageable target = hitInfo.collider.GetComponent<IDamageable>();
                if (target != null)
                {
                    weaponSystem.Attack(target);
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