using UnityEngine;

// updeitMeneger толка наследования
public abstract class RunMonoBehaviour : MonoBehaviour
{
    private void Start()
    {updeitMeneger.singleton.RMB.Add(this);}

    public abstract bool Run();
}