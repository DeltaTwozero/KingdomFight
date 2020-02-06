using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    //Character movement
    [SerializeField]
    float speed, sprintSpeed;

    //Movement variables
    private float moveForwards;
    private float moveSideways;
    public bool isAlive;

    //Character Physics
    [SerializeField] Rigidbody rb;
    [SerializeField] float jumpforce;
    bool canTakeDamage;

    public GameObject inventoryHand;
    CameraControls cameraGO;
    GroundCheck groundCheck;

    //Character Health
    [SerializeField] float maxHP;
    float currentHP;
    [SerializeField] float hpRegenTimerReset, hpRegenRate;
    float hpRegenTimerCurrent;

    //Character Animation
    [SerializeField] Animator anim;

    //Character Audio
    AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] AudioClip[] takeDamageClips;
    bool playOnce;

    //Character UI
    [SerializeField] Image imageTeleport, imageDeath;
    Color teleportFade, deathFade;
    float teleportFadeFloat, deathFadeFloat;
    public bool teleportFadeBool;

    // Start is called before the first frame update
    void Start()
    {
        //Health
        canTakeDamage = true;
        currentHP = maxHP;

        //Movement
        cameraGO = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControls>();
        groundCheck = GameObject.Find("GroundCheck").GetComponent<GroundCheck>();

        //Animation
        //anim = GameObject.Find("aj@Idle").GetComponent<Animator>();
        
        //Audio
        audioSource = this.GetComponent<AudioSource>();
        playOnce = true;
        isAlive = true;

        //UI
        imageTeleport = GameObject.Find("teleportUI").GetComponent<Image>(); //getting image
        imageDeath = GameObject.Find("deathUI").GetComponent<Image>();

        teleportFade = imageTeleport.color; //getting color from image
        deathFade = imageDeath.color;

        teleportFadeFloat = 1f; //creating alpha channel (0-1)
        deathFadeFloat = 0f;

        teleportFade.a = teleportFadeFloat; //setting alpha channel for the color
        deathFade.a = deathFadeFloat;

        imageTeleport.color = teleportFade; //setting color to image
        imageDeath.color = deathFade;

        teleportFadeBool = false; //setting bool to change alpha for teleport image

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveForwards = Input.GetAxis("Vertical") * sprintSpeed * Time.deltaTime;
                moveSideways = Input.GetAxis("Horizontal") * sprintSpeed * Time.deltaTime;
                cameraGO.cameraFOV = 70;
            }
            else
            {
                moveForwards = Input.GetAxis("Vertical") * speed * Time.deltaTime;
                moveSideways = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            }
        }
        

        if (moveForwards != 0 || moveSideways != 0)
        {
            //anim.SetBool("IsWalking", true);
            if (playOnce && groundCheck.isGrounded)
                audioSource.PlayOneShot(audioClips[0]);
            //else
                //audioSource.Stop();

            playOnce = false;
        }
        else
        {
            //anim.SetBool("IsWalking", false);
            audioSource.Stop();
            playOnce = true;
        }

        transform.Translate(moveSideways, 0, moveForwards);

        if (Input.GetAxis("Jump") > 0 && groundCheck.isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpforce, 0));
            audioSource.PlayOneShot(audioClips[1]);
        }

        //Change level white image
        if (teleportFadeBool)
        {
            teleportFadeFloat += .05f;
            if (teleportFadeFloat > 1)
                teleportFadeFloat = 1;

            teleportFade.a = teleportFadeFloat;
            imageTeleport.color = teleportFade;
        }
        else
        {
            teleportFadeFloat -= .01f;
            if (teleportFadeFloat < 0)
                teleportFadeFloat = 0;

            teleportFade.a = teleportFadeFloat;
            imageTeleport.color = teleportFade;
        }

        HealthHandler(hpRegenRate);

        if (isAlive)
        {
            if (currentHP > 50)
            {
                deathFadeFloat = 0f;
            }
            else
            {
                deathFadeFloat = 0.5f;
            }
        }
        else
        {
            deathFadeFloat = 1f;

        }

        //currentHP -= 0.1f;
        //deathFadeFloat = Mathf.Clamp(currentHP, 0, 1);
        //print(deathFadeFloat);

        //deathFade.a = deathFadeFloat;
        //imageDeath.color = Color.Lerp(imageDeath.color, deathFade, deathFadeFloat);
        //imageDeath.color = deathFade;

        //if (currentHP <= 0)
        //    Application.Quit();
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    //public void PickupItem(GameObject obj)
    //{
    //    obj.transform.position = Vector3.MoveTowards(obj.transform.position, inventoryHand.transform.position, 100f * Time.deltaTime);
    //    inventoryBool[0] = true;
    //    itemInHand = obj;
    //}

    public void TakeDamage(float damage)
    {
        if (canTakeDamage)
        {
            currentHP -= damage;
            StartCoroutine("TakeDamageCoroutine", 0.2f);
            print(currentHP);
            int random = Random.Range(0, takeDamageClips.Length);
            audioSource.PlayOneShot(takeDamageClips[random]);
            hpRegenTimerCurrent = hpRegenTimerReset;
        }
    }

    void HealthHandler(float regenRate)
    {
        hpRegenTimerCurrent -= Time.deltaTime;
        if (hpRegenTimerCurrent < 0)
        {
            currentHP += regenRate * Time.deltaTime;
        }

        if (currentHP > maxHP)
            currentHP = maxHP;
        else if(currentHP <= 0)
        {
            isAlive = false;
            deathFadeFloat = 1;
        }

        print(currentHP);
        deathFade.a = deathFadeFloat;
        imageDeath.color = deathFade; 
    }

    public void WhiteFade()
    {
        teleportFadeFloat = 1f;
    }

    IEnumerator TakeDamageCoroutine(float waitTime)
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(waitTime);
        canTakeDamage = true;
    }
}
