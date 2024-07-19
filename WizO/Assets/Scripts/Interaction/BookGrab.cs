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

            // Désactiver la gravité pendant que l'objet est saisi
            if (originalRigidbody != null)
            {
                originalRigidbody.isKinematic = true; // Désactiver la physique pour éviter des comportements inattendus
                originalRigidbody.detectCollisions = true; // Assurer que les collisions sont activées
            }

            originalPrefab.SetActive(true);
        }
    }

    private void HandleRelease(SelectExitEventArgs args)
    {
        XRBaseInteractor interactor = args.interactorObject as XRBaseInteractor;
        if (interactor != null)
        {
            // Détacher l'objet de la main
            originalPrefab.transform.SetParent(null);

            // Activer la gravité pour que l'objet tombe librement
            if (originalRigidbody != null)
            {
                originalRigidbody.isKinematic = false; // Réactiver la physique pour que l'objet tombe
                originalRigidbody.detectCollisions = true; // Assurer que les collisions sont activées
            }

            // Réactiver l'objet pour qu'il soit visible et soumis à la gravité
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
