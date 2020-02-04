using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterControl : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;

    public Animator animator;
    public Rigidbody2D playerRB;

    public Image filler;
   
    public float counter;
    public float maxCounter;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        GameStatus.status.previousHealth = GameStatus.status.health;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);

        //Joko a tai d on pohjassa, muutetaan hahmon x scaalaa inputin mukaan
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("Jump");
            playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
        //Healthkoodit
        if(counter > maxCounter)
        {
            GameStatus.status.previousHealth = GameStatus.status.health;
            counter = 0;
        }
        else
        {
            counter += Time.deltaTime;
        }

        filler.fillAmount = Mathf.Lerp(GameStatus.status.previousHealth / GameStatus.status.maxHealth, GameStatus.status.health / GameStatus.status.maxHealth, counter / maxCounter);

    }
    //Ansaan osuminen
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            
            TakeDamage(20);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LevelEnd"))
        {
            SceneManager.LoadScene("MapLevel");

        }
    }

    void TakeDamage(float dmg)
    {
        GameStatus.status.previousHealth = filler.fillAmount * GameStatus.status.maxHealth;
        counter = 0;
        GameStatus.status.health -= dmg;
    }

}

/*
-Tee kerättävä sydän joka antaa +20 health
-Tee violetti sydän joka antaa +10 max health
-Korjaa hyppy ja ettei voi hypätä ilmassa
-Painamalla F näppäintä, pelaajan eteen instansioidaan nuotio. Jos pelaaja on kahden yksikön päässä nuotiosta, health kasvaa hitaasti ylöspäin.
-Jos painetaan hiiren nappulaa, pelaajasta instansioidaan kirves yläilmoihi. Kirves pyörii itsensä ympäri. Jos kirveen terä osuu puuhun, jää se siihen kiinni.
Pelaaja voi tämän jälkeen hypätä kirveen päälle.
*/
