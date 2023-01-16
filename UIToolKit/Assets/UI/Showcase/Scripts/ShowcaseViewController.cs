using UnityEngine;
using UnityEngine.UIElements;

public class ShowcaseViewController : MonoBehaviour
{
    private UIDocument showcaseMain;

    [SerializeField] private ShowcaseRotator _showcaseRotator;

    private Button btnSpine;
    private Button btnStop;

    private void OnEnable()
    {
        showcaseMain = GetComponent<UIDocument>();
        VisualElement showcaseMainRoot = showcaseMain.rootVisualElement;
        btnSpine = showcaseMainRoot.Q<Button>("showcase__footer-spine");
        btnStop = showcaseMainRoot.Q<Button>("showcase__footer-stop");

        btnSpine.clicked += () =>
        {
            _showcaseRotator.ShowCaseSpine();
        };
        
        btnStop.clicked += () =>
        {
            _showcaseRotator.ShowCaseStop();
        };
    }
}
