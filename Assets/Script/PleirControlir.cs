using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PleirControlir : MonoBehaviour
{
    [Header("Moving")]
    public float Speed;
    public float SpeedX;
    public float Jamp;
    public float gravitasion;

    [Header("Event")]
    public UnityEvent HitKill;
    public UnityEvent HitMoneuPlus;
    public GeneratorLevel GL;

    private int isGraund;
    private bool isDubleJamp;
    private bool isLiana;
    private Rigidbody2D rb;
    public float visataMira=50;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        // проверка колисий с умертвяисям обекте
        if (collision.gameObject.tag == "Kill")
        {
            //Debug.Log(transform.position.x);
            HitKill.Invoke();
        }
        // проверка на столкновения с монетой
        else if (collision.gameObject.tag == "Moneu")
        {
            Destroy(collision.gameObject);
            HitMoneuPlus.Invoke();
        }
        //всо осталное земля
        else isGraund++;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGraund--;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // проверка на столкновения с монетой
        if (collision.gameObject.tag == "Moneu")
        {
            MoneuDellControlir k = collision.gameObject.GetComponent<MoneuDellControlir>();
            k.start();
            HitMoneuPlus.Invoke();
            Speed += SpeedX/20;
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
            StartCoroutine(antiJamp(5));
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isGraund ++;
    }


    void FixedUpdate()
    {
        if(!MasterLevel.isPaus)
            transform.Translate(Vector2.right * Speed * Time.deltaTime);

        if (transform.position.y < -10)
            HitKill.Invoke();
    }

    bool isPaus;
    void Update()
    {
        if (transform.position.y > visataMira) rb.AddForce(new Vector2(0, -Jamp * 2), ForceMode2D.Impulse);
        if (isGraund > 0) isDubleJamp = true;
        else rb.AddForce(new Vector2(0, -gravitasion * Time.deltaTime), ForceMode2D.Impulse);
        if (isGraund < 0) isGraund = 0;

        if (isLiana)
        {
            if (Input.GetKey(KeyCode.Space) && !MasterLevel.isPaus)
            {
                rb.AddForce(new Vector2(0, Jamp*Time.deltaTime*2), ForceMode2D.Impulse);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && (isGraund>0 || isDubleJamp) && !MasterLevel.isPaus)
            {
                if (isGraund == 0) isDubleJamp = false;
                rb.AddForce(new Vector2(0, Jamp), ForceMode2D.Impulse);
                StartCoroutine(antiJamp(0.5f));
                isGraund--;
            }
        }


        if(MasterLevel.isPaus!= isPaus)
        {
            if(MasterLevel.isPaus) rb.constraints = RigidbodyConstraints2D.FreezePosition;
            else rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.freezeRotation = true;
        }
        isPaus = MasterLevel.isPaus;
    }

    public IEnumerator antiJamp(float sila = 1)
    {
        yield return new WaitForSeconds(0.7f);
        rb.AddForce(new Vector2(0, -Jamp/3* sila), ForceMode2D.Impulse);
    }

}
