using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class BananaBoy : MonoBehaviour
{
    //Object of our player which we will use to calculate the distance between the player and enemy
    public GameObject player;
   
    //Used to see how far away the enemy is from the player 
    private Vector3 distance;

    public PlayerMovement playerMovement;

    private float pushDistance = 50f;
    private bool isScaring = false;
    [HideInInspector]public bool jumpScared = false;
    private float liftHeight = 3f;
    public float attackSpeed = 2f;
    [HideInInspector] public float normalSpeed;

    public TextMeshProUGUI gameOverText;

    public GameObject gameOverScrene;

    public Sanity sanity;

    public NavMeshAgent agent;

    private AudioSource audioSource;
    public AudioSource GameOverAudioSource;
    public AudioClip jumpScareSound;
    public AudioClip attackSound;
    public AudioClip drama;

    public Light jumpScareLight;

    private Animator animator;

    public Victory_Gameover vg;

    public TextMeshProUGUI speedText;

    public void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        normalSpeed = agent.speed;
        attackSpeed = agent.speed + 1;
    }

    private void Update()
    {
        Debug.Log("Banana Boy's Speed: " + agent.speed);
        speedText.text = "Banana Boy's speed: " + agent.speed;
    }


    void LateUpdate()
    { 
        
        if(!isScaring)
        {
            //Finding the distance vector between the player and Banana Boy
            distance = player.transform.position - transform.position;

            if (distance.magnitude < 50f && distance.magnitude > 5.5f && !jumpScared)
            {
                jumpScareLight.gameObject.SetActive(false);

                if (sanity.sanity > 0f)
                {
                    sanity.sanity -= 1f * Time.deltaTime; 
                    sanity.updateSanity();
                }

                agent.speed = attackSpeed;
                agent.destination = player.transform.position;

            }
            else if (distance.magnitude < 5.5f && !jumpScared)
            {
                StartCoroutine(JumpScare());
            }
            else if(!jumpScared)
            {
                jumpScareLight.gameObject.SetActive(false);
                agent.speed = normalSpeed;
                agent.destination = player.transform.position;
                GetComponent<Animator>().Play("Armature|Real Running");
      
            }
           

        }
        
      

    }

    private IEnumerator JumpScare()
    {
        isScaring = true;

        agent.isStopped = true;

        Vector3 playerInitialPosition = player.transform.position;
        Quaternion playerInitialRotation = player.transform.rotation;

        player.transform.LookAt(transform.position);
        Vector3 playerPosition = player.transform.position;
        playerPosition.y += liftHeight;
        playerPosition.z -= 0.5f;
        player.transform.position = playerPosition;
        player.GetComponent<PlayerMovement>().enabled = false;

        audioSource.clip = jumpScareSound;

        jumpScareLight.gameObject.SetActive(true);

        GetComponent<Animator>().Play("Armature|Jump Scare");

        audioSource.Play();

        yield return new WaitForSeconds(2f);

        audioSource.Stop();

        jumpScared = true;

        vg.GameOver();
        GameOverAudioSource.clip = drama;
        GameOverAudioSource.loop = false;
        GameOverAudioSource.Play();

        yield return new WaitForSeconds(1f);

        agent.isStopped = false;
        isScaring = false;


    }
    
    private void playAttackSoundEffect()
    {
        audioSource.clip = attackSound;
        audioSource.Play();
    }

   
   
}
