/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandColliderManager : MonoBehaviour
{
    public CapsuleCollider[] leftHandColliders;
    public CapsuleCollider[] rightHandColliders;

    void OnEnable()
    {
        XRGrabInteractable.selectEntered.AddListener(OnSelectEntered);
        XRGrabInteractable.selectExited.AddListener(OnSelectExited);
    }

    void OnDisable()
    {
        XRGrabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        XRGrabInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Désactivez les colliders des doigts pour éviter une distance de grab
        SetHandColliders(false);
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        // Réactivez les colliders des doigts après avoir relâché l'objet
        SetHandColliders(true);
    }

    private void SetHandColliders(bool enabled)
    {
        foreach (var collider in leftHandColliders)
        {
            collider.enabled = enabled;
        }
        foreach (var collider in rightHandColliders)
        {
            collider.enabled = enabled;
        }
    }
}
*/