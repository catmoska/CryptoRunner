using UnityEngine;

// updeitMeneger ����� ������������
public abstract class RunMonoBehaviour : MonoBehaviour
{
    private void Start()
    {updeitMeneger.singleton.RMB.Add(this);}

    public abstract bool Run();
}