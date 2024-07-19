using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad = "Chambre"; // Assurez-vous que le nom de la scène est correct

    public void LoadScene()
    {
        
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.Log("Tentative de chargement de la scène : " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Le nom de la scène est vide ou invalide.");
        }
    }

    public void KillApp()
    {
        Application.Quit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    }
}
