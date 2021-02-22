using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : Singleton<InputManager>
{
    public bool isMobile;

    public Camera mainCamera;
    Vector3 cameraForward;
    Vector3 cameraRight;

    public Vector3 inputMovement;
    public Vector3 inputRotation;

    public bool clickDown = false;
    public bool clickUp = false;

    //public Joystick leftJoystick;
    //public Joystick rightJoystick;

    public Vector3 mousePostionInWorld;
    public Vector3 lastMousePosition;

    public bool mouseOverUI = false;

    public float pinch;
    public float scrollWheel;

    public float timeWaitDoubleClick = 0.5f;
    private float currentTimeWait = 0;
    private bool waintingDoubleClick = false;
    public bool doubleClick = false;
    public Coroutine endWaiting;

    EventSystem eventSystem;

    private void Start()
    {
        currentTimeWait = timeWaitDoubleClick;
        eventSystem = GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();


#if UNITY_IOS || UNITY_ANDROID
         isMobile = true;
#endif

#if UNITY_STANDALONE
        isMobile = false;
#endif
    }

    public void CheckInputMobile()
    {
       /* inputMovement = leftJoystick.Horizontal * cameraRight + leftJoystick.Vertical * cameraForward;

        inputRotation = new Vector3(rightJoystick.Horizontal, 0, rightJoystick.Vertical);

        clickDown = rightJoystick.onPointerDown;
        clickUp = rightJoystick.onPointerUp;*/

        PinchInput();

        CheckDoubleClick();

        MoveDetect();
    }

    public void GetCameraDirections()
    {
        cameraForward = mainCamera.transform.TransformDirection(Vector3.forward);
        cameraForward.y = 0;
        cameraForward = cameraForward.normalized;
        cameraRight = new Vector3(cameraForward.z, 0, -cameraForward.x);
    }

    public void SetOverUI()
    {
        mouseOverUI = eventSystem.IsPointerOverGameObject();
    }

    public void CheckInputStandalone()
    {
        inputMovement = Input.GetAxis("Horizontal") * cameraRight + Input.GetAxis("Vertical") * cameraForward;

        clickDown = Input.GetMouseButtonDown(0);
        clickUp = Input.GetMouseButtonUp(0);

        inputRotation = new Vector3(mousePostionInWorld.x, 0, mousePostionInWorld.z);
        

        MouseWheel();

        CheckDoubleClick();

        SetOverUI();

        MoveDetect();

    }


    public void CheckDoubleClick()
    {
        if (clickDown == true)
        {
            if (waintingDoubleClick == true)
            {
                waintingDoubleClick = false;
                doubleClick = true;
            }
            else
            {
                if (endWaiting != null)
                {
                    StopCoroutine(endWaiting);
                }

                currentTimeWait = timeWaitDoubleClick;
                waintingDoubleClick = true;
                endWaiting = StartCoroutine(WaitingDoubleClick());
            }
        }
    }

    void PinchInput()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            pinch = deltaMagnitudeDiff;
        }
    }

    public void MoveDetect()
    {
        if (isMobile && Input.touchCount == 1)
        {
            Touch touchZero = Input.GetTouch(0);

            inputMovement = touchZero.position - touchZero.deltaPosition;
        }

        if (isMobile == false && Input.GetMouseButton(0) == true)
        {
            Debug.Log("MoveDetect");
            inputMovement = Input.mousePosition - lastMousePosition;
            Debug.Log(inputMovement);
           
            Debug.Log(inputMovement.magnitude);
        }

        lastMousePosition = Input.mousePosition;
    }

    void MouseWheel()
    {
        scrollWheel = Input.GetAxis("Mouse ScrollWheel");
    }

    IEnumerator WaitingDoubleClick()
    {
        if (waintingDoubleClick == true)
        {
            while (waintingDoubleClick == true)
            {
                if (currentTimeWait > 0 && doubleClick == false)
                {
                    currentTimeWait -= Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
                else if (currentTimeWait <= 0 || doubleClick == true)
                {
                    waintingDoubleClick = false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (doubleClick == true)
        {
            doubleClick = false;
        }

        GetCameraDirections();

        if (isMobile == true)
        {
            CheckInputMobile();
        }
        else
        {
            CheckInputStandalone();
        }
    }
}
