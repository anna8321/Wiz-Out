using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad = "Chambre"; // Assurez-vous que le nom de la sc�ne est correct

    public void LoadScene()
    {
        
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.Log("Tentative de chargement de la sc�ne : " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Le nom de la sc�ne est vide ou invalide.");
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
