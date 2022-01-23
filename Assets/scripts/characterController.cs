using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class characterController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    
    public int scoreValue = 0;
    public Text score;
    public Text wintext;
    public Text reset;
    public Text losetext;

    public bool gameOver = false;

    public float speed;

    public float starttime;

    public float gametimer;
    public Text  gTimer;

    Animator anim;

    public GameObject backgroundMusic;
    public GameObject winMusic;
    public GameObject loseMusic;
    public GameObject StartMusic;
    public AudioClip Coinsound;

    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        wintext.text = "";
        reset.text = "";
        losetext.text = "";

        audioSource = GetComponent<AudioSource>();

        anim = GetComponent<Animator>();

        StartMusic.SetActive(false);
        backgroundMusic.SetActive(false);
        winMusic.SetActive(false);
        loseMusic.SetActive(false);


        ///
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        gametimer -= Time.deltaTime;
        starttime -= Time.deltaTime;
        gTimer.text = "" + Mathf.Round(gametimer);

        if (scoreValue == 5)
        {
            speed = 0;
            gametimer = 10;
            gTimer.text = "";
        }

        if (starttime >= 0)
        {

            gametimer = 10;
            gTimer.text = "";
            speed = 0;
            
        }

        if (starttime <= 2)
        {
            StartMusic.SetActive(true);
        }

        if(starttime <=0)
        {
            StartMusic.SetActive(false);
        }
        

        if (gametimer < 10)
        {
            speed = 10;
            backgroundMusic.SetActive(true);
          
        }



        //Lose
        if (gametimer <= 0)
        {
            losetext.text = "you lose!: Press R to restart";
            gameOver = true;
            gTimer.text = "out of time!";
            gametimer = 0;
            speed = 0;
            backgroundMusic.SetActive(false);
            loseMusic.SetActive(true);
        }


        if (scoreValue >= 0)
        {
            score.text = "Score: " + scoreValue.ToString();
        }

        //reset
        if (Input.GetKey(KeyCode.R))
        {
            if (gameOver == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                

            }
        }

        //animation
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "coin")
        {
            Destroy(collision.collider.gameObject);
            scoreValue +=1;
            score.text = scoreValue.ToString();
            SetWinText();
            PlaySound(Coinsound);
        }

    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    void SetWinText()
    {
        if (scoreValue >= 5) 
        {
            wintext.text = "You win! Game created by Jesus Casas ";
            reset.text = " Press R to restart";
            gameOver = true;
            backgroundMusic.SetActive(false);
            winMusic.SetActive(true);
            
        }
    }
}
