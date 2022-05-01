using UnityEngine;
using System.Runtime.InteropServices;

public class ShareScreenScript : MonoBehaviour {

    [DllImport("__Internal")]
    private static extern void ShareFuncsiaTelegram(int moneu,int distansion);
    [DllImport("__Internal")]
    private static extern void ShareFuncsiaFacebook(int moneu, int distansion);
    [DllImport("__Internal")]
    private static extern void ShareFuncsiaTwitter(int moneu, int distansion);

    private int moneu;
    private int distansion;
    public MasterLevel ML;

    private void Start()
    {
        ML = GetComponent<MasterLevel>();
    }

    private void dani()
    {
        moneu = ML.GetMoneuShare();
        distansion = ML.GetDistansShare();
    }

    public void ShareTelegram()
    {
        dani();
        ShareFuncsiaTelegram(moneu, distansion);
    }

    public void ShareFacebook()
    {
        dani();
        ShareFuncsiaFacebook(moneu, distansion);
    }

    public void ShareTwitter()
    {
        dani();
        ShareFuncsiaTwitter(moneu, distansion);
    }
}
