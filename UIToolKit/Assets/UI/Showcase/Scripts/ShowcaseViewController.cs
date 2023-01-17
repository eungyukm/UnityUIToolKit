using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class ShowcaseViewController : MonoBehaviour
{
    private UIDocument _showcaseMain;

    [SerializeField] private ShowcaseRotator showcaseRotator;

    private Button _btnSpine;
    private Button _btnStop;

    private void OnEnable()
    {
        _showcaseMain = GetComponent<UIDocument>();
        var showcaseMainRoot = _showcaseMain.rootVisualElement;
        _btnSpine = showcaseMainRoot.Q<Button>("showcase__footer-spine");
        _btnStop = showcaseMainRoot.Q<Button>("showcase__footer-stop");

        _btnSpine.clicked += () =>
        {
            showcaseRotator.ShowCaseSpine();
        };
        
        _btnStop.clicked += () =>
        {
            showcaseRotator.ShowCaseStop();
        };
    }
}
