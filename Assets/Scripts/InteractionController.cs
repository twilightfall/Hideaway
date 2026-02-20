using StarterAssets;
using System;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    [SerializeField]
    GameObject currentlyLookingAt;

    StarterAssetsInputs _input;

    [SerializeField]    
    TMP_Text HUDText;

    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    private void LateUpdate()
    {
        Look();
    }

    private void Look()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        Debug.DrawRay(ray.origin, ray.direction * 3f, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, 3f, ~LayerMask.GetMask("Triggers")))
        {            
            if (hit.transform.gameObject.TryGetComponent(out InteractionHandler handler))
            {
                currentlyLookingAt = hit.transform.gameObject;

                HUDText.text = handler.SetInteractionText();
                
            }
            else
            {
                currentlyLookingAt = null;

                HUDText.text = string.Empty;
            }
        }
        else
        {
            currentlyLookingAt = null;
            HUDText.text = string.Empty;
        }
    }

    public void OnInteract(InputValue value)
    {
        if (currentlyLookingAt != null && value.isPressed)
        {
            if (currentlyLookingAt.TryGetComponent(out InteractionHandler handler))
            {
                handler.Interact();
            }
        }
    }

    public void OnOpenMenu(InputValue value)
    {
        if (value.isPressed)
        {
            GameManager.Instance.OpenMenu();
        }
    }
}
