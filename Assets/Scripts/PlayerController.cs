using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRb;
    public float speed = 3.0f;

    private GameObject focalPoint;
    public bool hasPowerUp;
    private float powerUpStrength = 15.0f;
    public GameObject powerUpIndicator;

    //Sound Effects
    private AudioSource playerAudio;
    public AudioClip powerUpSound;
    public AudioClip crashingSound;

    //Visual Effects
    public ParticleSystem powerUpEffect;





    // Start is called before the first frame update
    void Start()
    {
        powerUpEffect.Stop();
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovementController();

        powerUpIndicator.gameObject.transform.position = transform.position - new Vector3(0, 0.5f, 0);
        powerUpIndicator.gameObject.SetActive(hasPowerUp);
        
    }

    void PlayerMovementController(){
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed);

        float horizontalInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(focalPoint.transform.right * horizontalInput * speed);
    }

    private void OnTriggerEnter(Collider other) {
        
        if (other.CompareTag("PowerUp") && !hasPowerUp)
        {
            hasPowerUp = true;
            powerUpEffect.Play();
            playerAudio.PlayOneShot(powerUpSound, 1.0f);
            Destroy(other.gameObject);
            StartCoroutine(PowerCountDownRoutine());
        }
    }

    IEnumerator PowerCountDownRoutine(){
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
    }

    private void OnCollisionEnter(Collision other) {

        if(other.gameObject.CompareTag("Enemy") ){
            
            playerAudio.PlayOneShot(crashingSound, 1.0f);
            if(hasPowerUp){
                Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;
                enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
            }
            

        }
    }

}
