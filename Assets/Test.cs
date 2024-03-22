using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    [SerializeField] private float totalTime = 0f;
    [SerializeField] private bool allowIncrease = false;
    InputAction.CallbackContext currentContext;
    private float maxTimeDT = 0.2f;
    private float minTimePress = 0.15f;
    bool isDoubleTap = false;
    bool IsTapped = true;
    private void Update()
    {
        if (allowIncrease)
            totalTime += Time.deltaTime;

        if (totalTime >= maxTimeDT)
        {
            allowIncrease = false;
            totalTime = default;
            isDoubleTap = false;
            if (IsTapped)
                Debug.Log("Tap");
            IsTapped = true;
        }
        else if (currentContext.phase == InputActionPhase.Performed && totalTime >= minTimePress)
        {
            totalTime = minTimePress;

            Debug.Log("Press");
        }
    }

    public void OnTouch(InputAction.CallbackContext context)
    {
        currentContext = context;
        switch (context.phase)
        {
            case InputActionPhase.Waiting:
                break;
            case InputActionPhase.Disabled:
                break;
            case InputActionPhase.Started:
                if (isDoubleTap)
                {
                    IsTapped = false;
                    Debug.Log("DoubleTap");
                    totalTime = maxTimeDT;
                }
                break;
            case InputActionPhase.Performed:
                allowIncrease = true;
                isDoubleTap = true;
                break;
            case InputActionPhase.Canceled:
                break;
        }
    }
}
