using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using UnityEngine;

using UnityEngine.UI;


public class Enemy : MonoBehaviour
{

    public Text enemylv=null;

    public AudioClip hitSE1;
    public AudioClip hitSE2;
    public AudioClip hitSE3;


    [SerializeField]
    float SEvolume=0.8f;




    AudioSource audioSource;
    [SerializeField]
    Color[] colors;

    int col_lv=0;



    [SerializeField]
    ParticleSystem particle;


    Transform PlayerTr;

    [SerializeField]
    double E_attackCT = 0;
    [SerializeField]
    double E_CT = 0.5;


    [SerializeField]
    int E_damage=2;

    [SerializeField]
    float E_moveSpeed = 2;
    Vector2 E_moveDirec;

    [SerializeField]
    int E_HP = 10;
    bool E_isDead = false;

    float E_time;


    int E_point=0;

    
    Player player;

    IngameController ingameController;

    EnemyMaker enemymaker;

    public void Initialize(IngameController controller,EnemyMaker maker)
    {
        ingameController = controller;
        enemymaker = maker;
        StartCoroutine("ColorChange");
    }

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        E_time = Time.time;// Enemy born Time

        PlayerTr = GameObject.FindGameObjectWithTag("Player").transform;

        player = PlayerTr.GetComponent<Player>();
        particle.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ingameController.State != InGameState.Game)
        {
            StopAllCoroutines();
            return;
        }

        if (E_isDead)
        {
            return;
        }

        Move();

        enemylv.text = ""+(int)(Time.time - E_time);


        
        //main.startColor = colors[col_lv];



    }
    void Move()
    {
        //float E_h = Input.GetAxis("Horizontal");
        //float E_v = Input.GetAxis("Vertical");
        if (Vector2.Distance(transform.position, PlayerTr.position) < 0.1f)
        {
            return;
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(PlayerTr.position.x, PlayerTr.position.y),
            E_moveSpeed * Time.deltaTime
            );
        if(Time.time - E_attackCT >= E_CT)
        {
            E_attackCT = 0;
        }

        //E_moveDirec = Vector2.ClampMagnitude(new Vector2(E_h, E_v), 1);
        //transform.Translate(E_moveDirec * E_moveSpeed * Time.deltaTime);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Col(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Col(collision);
    }
    void Col(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && E_attackCT == 0)
        {
            player.Damage(E_damage);
            Debug.Log("Damage!!");
            E_attackCT = Time.time;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            //E_isDead = true;

            ingameController.AddScore((int)(Time.time - E_time));
            if (Time.time - E_time < 10)
            {
                audioSource.PlayOneShot(hitSE1, SEvolume);
            }
            else if (Time.time - E_time < 20)
            {
                audioSource.PlayOneShot(hitSE2,SEvolume);
            }
            else
            {
                audioSource.PlayOneShot(hitSE3,SEvolume);
            }

            Instantiate(particle.gameObject, transform.position, Quaternion.identity).SetActive(true);

            transform.position = enemymaker.GetRandomPosition();
        }
    }
    IEnumerator ColorChange()
    {
        while(col_lv<3)
        {
            enemylv.color = colors[col_lv];
            ParticleSystem.MainModule main = particle.main;
            main.startColor = colors[col_lv];
            yield return new WaitForSeconds(10);
            col_lv++;
        }
    }

}
