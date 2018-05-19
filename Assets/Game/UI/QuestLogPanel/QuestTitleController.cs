using UnityEngine;
using UnityEngine.UI;
using Zenject;

/// <summary>
/// Used for managing quest item in <see cref="QuestLogPanelController"/>
/// </summary>
public class QuestTitleController : MonoBehaviour
{
    #region Properties

    public AbstractQuest Quest { get; set; }

    #endregion

    #region Private methods

    [Inject]
    private UIController uiController;

    private Button button;
    private Text title;
    private RectTransform rectTransform;

    #endregion

    #region Unity callbacks

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => uiController.OpenQuestDescriptionPanel(Quest));

        title = GetComponentInChildren<Text>();
        rectTransform = GetComponent<RectTransform>();
    }

    #endregion

    #region Public methods

    public void Init(AbstractQuest quest, Position position)
    {
        Quest = quest;
        title.text = Quest.Title;
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.offsetMax = new Vector2(position.OffsetRight, position.OffsetTop);
        rectTransform.offsetMin = new Vector2(position.OffsetLeft, position.OffsetBottom);
    }

    #endregion

    /// <summary>
    /// Used for instantiating <see cref="QuestTitleController"/> prefab in runtime,
    /// as it has fields, marked with <see cref="InjectAttribute"/>
    /// </summary>
    public class Factory : Factory<QuestTitleController>
    {
    }

    public struct Position
    {
        private readonly float offsetLeft;
        private readonly float offsetRight;
        private readonly float offsetTop;
        private readonly float offsetBottom;

        public Position(float offsetLeft, float offsetRight, float offsetTop, float offsetBottom)
        {
            this.offsetLeft = offsetLeft;
            this.offsetRight = offsetRight;
            this.offsetTop = offsetTop;
            this.offsetBottom = offsetBottom;
        }

        public float OffsetLeft
        {
            get { return offsetLeft; }
        }

        public float OffsetRight
        {
            get { return offsetRight; }
        }

        public float OffsetTop
        {
            get { return offsetTop; }
        }

        public float OffsetBottom
        {
            get { return offsetBottom; }
        }
    }
}