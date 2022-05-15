using System;
using System.Collections.Generic;
using UnityEngine;

public class updeitMeneger : MonoBehaviour
{
    public static updeitMeneger singleton { get; private set; }
    public List<RunMonoBehaviour> RMB;
    public int sislaSmena=20;
    private List<int> nomentic = new List<int>();
    private int sisla;
    public MasterLevel ML;

    private void Awake() 
    {
        RMB = new List<RunMonoBehaviour>();
        singleton = this;
    }

    private void Start()
    {
        if (ML == null)
            ML = MasterLevel.singleton;
    }

    public void start()
    {
        RMB = new List<RunMonoBehaviour>();
        nomentic = new List<int>();
        FixedUpdate();
    }

    private bool Run(int i)
    {
        if (RMB[i] == null) {
            nomentic.Add(i);
            return true; 
        }
        return RMB[i].Run();
    }
   

    void FixedUpdate()
    {
        //if (ML.GetisPaus()) return;

        int i;

        for (i=0;i < RMB.Count; i++)
            if(!Run(i))break;
        if (i == RMB.Count) return;
        int res = i + 10;

        for (i = 0; res>i && i < RMB.Count; i++)
            if(Run(i))res= i + 10;

        if (sislaSmena <= sisla)
        {
            sisla = 0;
            nomentic = new List<int>();
            sistka();
        }
        sisla++;
    }

    void sistka()
    {
        if (nomentic == null) return;
        if (nomentic.Count == 0) return;
        if (nomentic.Count == 1)
        { RMB.RemoveAt(nomentic[0]);return;}
        if (nomentic.Count == 2)
        { RMB.RemoveAt(nomentic[1]); RMB.RemoveAt(nomentic[0]); return; }
        if (nomentic.Count == 3)
        { RMB.RemoveAt(nomentic[0]); RMB.RemoveAt(nomentic[1]); RMB.RemoveAt(nomentic[2]); return; }



        int[] arre = new int[nomentic.Count];
        for(int i =0;i< nomentic.Count; i++)
            arre[i] = nomentic[i];
        
        Array.Sort(arre);
        int nrosl=-1;
        for (int i = 0; i < nomentic.Count; i++)
        {
            int ten = arre[arre.Length - 1 - i];
            if (nrosl != ten) 
            {
                nrosl = ten;
                RMB.RemoveAt(ten);
            } 
        }
    }
}
