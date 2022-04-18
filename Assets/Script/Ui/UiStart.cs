using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// при назатие кнопки старт в мену заходет в игру
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
