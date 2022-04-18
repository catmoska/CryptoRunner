using UnityEngine;
using UnityEngine.UI;

//читает количество монет и виводит
public class UiSpisac : MonoBehaviour
{
    public MasterLevel ML;
    public GameObject menu;
    public Text moneuText;
    public Text serseText;
    private int Moneu;

    private void Start()
    {
        if (ML == null)
            ML = GetComponent<MasterLevel>();
        int Life = ML.Getlife();
        moneuText.text = "0";
        serseText.text = Life.ToString();
    }

    public void MoneuPlus()
    {
        //Debug.Log("MoneuPlus");
        Moneu++;
        moneuText.text = Moneu.ToString();
    }

    public int GetMoneu()
    {
        return Moneu;
    }
}
