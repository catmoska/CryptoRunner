using UnityEngine;

public class vilkaControlir : MonoBehaviour
{
    private Transform Pleir;
    public MasterLevel ML;
    public float triger;
    public float trigerObzect;
    public float speed;
    public GameObject znasok;
    public float smesenia;


    private void Start()
    {
        if (Pleir == null)
            Pleir = PleirControlir.singletonGameObject.transform;
        
        if (ML == null)
            ML = MasterLevel.singleton;
    }

    void FixedUpdate()
    {
        if (Pleir == null)
            Pleir = PleirControlir.singletonGameObject.transform;
        if (ML == null)
            ML = MasterLevel.singleton;

        if (znasok != null && transform.position.x - Pleir.position.x <= triger+ trigerObzect)
            znasok.transform.position = new Vector2(Pleir.position.x+ smesenia,transform.position.y);

        //Debug.Log(ML.GetisPaus());
        //Debug.Log(Pleir.position.x);


        if (!ML.GetisPaus() && transform.position.x - Pleir.position.x <= triger)
        {
            transform.Translate(Vector2.up * speed * Time.fixedDeltaTime);
            if (transform.position.x - Pleir.position.x <= -triger)
                Destroy(gameObject);
            if (znasok !=null)
                Destroy(znasok);
        }
    }
}
