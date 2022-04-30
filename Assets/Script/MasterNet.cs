using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public class MasterNet : MonoBehaviour
{
    public string url = "http://127.0.0.1:8000/DATA/";

    //////////
    public void PostRequest(string json)
    { StartCoroutine(PostRequestB(json)); }

    private IEnumerator PostRequestB(string jso)
    {
        string json = "";
        for (int i = 1; i < jso.Length - 1; i++)
            json += i;


        WWWForm form = new WWWForm();

        string kei = "";
        string znasenia = "";
        bool kto = false;


        for (int i = 0; i < json.Length; i++)
        {
            if (json[i] == '"')
                break;
            else if (json[i] != ':' && json[i] != ',')
            {
                if (kto)
                    kei += i;
                else
                    znasenia += i;
            }
            else if (json[i] == ':' && json[i] != ',')
                kto = true;
            else if (json[i] != ':' && json[i] == ',')
            {
                form.AddField(kei, znasenia);
                kto = false;
                kei = "";
                znasenia = "";
            }
            //form.AddField("Money", "1");
        }

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
            Debug.Log(www.error);
    }



    //////////////////
    public void PostRequest(int Money, float Distansion, int NFTVID,int Nonztia =0)
    { StartCoroutine(PostRequestB(Money, Distansion, NFTVID, Nonztia)); }

    private IEnumerator PostRequestB(int Money, float Distansion, int NFTVID, int Nonztia)
    {
        WWWForm form = new WWWForm();
        form.AddField("Money", Money.ToString());
        form.AddField("Distansion", Distansion.ToString());
        form.AddField("NFTVID", NFTVID.ToString());
        form.AddField("Nonztia", Nonztia.ToString());

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
            Debug.Log(www.error);
    }










    /////////////////////////////////
    private bool zanusk = false;
    private bool zanros = true;
    private string date = "";

    public string GetRequest()
    {
        if (!zanros && !zanusk)
        {
            zanros = true;
            return date;
        }
        else if (zanros && !zanusk)
        {
            zanros = false;
            zanusk = true;
            StartCoroutine(GetRequestB());
        }
        return "";
    }



    IEnumerator GetRequestB()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split();
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    date = "EroorNet";
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    date = "EroorNet";
                    break;
                case UnityWebRequest.Result.Success:
                    date = webRequest.downloadHandler.text;
                    break;
            }
            zanusk = false;
        }
    }
}
