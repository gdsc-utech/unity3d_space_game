using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // public variables are assignable through the Unity Editor
    public float speed;
    public float turnSpeed;
    public TextMeshProUGUI scoreText;
    public GameObject bullet;
    public Transform[] bulletSpawnPoints;
    float score = 0;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // GetComponent: get a component attached to this object
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // make jet move
        float pitch = Input.GetAxis("Vertical");
        float roll = Input.GetAxis("Horizontal");
        float yaw = 0;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            yaw -= 1f;
        }
        else if (Input.GetKey(KeyCode.RightShift))
        {
            yaw += 1f;
        }

        // move the plane forward with a constant speed
        transform.Translate(0, 0, speed * Time.deltaTime);
        // Time.deltaTime is used to smooth movement in sync with time

        // rotate the plane
        transform.Rotate(
            pitch * turnSpeed * Time.deltaTime,
            yaw * turnSpeed * Time.deltaTime,
            -roll * turnSpeed * Time.deltaTime
        );

        // shooting
        if (Input.GetKey(KeyCode.Space))
        {
            foreach(var point in bulletSpawnPoints)
            {
                // spawn the bullet
                GameObject a = Instantiate(bullet, point.position, point.rotation);
                // push the bullet forward
                a.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 60, ForceMode.Impulse);
                // remove it after 2 seconds
                Destroy(a, 2);
            }
        }
    }

    // Called when this object collides with a trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // check what we hit
        if(other.gameObject.tag == "Ring")
        {
            // update our score
            score++;
            scoreText.text = "Score: " + score;
            audioSource.Play();
        }
    }
}
