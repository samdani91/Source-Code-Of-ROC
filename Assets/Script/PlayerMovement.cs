using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private AudioSource fightAudioSource; 
    [SerializeField] private AudioSource RunAudioSource;

    [SerializeField] private float speed;
    private float jumpLimit = 6f;
    private bool isGrounded;


    private bool isFiring = false;
    private bool isKnifeAttack = false;
    private bool isGasOpen = false;
    private bool canTakeDamageFromHelmetGuy = false; 

    public PlayerHealth playerHealth;
    private TearGasScript tearGasScript;
    private PoliceFIreScript policeFIreScript;

    private Animator anim;


    private HelmetGuy currentHelmetGuy;
    private PoliceFIreScript currentPolice;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        AudioSource[] audioSources = GetComponents<AudioSource>();
        if (audioSources.Length > 1)
        {   
            //RunAudioSource = audioSources[0];
            fightAudioSource = audioSources[1];
        }
        else
        {
            Debug.LogWarning("No second AudioSource found for fight audio.");
        }
    }

    private void Update()
    {

        float moveInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(moveInput * speed, body.velocity.y);

        if (moveInput != 0)
        {
            anim.SetTrigger("run");
            //RunAudioSource.Play();
        }
        else
        {
            //RunAudioSource.Stop();
        }

        if ((Input.GetKeyDown(KeyCode.Space) && isGrounded))
        {
            body.velocity = new Vector2(body.velocity.x, jumpLimit);
            isGrounded = false;
        }


        if (Input.GetKeyDown(KeyCode.F) && currentHelmetGuy != null)
        {
            anim.SetTrigger("attack");

            if (fightAudioSource != null)
            {
                fightAudioSource.Play();
            }

            currentHelmetGuy.TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.F) && currentPolice != null)
        {

            if (currentPolice != null && currentPolice.gameObject != null)
            {
                anim.SetTrigger("attack");
                if (fightAudioSource != null)
                {
                    fightAudioSource.Play();
                }
                currentPolice.TakeDamage(10);  
            }
            else
            {
                currentPolice = null;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("gas"))
        {
            TearGasScript gasScript = collision.gameObject.GetComponent<TearGasScript>();


            if (gasScript != null)
            {
                gasScript.gasOpen();
                playerHealth.TakeDamage(5);
            }
        }

        if (collision.gameObject.CompareTag("helmetGuy"))
        {
            currentHelmetGuy = collision.gameObject.GetComponent<HelmetGuy>();
            isKnifeAttack = true; 
        }

        if (collision.gameObject.CompareTag("Fire"))
        {   
            currentPolice = collision.gameObject.GetComponent<PoliceFIreScript>();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("helmetGuy"))
        {
            currentHelmetGuy = null; 
            isKnifeAttack = false; 
        }
        if (collision.gameObject.CompareTag("Fire"))
        {
            currentPolice = null;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fire") && !isFiring)
        {
            
            if (currentPolice != null && currentPolice.gameObject != null)
            {
                StartCoroutine(FireWithDelay());
            }
            else
            {
                Debug.LogWarning("PoliceFireScript reference is null or destroyed.");
            }
        }

        if (collision.gameObject.CompareTag("helmetGuy") && !canTakeDamageFromHelmetGuy)
        {
            StartCoroutine(HelmetGuyDamage());
        }
    }


    private IEnumerator FireWithDelay()
    {
        isFiring = true;

        if (currentPolice != null && currentPolice.gameObject != null)
        {
            
            currentPolice.startFire();

            playerHealth.TakeDamage(30);
        }
        else
        {
            Debug.LogWarning("Cannot start fire: currentPolice or its game object is null.");
        }

    
        yield return new WaitForSeconds(2f);

        isFiring = false;
    }


    private IEnumerator HelmetGuyDamage()
    {
        canTakeDamageFromHelmetGuy = true; 

        playerHealth.TakeDamage(15);
        TakeDamage(); 

        yield return new WaitForSeconds(1f); 

        canTakeDamageFromHelmetGuy = false; 
    }

    private void TakeDamage()
    {
        anim.SetTrigger("hurt"); 
    }
}
