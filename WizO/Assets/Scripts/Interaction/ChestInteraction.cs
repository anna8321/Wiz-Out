using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChestInteraction : MonoBehaviour
{
    public XRGrabInteractable grabInteractable;
    public HingeJoint hingeJoint;
    private bool isOpen = false;

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (!isOpen)
        {
            OpenChest();
            isOpen = true;
        }
    }

    private void OpenChest()
    {
        JointSpring spring = hingeJoint.spring;
        spring.targetPosition = 90; // Ajustez cette valeur pour définir l'angle d'ouverture
        hingeJoint.spring = spring;
        hingeJoint.useSpring = true;
    }
}
