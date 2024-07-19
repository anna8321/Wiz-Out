using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VRScrollableText : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private TextMeshProUGUI infoText;

    private void Start()
    {
        string info = "Vous êtes un sorcier amnésique, piégé dans votre atelier. Trouvez les ingrédients pour préparer une potion et vous échapper.\n\n" +
                        "Objectif :\n" +
                        "Collectez les ingrédients décrits dans le grimoire pour concocter une potion magique et ouvrir la porte.\n\n";


        infoText.text = info;

        RectTransform contentRect = scrollRect.content;
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, infoText.preferredHeight);
    }

    public void ScrollUp()
    {
        scrollRect.verticalNormalizedPosition += 0.1f;
        if (scrollRect.verticalNormalizedPosition > 1f)
        {
            scrollRect.verticalNormalizedPosition = 1f;
        }
    }

    public void ScrollDown()
    {
        scrollRect.verticalNormalizedPosition -= 0.1f;
        if (scrollRect.verticalNormalizedPosition < 0f)
        {
            scrollRect.verticalNormalizedPosition = 0f;
        }
    }

    public void OnScrollValueChanged(Vector2 position)
    {
        float y = Mathf.Lerp(0f, 1f, position.y);
        Debug.Log("Current Y position: " + y);
    }
}
