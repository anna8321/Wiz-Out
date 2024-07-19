using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BookInteraction : MonoBehaviour
{
    public GameObject closedBook; // Livre ferm�
    public GameObject openBook;   // Livre ouvert

    private XRGrabInteractable grabInteractable;
    private Vector3 initialClosedBookPosition;
    private Quaternion initialClosedBookRotation;
    private Rigidbody rb;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);

        // Assurez-vous que seul le livre ferm� est visible au d�part
        closedBook.SetActive(true);
        openBook.SetActive(false);

        // Sauvegardez la position et la rotation initiales du livre ferm�
        initialClosedBookPosition = closedBook.transform.position;
        initialClosedBookRotation = closedBook.transform.rotation;

        // Assurez-vous que le Rigidbody est en mode kinematic et que la gravit� est d�sactiv�e
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Lorsque l'objet est saisi, passez � l'�tat "ouvert"
        closedBook.SetActive(false);
        openBook.SetActive(true);

        // D�sactivez le kinematic et activez la gravit� pour que le livre puisse tomber
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        // Revenir � l'�tat ferm� lorsque rel�ch�
        closedBook.SetActive(true);
        openBook.SetActive(false);

        // R�initialisez la position et la rotation du livre ferm�
        closedBook.transform.position = initialClosedBookPosition;
        closedBook.transform.rotation = initialClosedBookRotation;

        // R�activez le kinematic pour emp�cher l'objet de tomber
        rb.isKinematic = true;
        rb.useGravity = false;
    }
}
