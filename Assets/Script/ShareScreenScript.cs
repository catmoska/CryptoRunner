using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;

public class ShareScreenScript : MonoBehaviour {

    [DllImport("__Internal")]
    private static extern void ShareFuncsiaTelegram(int moneu,int distansion);
    [DllImport("__Internal")]
    private static extern void ShareFuncsiaFacebook(int moneu, int distansion);
    [DllImport("__Internal")]
    private static extern void ShareFuncsiaTwitter(int moneu, int distansion);
    [DllImport("__Internal")]
    private static extern void registor();

    private int moneu;
    private int distansion;
    public MasterLevel ML;

    private void Start()
    {
        ML = MasterLevel.singleton;
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

    public void registorAc()
    {
        registor();
    }

    public IEnumerator barerc()
    {
        yield return new WaitForSeconds(1);
        ML.vozrat();
    }
}
