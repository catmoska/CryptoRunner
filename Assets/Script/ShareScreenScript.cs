using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;

public static class ShareScreenScript {
    // поделиса
    [DllImport("__Internal")]
    private static extern void ShareFuncsiaTelegram(int moneu,int distansion);
    [DllImport("__Internal")]
    private static extern void ShareFuncsiaFacebook(int moneu, int distansion);
    [DllImport("__Internal")]
    private static extern void ShareFuncsiaTwitter(int moneu, int distansion);
    

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

    ///////////////////// зарегистривуваса
    [DllImport("__Internal")]
    private static extern void registor();

    public static void registorAc()
    {
        registor();
    }

    ///////////////////// купить бокс
    [DllImport("__Internal")]
    private static extern bool buiNftJS();

    public static bool buiNft()
    {
        bool i = buiNftJS();
        //Debug.Log(i +  "  dasdad");
        return i;
    }

}

