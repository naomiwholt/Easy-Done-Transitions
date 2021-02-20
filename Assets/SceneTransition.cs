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
    public enum TransitionAnimations {FadeToColour, FadeToImage, Dissolve, Swipe}
    public TransitionAnimations TransitionAnimation;
    public enum SwipeDirection { Left, Right, Up, Down}
    public SwipeDirection Direction;
    public int SwipeSpeed;
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
            case TransitionAnimations.Swipe:
                {
                    GameObject Panel = transform.GetChild(0).gameObject;
                  //  Vector2 SwipeDir;
                    Vector3 EndPos = Panel.transform.position;
                    float Xoffset = Screen.width + Panel.transform.localScale.x;
                    float Yoffset = Screen.height + Panel.transform.localScale.y;
                    switch (Direction) 
                    {
                        
                        case SwipeDirection.Left:
                         {
                                //SwipeDir = new Vector2(1, 0);                               
                                Panel.transform.position = new Vector3(Panel.transform.position.x + Xoffset, Panel.transform.position.y, Panel.transform.position.z);
                                SwipeScreen(Panel,EndPos);
                         }
                         break;
                        case SwipeDirection.Right:
                            {
                                //SwipeDir = new Vector2(-1, 0);                              
                                Panel.transform.position = new Vector3(Panel.transform.position.x - Xoffset, Panel.transform.position.y, Panel.transform.position.z);
                                SwipeScreen(Panel,EndPos);
                            }
                          break;
                         case SwipeDirection.Up:
                         {
                                // SwipeDir = new Vector2(0, -1);                               
                                Panel.transform.position = new Vector3(Panel.transform.position.x, Panel.transform.position.y - Yoffset, Panel.transform.position.z);
                                SwipeScreen(Panel,EndPos);
                         }
                         break;
                        case SwipeDirection.Down:
                            {
                               // SwipeDir = new Vector2(0, 1);                                
                                Panel.transform.position = new Vector3(Panel.transform.position.x, Panel.transform.position.y + Yoffset, Panel.transform.position.z);                               
                                SwipeScreen(Panel,EndPos);
                            }
                         break;
                    }
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
    void SwipeScreen(GameObject a, Vector3 b)
    {        
        TransitionScreen = a.GetComponent<CanvasGroup>();
        TransitionScreen.alpha = 1;
        StartCoroutine(Swipe(a,a.transform.position,b,SwipeSpeed));
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
    public IEnumerator Swipe(GameObject PanelSLide, Vector2 start, Vector2 end, float lerpTime)
    {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;
        while (true)
        {
            print("swiping");
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;
            PanelSLide.transform.position = Vector3.Lerp(start, end, percentageComplete);
            if (percentageComplete >= 1)  break; 
            yield return new WaitForFixedUpdate();
        }
        LoadNextScene();
      
    }
}
