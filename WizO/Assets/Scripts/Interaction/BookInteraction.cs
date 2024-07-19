using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BookInteraction : MonoBehaviour
{
    public GameObject closedBook; // Livre fermé
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

        // Assurez-vous que seul le livre fermé est visible au départ
        closedBook.SetActive(true);
        openBook.SetActive(false);

        // Sauvegardez la position et la rotation initiales du livre fermé
        initialClosedBookPosition = closedBook.transform.position;
        initialClosedBookRotation = closedBook.transform.rotation;

        // Assurez-vous que le Rigidbody est en mode kinematic et que la gravité est désactivée
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Lorsque l'objet est saisi, passez à l'état "ouvert"
        closedBook.SetActive(false);
        openBook.SetActive(true);

        // Désactivez le kinematic et activez la gravité pour que le livre puisse tomber
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        // Revenir à l'état fermé lorsque relâché
        closedBook.SetActive(true);
        openBook.SetActive(false);

        // Réinitialisez la position et la rotation du livre fermé
        closedBook.transform.position = initialClosedBookPosition;
        closedBook.transform.rotation = initialClosedBookRotation;

        // Réactivez le kinematic pour empêcher l'objet de tomber
        rb.isKinematic = true;
        rb.useGravity = false;
    }
}
