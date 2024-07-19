using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabKnife : MonoBehaviour
{
    public Transform knifePrefab; // Assurez-vous de glisser votre prefab de couteau dans cette variable depuis l'inspecteur
    public Color knifeColor = Color.blue; // Définissez la couleur désirée pour l'instancié
    public Color originalColor = Color.green; // Couleur pour indiquer que le couteau original est réactivé

    private XRGrabInteractable grabInteractable;
    private Transform currentKnifeInstance;
    private Renderer originalRenderer;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab); // Ajoute l'écouteur pour l'événement de saisie
        grabInteractable.selectExited.AddListener(OnRelease); // Ajoute l'écouteur pour l'événement de libération

        originalRenderer = GetComponent<Renderer>();

        if (originalRenderer != null)
        {
            originalRenderer.material.color = originalColor; // Assurez-vous que la couleur d'origine est correcte
        }
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab); // Supprime l'écouteur lorsque l'objet est détruit
        grabInteractable.selectExited.RemoveListener(OnRelease); // Supprime l'écouteur lorsque l'objet est détruit
    }

    // Cette méthode est appelée lorsque l'objet est saisi
    private void OnGrab(SelectEnterEventArgs args)
    {
        if (originalRenderer != null)
        {
            originalRenderer.material.color = Color.red; // Change la couleur de l'objet original pour indiquer qu'il est désactivé
        }

        gameObject.SetActive(false); // Désactive le couteau original

        // Créez une instance du couteau
        currentKnifeInstance = Instantiate(knifePrefab, args.interactorObject.transform.position, args.interactorObject.transform.rotation);

        // Assurez-vous que l'objet instancié est un enfant de l'interactor pour qu'il suive la main
        currentKnifeInstance.SetParent(args.interactorObject.transform, true);

        // Réorientez le couteau pour que la lame ne pointe pas vers le joueur
        currentKnifeInstance.localRotation = Quaternion.Euler(currentKnifeInstance.localRotation.eulerAngles.x,
                                                              currentKnifeInstance.localRotation.eulerAngles.y + 180f, // Inverser l'axe Y
                                                              currentKnifeInstance.localRotation.eulerAngles.z);

        // Changez la couleur du couteau instancié
        Renderer knifeRenderer = currentKnifeInstance.GetComponent<Renderer>();
        if (knifeRenderer != null)
        {
            knifeRenderer.material.color = knifeColor;
        }
        else
        {
            Debug.LogWarning("Le couteau instancié n'a pas de Renderer.");
        }

        // Assurez-vous que le Rigidbody est configuré correctement
        Rigidbody knifeRigidbody = currentKnifeInstance.GetComponent<Rigidbody>();
        if (knifeRigidbody != null)
        {
            knifeRigidbody.isKinematic = true; // Assurez-vous que l'objet est kinematic pour éviter les forces physiques non désirées
        }
        else
        {
            Debug.LogWarning("Le couteau instancié n'a pas de Rigidbody.");
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if (currentKnifeInstance != null)
        {
            // Assurez-vous que le Rigidbody n'est plus kinematic
            Rigidbody knifeRigidbody = currentKnifeInstance.GetComponent<Rigidbody>();
            if (knifeRigidbody != null)
            {
                knifeRigidbody.isKinematic = false; // Permet à l'objet de réagir à la physique
            }

            currentKnifeInstance.SetParent(null); // Détache l'objet instancié de la main
            Destroy(currentKnifeInstance.gameObject); // Détruit l'instance actuelle du couteau
            currentKnifeInstance = null; // Réinitialise la référence
        }

        gameObject.SetActive(true); // Réactive le couteau original

        // Changez la couleur du couteau original pour indiquer qu'il est réactivé
        if (originalRenderer != null)
        {
            originalRenderer.material.color = originalColor;
        }
    }
}
