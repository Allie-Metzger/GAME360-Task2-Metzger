using UnityEngine;

namespace GAME360Project.Assets.Scripts.Shooter
{

    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed = 5f;

        [Header("Shooting")]
        public GameObject bulletPrefab, bulletPrefab1, bulletPrefab2;
        public Transform firePoint;
        public float fireRate = 0.5f;
        private float nextFireTime = 0f;

        [Header("Audio")]
        public AudioClip shootSound; //this is where you put you mp3/wav files
        public AudioClip CoinSound;
        private AudioSource audioSource; //Unity component

        private Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            // Get or add AudioSource component
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            // Configure AudioSource for sound effects
            audioSource.playOnAwake = false;
            audioSource.volume = 0.7f; // Adjust volume as needed

        }

        private void Update()
        {
            HandleMovement();
            HandleShooting();
        }

        private void HandleMovement()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector2 movement = new Vector2(horizontal, vertical).normalized;
            rb.linearVelocity = movement * moveSpeed;
        }

        private void HandleShooting()
        {

            if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
            {
                FireBullet();
                nextFireTime = Time.time + fireRate;
            }

        }

        private void FireBullet()
        {
            if (ShooterManager.Instance.score > 100 && ShooterManager.Instance.score < 300)
            {
                fireRate = 0.3f;
                bulletPrefab = bulletPrefab1;

            }

            if (ShooterManager.Instance.score > 300)
            {
                fireRate = 0.1f;
                bulletPrefab = bulletPrefab2;
            }

            if (bulletPrefab && firePoint)
            {
            
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
           
            }

            // Play shoot sound effect
            audioSource.PlayOneShot(shootSound);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                // Player hit by enemy - lose a life
                ShooterManager.Instance.LoseLife();
            }

            if (other.CompareTag("Collectible"))
            {
                // Player collected an item
                Collectible collectible = other.GetComponent<Collectible>();
                if (collectible)
                {
                    audioSource.PlayOneShot(CoinSound);
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
