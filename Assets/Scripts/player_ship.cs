using UnityEngine;

public class player_ship : MonoBehaviour
{
    public int armor;
    public int acceleration;
    public int maxVelocity;

    public float rotation;
    public Rigidbody2D player;

    // Use this for initialization
    void Start()
    {

    }

    void Awake()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        bool isVerticalPressed = Input.GetButton("Vertical");

        transform.Rotate(new Vector3(0, 0, horizontal * Time.deltaTime * rotation));

        if (isVerticalPressed)
        {
            float accelCoefficient = 1.0f - (player.velocity.magnitude / maxVelocity);
            player.AddForce(transform.up * Time.deltaTime * acceleration * accelCoefficient);
        }
    }

}
