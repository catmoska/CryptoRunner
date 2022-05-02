using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PleirControlir : MonoBehaviour
{
    public static PleirControlir singleton { get; private set; }
    public static GameObject singletonGameObject { get; private set; }

    [Header("Moving")]
    public float SpeedStart;
    public float Speed;
    public float SpeedX;
    public float Jamp;
    public float gravitasion;
    private Rigidbody2D rb;
    public Vector2 StrtPosision;

    [Header("Event")]
    public UnityEvent HitKill;
    public UnityEvent HitMoneuPlus;
    public UnityEvent HitEnergiaPlus;
    public GeneratorLevel GL;

    //is triger
    private int isGraund;
    private bool isDubleJamp;
    private bool isLiana;
    private bool isJamp;


    // разное
    public float visataMira = 50;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // проверка колисий с умертвяисям обекте
        if (collision.gameObject.tag == "Kill")
        {
            Umer();
            //Debug.Log(transform.position.x);
            HitKill.Invoke();
        }
        //всо осталное земля
        else isGraund++;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isGraund != 0)
        {
            //StartCoroutine(antiJamp(3f,0.02f));
            isGraund--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // проверка на столкновения с монетой
        if (collision.gameObject.tag == "Moneu") 
        {
            MoneuDellControlir k = collision.gameObject.GetComponent<MoneuDellControlir>();
            k.start();
            HitMoneuPlus.Invoke();
            Speed += SpeedX / 20;
        }
        if (collision.gameObject.tag == "Energia")
        {
            MoneuDellControlir k = collision.gameObject.GetComponent<MoneuDellControlir>();
            k.start();
            HitEnergiaPlus.Invoke();
            Speed += SpeedX;
        }
        else if (collision.gameObject.tag == "respavn")
        {
            Speed += SpeedX;
            GL.generit(collision.gameObject.transform.position);
        }
        else if (collision.gameObject.tag == "Leana")
        {
            isGraund++;
            isLiana = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Leana")
        {
            isGraund--;
            isLiana = false;
            StartCoroutine(antiJamp(5,0f));
            //StartCoroutine(gravitasia());
        }
    }

    private void Awake()
    {
        singleton = this;
        singletonGameObject = gameObject;
    }

    private void Start()
    {
        singleton = this;
        singletonGameObject = gameObject;
        start();
    }

    public void start()
    {
        singleton = this;
        singletonGameObject = gameObject;
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        isGraund = 1;
        Skin();
        transform.position = StrtPosision;
        Speed = SpeedStart;
    }

    private void FixedUpdate()
    {
        //вечное двизения вперод
        if (!MasterLevel.isPaus)
            transform.Translate(Vector2.right * Speed * Time.fixedDeltaTime);

        //амерть при проваливание
        if (transform.position.y < -10 && !MasterLevel.isPaus)
            HitKill.Invoke();
    }

    private void Update()
    {
        yslov();
        nrig();
        pausRigidbody2d();
    }

    // толчок в низ
    public IEnumerator antiJamp(float sila = 1, float time = -1)
    {
        time = (time == -1) ? 0.7f : time;
        yield return new WaitForSeconds(time);
        for (int i = 0; i < 20; i++)
        {
            rb.AddForce(new Vector2(0, (-Jamp / 3 * sila) / 20), ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.01f);
        }
    }

    // gravitasia
    public IEnumerator gravitasia()
    {
        for (int i = 0; i < 20; i++)
        {
            rb.AddForce(new Vector2(0, -gravitasion * Time.deltaTime * 3), ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.02f);
        }
    }

    //3 условия работи призка
    private void yslov()
    {
        //удар читеров(если привишает висату мира то его припускает)
        if (transform.position.y > visataMira) rb.AddForce(new Vector2(0, -Jamp * 2), ForceMode2D.Impulse);
        //востановления двоиного прижка или увиличает гравитасию
        if (isGraund > 0) isDubleJamp = true;
        else rb.AddForce(new Vector2(0, -gravitasion * Time.deltaTime), ForceMode2D.Impulse);
        //испраления бага до его появления
        if (isGraund < 0)
            isGraund = 0;
        //Debug.LogError("призок пошол по пи**е Eroor: PleirControlir ->yslov");
    }

    //релизасия призка
    private void nrig()
    {
        if (isLiana && ((Input.GetKey(KeyCode.Space) || isJamp) && !MasterLevel.isPaus))
        {
            KarankanaLIana();
            rb.AddForce(new Vector2(0, Jamp * Time.deltaTime * (4f - transform.position.y / 13)), ForceMode2D.Impulse);
        }
        else if ((Input.GetKeyDown(KeyCode.Space) || isJamp) && (isGraund > 0 || isDubleJamp) && !MasterLevel.isPaus)
        {
            isJamp = false;
            rb.velocity *= 0f;
            if (isGraund == 0) isDubleJamp = false;
            isGraund = 0;
            rb.AddForce(new Vector2(0, Jamp), ForceMode2D.Impulse);
            StartCoroutine(antiJamp(0.5f));
            nrizok();
        }
        else Idot();
    }

    public void jampicON()
    {
        isJamp = true;
    }

    public void jampicOFF()
    {
        isJamp = false;
    }

    //пауза Rigidbody2D
    bool isPaus;
    private void pausRigidbody2d()
    {
        if (MasterLevel.isPaus != isPaus)
        {
            if (MasterLevel.isPaus) rb.constraints = RigidbodyConstraints2D.FreezePosition;
            else rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.freezeRotation = true;
            Stoit();
        }
        isPaus = MasterLevel.isPaus;
    }


    //аниматор///////////////////////////////////////
    private void Stoit()
    {

    }

    private void Idot()
    {

    }

    private void nrizok()
    {

    }



    private void KarankanaLIana()
    {

    }

    private void Umer()
    {

    }

    public Animator anSkin;
    public string[] neimSkin = new string[10];
    public int[] NomerSkin = new int[10];
    private void Skin()
    {
        if (anSkin == null)
            return;
        for (int i = 0; i < neimSkin.Length; i++)
            anSkin.SetInteger(neimSkin[i], NomerSkin[i]);
    }
}











