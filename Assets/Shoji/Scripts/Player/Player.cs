using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    int health = 10;
    [SerializeField]
    float moveSpeed = 10;
    Vector2 moveDirec;
    public Vector2 MoveDirec { get => moveDirec; }

    float startTime;
    [SerializeField]
    WeaponBase[] weapons;

    bool isDead = false;

    [SerializeField]
    IngameController ingameController;
    bool isPlaying = false;

    [SerializeField]
    AudioSource source;
    [SerializeField]
    AudioClip deadSE;
    [SerializeField]
    GameObject deathParticle;

    void Awake()
    {
        foreach (var weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }
        StartCoroutine("StartEvent");
    }
    IEnumerator StartEvent()
    {
        if (ingameController != null)
            yield return new WaitUntil(() => ingameController.State == InGameState.Game);
        startTime = Time.time;
        isPlaying = true;
        StartCoroutine("ChangeWeapon");
    }
    int debugI = 0;
    void Update()
    {
        if (!isPlaying) return;
        if (isDead) return;
        if (ingameController && ingameController.State != InGameState.Game) StopPlayer();

        //デバッグ用武器変更
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    debugI++;
        //    startTime = Time.time - 10 * debugI - Time.deltaTime;
        //    StopAllCoroutines();
        //    StartCoroutine("ChangeWeapon");
        //}

        LookCursor();
        Move();
    }
    void LookCursor()
    {
        Vector3 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursor.z = 0;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, cursor - transform.position);
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        moveDirec = Vector2.ClampMagnitude(new Vector2(h, v), 1);
        transform.position += new Vector3(moveDirec.x, MoveDirec.y) * moveSpeed * Time.deltaTime;
    }
    public void Damage(int damage)
    {
        if (isDead) return;
        health -= damage;
        if (health > 0) return;
        foreach (var weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }
        StopAllCoroutines();
        isDead = true;
        ingameController.GameFinish();
        source.PlayOneShot(deadSE);
        deathParticle.SetActive(true);
    }


    IEnumerator ChangeWeapon()
    {
        while (true)
        {
            if (!isPlaying | isDead) break;

            int curIndex = Mathf.Min(CalcWeapon(), weapons.Length - 1);

            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].InitWeapon();
                weapons[i].gameObject.SetActive(i == curIndex);
            }

            yield return new WaitForSeconds(10);
        }
    }
    int CalcWeapon()
    {
        return (int)(Time.time - startTime) / 10;
    }
    void StopPlayer()
    {
        isPlaying = false;
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].StopWeapon();
        }
    }
}
