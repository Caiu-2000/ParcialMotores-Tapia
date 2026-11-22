using UnityEngine;
using UnityEngine.InputSystem;

public abstract class ExitReader : MonoBehaviour
{
    InputAction GoBack;

    protected virtual void Start()
    {
        GoBack = InputSystem.actions.FindAction("Cancel");
    }

    private void Update()
    {
        if (GoBack.WasPressedThisFrame())
        {
            GoBackPressed();
        }
    }

    protected abstract void GoBackPressed();

}