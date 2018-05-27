using System.Diagnostics.CodeAnalysis;
using Game.Characters.Scripts;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

[SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
public class PlayerInputSystem : MonoBehaviour
{
    #region Editor tweakable fields

    [SerializeField]
    [Tooltip("World units per second")]
    private float movementSpeed = 5f;

    [Tooltip("Degrees per second")]
    [SerializeField]
    private float rotationSpeed = 90f;

    #endregion

    #region Private fields

    [NotNull]
    [Inject]
    private GameController gameController;

    [NotNull]
    [Inject]
    private UIController uiController;

    [NotNull]
    private WeaponSystem weaponSystem;

    [NotNull]
    private Animator animator;

    private new Rigidbody rigidbody;

    #endregion

    #region Unity callbacks

    private void Start()
    {
        weaponSystem = GetComponent<WeaponSystem>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleEscapeButtonClicked();
        HandleQuestButtonClicked();
        HandlePlayerRightMouseButtonClicked();
    }

    private void FixedUpdate()
    {
        HandleMovingButtonsPressed();
    }

    private void OnAnimatorMove()
    {
        // we implement this function to override the default root motion.
        // this allows us to modify the positional speed before it's applied.
        if (Time.deltaTime > 0)
        {
            Vector3 v = animator.deltaPosition / Time.deltaTime;

            // we preserve the existing y part of the current velocity.
            v.y = rigidbody.velocity.y;
            rigidbody.velocity = v;
        }
    }

    #endregion

    #region Public methods

    public void OnInteraction()
    {
        animator.SetTrigger("OnInteraction");
    }

    #endregion

    #region Private methods

    private void HandlePlayerRightMouseButtonClicked()
    {
        if (Input.GetMouseButtonDown(1))
        {
            weaponSystem.Attack();
        }
    }

    private void HandleMovingButtonsPressed()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 movementDirection = GetMovementDirection();
        Vector3 movement = (v * movementDirection + h * Camera.main.transform.right).normalized;
        movement = transform.InverseTransformDirection(movement);

        animator.SetFloat("Forward", movement.z, 0.1f, Time.deltaTime);
        animator.SetFloat("Turn", h, 0.1f, Time.deltaTime);

        if (h > 0)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
        else if (h < 0)
        {
            transform.Rotate(-1 * Vector3.up * rotationSpeed * Time.deltaTime);
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
            else if (uiController.IsTargetHealthBarVisible())
            {
                uiController.HideTargetHealthBar();
                weaponSystem.SetTarget(null);
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

    private Vector3 GetMovementDirection()
    {
        return Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
    }

    #endregion
}