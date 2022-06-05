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

    private static void dani()
    {
        MasterLevel ML;
        ML = MasterLevel.singleton;
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
        bool i = buiNftJS();
        //Debug.Log(i +  "  dasdad");
        return i;
    }

}

