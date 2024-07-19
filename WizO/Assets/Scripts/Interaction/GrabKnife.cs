using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabKnife : MonoBehaviour
{
    public Transform knifePrefab; // Assurez-vous de glisser votre prefab de couteau dans cette variable depuis l'inspecteur
    public Color knifeColor = Color.blue; // D�finissez la couleur d�sir�e pour l'instanci�
    public Color originalColor = Color.green; // Couleur pour indiquer que le couteau original est r�activ�

    private XRGrabInteractable grabInteractable;
    private Transform currentKnifeInstance;
    private Renderer originalRenderer;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab); // Ajoute l'�couteur pour l'�v�nement de saisie
        grabInteractable.selectExited.AddListener(OnRelease); // Ajoute l'�couteur pour l'�v�nement de lib�ration

        originalRenderer = GetComponent<Renderer>();

        if (originalRenderer != null)
        {
            originalRenderer.material.color = originalColor; // Assurez-vous que la couleur d'origine est correcte
        }
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab); // Supprime l'�couteur lorsque l'objet est d�truit
        grabInteractable.selectExited.RemoveListener(OnRelease); // Supprime l'�couteur lorsque l'objet est d�truit
    }

    // Cette m�thode est appel�e lorsque l'objet est saisi
    private void OnGrab(SelectEnterEventArgs args)
    {
        if (originalRenderer != null)
        {
            originalRenderer.material.color = Color.red; // Change la couleur de l'objet original pour indiquer qu'il est d�sactiv�
        }

        gameObject.SetActive(false); // D�sactive le couteau original

        // Cr�ez une instance du couteau
        currentKnifeInstance = Instantiate(knifePrefab, args.interactorObject.transform.position, args.interactorObject.transform.rotation);

        // Assurez-vous que l'objet instanci� est un enfant de l'interactor pour qu'il suive la main
        currentKnifeInstance.SetParent(args.interactorObject.transform, true);

        // R�orientez le couteau pour que la lame ne pointe pas vers le joueur
        currentKnifeInstance.localRotation = Quaternion.Euler(currentKnifeInstance.localRotation.eulerAngles.x,
                                                              currentKnifeInstance.localRotation.eulerAngles.y + 180f, // Inverser l'axe Y
                                                              currentKnifeInstance.localRotation.eulerAngles.z);

        // Changez la couleur du couteau instanci�
        Renderer knifeRenderer = currentKnifeInstance.GetComponent<Renderer>();
        if (knifeRenderer != null)
        {
            knifeRenderer.material.color = knifeColor;
        }
        else
        {
            Debug.LogWarning("Le couteau instanci� n'a pas de Renderer.");
        }

        // Assurez-vous que le Rigidbody est configur� correctement
        Rigidbody knifeRigidbody = currentKnifeInstance.GetComponent<Rigidbody>();
        if (knifeRigidbody != null)
        {
            knifeRigidbody.isKinematic = true; // Assurez-vous que l'objet est kinematic pour �viter les forces physiques non d�sir�es
        }
        else
        {
            Debug.LogWarning("Le couteau instanci� n'a pas de Rigidbody.");
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
                knifeRigidbody.isKinematic = false; // Permet � l'objet de r�agir � la physique
            }

            currentKnifeInstance.SetParent(null); // D�tache l'objet instanci� de la main
            Destroy(currentKnifeInstance.gameObject); // D�truit l'instance actuelle du couteau
            currentKnifeInstance = null; // R�initialise la r�f�rence
        }

        gameObject.SetActive(true); // R�active le couteau original

        // Changez la couleur du couteau original pour indiquer qu'il est r�activ�
        if (originalRenderer != null)
        {
            originalRenderer.material.color = originalColor;
        }
    }
}
