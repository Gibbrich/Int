using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class QuestTitleController : MonoBehaviour
{
    public Quest Quest { get; set; }

    #region Private methods

    [Inject] private UIController uiController;

    private Button button;

    #endregion

    // Use this for initialization
    void Start()
    {
        /* todo    - remove quest initialization
         * @author - Артур
         * @date   - 17.05.2018
         * @time   - 22:12
        */
        Quest = new Quest {Description = "Test description", Title = "Test title"};
        
        button = GetComponent<Button>();
        button.onClick.AddListener(() => uiController.OpenQuestDescriptionPanel(Quest));
    }

    // Update is called once per frame
    void Update()
    {
    }
}