using UnityEngine;

public class RunMonoBehaviour : MonoBehaviour
{
    private void Start()
    {updeitMeneger.singleton.RMB.Add(this);}

    public virtual bool Run()
    { return false; }
}