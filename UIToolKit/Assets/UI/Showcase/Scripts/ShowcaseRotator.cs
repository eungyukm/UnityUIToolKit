using System.Collections;
using UnityEngine;

public class ShowcaseRotator : MonoBehaviour
{
    public int speed = 30;
    private Coroutine showcaseRoutine;
    
    public void ShowCaseSpine()
    {
        showcaseRoutine = StartCoroutine(ShowcaseSpineRoutine());
    }

    public void ShowCaseStop()
    {
        StopCoroutine(showcaseRoutine);
    }

    private IEnumerator ShowcaseSpineRoutine()
    {
        while (true)
        {
            transform.Rotate(Vector3.up * (speed * Time.deltaTime));
            yield return null;
        }
    }
}
