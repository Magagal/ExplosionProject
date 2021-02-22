using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PhysicsPropertyManager : Singleton<PhysicsPropertyManager>
{
    //[Tooltip("The keyboard button to toggle the time scale.")]
    //public KeyCode timeScaleKey = KeyCode.Z;
    [Tooltip("The altered time scale.")]
    public float timeScale;
    [Tooltip("The UI image for the time mode.")]
    public Image timeImage;
   
    bool timeToggle;
    
    float defaultTimeScale;
    float defaultFixedDeltaTime;
    Vector3 defaultGravity;

    void Start()
    {
        defaultTimeScale = Time.timeScale;
        defaultFixedDeltaTime = Time.fixedDeltaTime;
        defaultGravity = Physics.gravity;

        SetTimeImage(timeToggle);
    }

    public void SetTimeScale(bool _timeScale)
    {
        timeToggle = _timeScale;
        Time.timeScale = _timeScale ? timeScale : defaultTimeScale;
        Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
        SetTimeImage(_timeScale);

        timeToggle = _timeScale;
    }

    void Update()
    {
        //Check to see if time change key has been released
        //if (Input.GetKeyUp(timeScaleKey))
        {
            //Reverse the time toggle and change the timeScale
            //timeToggle = !timeToggle;
            //SetTimeScale(timeToggle);
        }
    }


    void SetTimeImage(bool v)
    {
        timeImage.gameObject.SetActive(v);
    }
}
