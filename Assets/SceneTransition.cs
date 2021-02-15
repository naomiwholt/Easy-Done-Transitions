using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour 
{

    public enum EndSceneTriggerS {None, ButtonClick, ColliderTrigger }
    [Tooltip("if you want to trigger end scene yourself, select (none) from the dropdown menu and call the LoadNextScene() method in your code" +
    "          " +
    "LoadNextScene(SceneTransitionReference.NextScene, SceneTransitionReference.TransitionAnimation)")]

    public EndSceneTriggerS EndSceneTrigger; 
    public enum TransitionAnimations {FadeToColour, FadeToImage, Dissolve, VerticalSwipe, HorizontalSwipe}
    public TransitionAnimations TransitionAnimation;
    int ScenetoLoad;
    [SerializeField]
    public int NextScene;
    CanvasGroup TransitionScreen;
    private void Start()
    {
        SetEndSceneTrigger(EndSceneTrigger);    
    }
    void SetEndSceneTrigger(EndSceneTriggerS trigger)
    {
        switch (trigger)
        {
            case EndSceneTriggerS.ButtonClick:
                {
                    GameObject button = transform.GetChild(1).gameObject;
                    button.SetActive(true);
                }
                break;
        }
    }
   public void PlayTransitionAnimation(TransitionAnimations animation) 
    {      
        switch (animation)
        {
            case TransitionAnimations.FadeToColour:
                {
                    GameObject ColourFadeScreen = transform.GetChild(0).gameObject;
                    TransitionScreen = ColourFadeScreen.GetComponentInChildren<CanvasGroup>();
                    FadeIntoColour();
                }
                break;
            case TransitionAnimations.FadeToImage:
                {
                    
                }
                break;
            case TransitionAnimations.Dissolve:
                {
                    
                }
                break;
            case TransitionAnimations.VerticalSwipe:
                {
                    
                }
                break;
            case TransitionAnimations.HorizontalSwipe:
                {
                    
                }
                break;
        }
    }
    void LoadNextScene()
    {
        SceneManager.LoadScene(NextScene);
    }
    public void FadeIntoColour()
    { StartCoroutine(FadeCanvasGroup(TransitionScreen, TransitionScreen.alpha, 1, 1)); }
    void FadetoImage()
    {

    }
    void Dissolve()
    {

    }
    void VerticalSwipe()
    {

    }
    void HorizontalSwipe()
    {

    }

    public void ButtonClicked()
    {
        PlayTransitionAnimation(TransitionAnimation);
        transform.GetChild(1).gameObject.SetActive(false);
    }
    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime)
    {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        while (true)
        {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);
            cg.alpha = currentValue;
            if (percentageComplete >= 1) break;

            yield return new WaitForFixedUpdate();

        }
        LoadNextScene();
    }
}
