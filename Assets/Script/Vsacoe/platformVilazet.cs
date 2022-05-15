using UnityEngine;

public class platformVilazet : RunMonoBehaviour
{
    private Transform Pleir;
    public MasterLevel ML;
    private Vector3 nanravlenia;
    [SerializeField] private Vector3 smesenia = new Vector3(0, -5, 0);
    [SerializeField] private float triger = 15;
    [SerializeField] private float speed = 7;
    public bool raznia;

    void Start()
    {
        updeitMeneger.singleton.RMB.Add(this);
        if (Pleir == null)
            Pleir = PleirControlir.singletonGameObject.transform;
        if (ML == null)
            ML = MasterLevel.singleton;

        nanravlenia = transform.position;

        if (raznia)
            transform.position += smesenia * (1 + (Random.Range(0, 2) * -2))+ new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-2f, 2f));
        else
            transform.position += smesenia + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-2f, 2f));
    }

    public override bool Run()
    {
        if (Pleir == null)
            Pleir = PleirControlir.singletonGameObject.transform;

        if (ML == null)
            ML = MasterLevel.singleton;

        if (!ML.GetisPaus() && transform.position.x - Pleir.position.x < triger)
        {
            transform.position = Vector2.MoveTowards(transform.position, nanravlenia, speed * Time.fixedDeltaTime);
            if (transform.position.x - Pleir.position.x < -triger)
                Destroy(gameObject);
        }
        return true;
    }
}