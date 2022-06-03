using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;

public static class ShareScreenScript {

    [DllImport("__Internal")]
    private static extern void ShareFuncsiaTelegram(int moneu,int distansion);
    [DllImport("__Internal")]
    private static extern void ShareFuncsiaFacebook(int moneu, int distansion);
    [DllImport("__Internal")]
    private static extern void ShareFuncsiaTwitter(int moneu, int distansion);
    [DllImport("__Internal")]
    private static extern void registor();
    [DllImport("__Internal")]
    private static extern bool buiNftJS();

    private static int moneu;
    private static int distansion;
    public static MasterLevel ML;

    private static void Start()
    {
        ML = MasterLevel.singleton;
    }

    private static void dani()
    {
        moneu = ML.GetMoneuShare();
        distansion = ML.GetDistansShare();
    }

    public static void ShareTelegram()
    {
        dani();
        ShareFuncsiaTelegram(moneu, distansion);
    }

    public static void ShareFacebook()
    {
        dani();
        ShareFuncsiaFacebook(moneu, distansion);
    }

    public static void ShareTwitter()
    {
        dani();
        ShareFuncsiaTwitter(moneu, distansion);
    }

    public static void registorAc()
    {
        registor();
    }

    public static bool buiNft()
    {
        return  buiNftJS();
    }

    public static IEnumerator barerc()
    {
        yield return new WaitForSeconds(1);
        ML.vozrat();
    }
}

