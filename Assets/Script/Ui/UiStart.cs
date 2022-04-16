using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiStart : MonoBehaviour
{
    public Text Setsick;
    [SerializeField] private int Serse;

    private void Update()
    {
        Setsick.text = Serse.ToString();
    }

    // проверка
    public void StartButton()
    {
        if (Serse > 0 || Serse == -1)
            SceneManager.LoadScene(1);
    }

}
