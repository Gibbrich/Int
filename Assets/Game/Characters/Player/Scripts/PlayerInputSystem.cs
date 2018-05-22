using System.Diagnostics.CodeAnalysis;
using Game.Characters.Player.Scripts;
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
    private float movementSpeed = 5f;

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
    private Animator animator;
    
    #endregion

    #region Unity callbacks

    private void Start()
    {
        weaponSystem = GetComponent<WeaponSystem>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleRotationButtonsPressed();
        HandleMovingButtonsPressed();
        HandleEscapeButtonClicked();
        HandleQuestButtonClicked();
    }

    #endregion
    
    #region Private methods
    
    private void HandleRotationButtonsPressed()
    {
        float rotationAmplitude = Input.GetAxis("Horizontal");
        animator.SetFloat("Turn", rotationAmplitude);
        
        if (rotationAmplitude > 0)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
        else if (rotationAmplitude < 0)
        {
            transform.Rotate(-1 * Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }    
    
    private void HandleMovingButtonsPressed()
    {
        /* todo    - for now, there is no moving back animation in animation controller,
         * so moving back looks a little bit wierd. Do not step back! :D
         * @author - Артур
         * @date   - 21.05.2018
         * @time   - 21:16
        */
        
        float movementAmplitude = Input.GetAxis("Vertical");
        animator.SetFloat("Forward", movementAmplitude);
        
        if (movementAmplitude > 0)
        {
            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        }
        else if (movementAmplitude < 0)
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