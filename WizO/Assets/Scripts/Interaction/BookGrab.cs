using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BookGrab : MonoBehaviour
{
    public GameObject originalPrefab;

    private XRGrabInteractable grabInteractable;
    private Rigidbody originalRigidbody;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(HandleGrab);
        grabInteractable.selectExited.AddListener(HandleRelease);

        originalRigidbody = originalPrefab.GetComponent<Rigidbody>();
    }

    private void HandleGrab(SelectEnterEventArgs args)
    {
        XRBaseInteractor interactor = args.interactorObject as XRBaseInteractor;
        if (interactor != null)
        {

            originalPrefab.transform.SetParent(interactor.transform);
            originalPrefab.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 180, 0)); // Inverser la rotation sur l'axe Y

            // D�sactiver la gravit� pendant que l'objet est saisi
            if (originalRigidbody != null)
            {
                originalRigidbody.isKinematic = true; // D�sactiver la physique pour �viter des comportements inattendus
                originalRigidbody.detectCollisions = true; // Assurer que les collisions sont activ�es
            }

            originalPrefab.SetActive(true);
        }
    }

    private void HandleRelease(SelectExitEventArgs args)
    {
        XRBaseInteractor interactor = args.interactorObject as XRBaseInteractor;
        if (interactor != null)
        {
            // D�tacher l'objet de la main
            originalPrefab.transform.SetParent(null);

            // Activer la gravit� pour que l'objet tombe librement
            if (originalRigidbody != null)
            {
                originalRigidbody.isKinematic = false; // R�activer la physique pour que l'objet tombe
                originalRigidbody.detectCollisions = true; // Assurer que les collisions sont activ�es
            }

            // R�activer l'objet pour qu'il soit visible et soumis � la gravit�
            originalPrefab.SetActive(true);
        }
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(HandleGrab);
        grabInteractable.selectExited.RemoveListener(HandleRelease);
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
