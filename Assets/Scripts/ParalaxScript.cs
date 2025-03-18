using UnityEngine;

public class ParalaxScript : MonoBehaviour
{
    [Header ("Parallax Settings")]
    private Transform player;
    public float tileWidth;
    public float tileHeight;
    private Vector3 lastPlayerPos;

    [Header("Parallax Speed")]
    public float parallaxMovementX;
    public float parallaxMovementY;

    void Start()
    {
        InitialConfig();
    }

    void Update()
    {
        ParallaxBehaviour();
    }

    void InitialConfig()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform; //cuesta muchos recursos
        }

        lastPlayerPos = player.position;

        SpriteRenderer spriteSettings = GetComponent<SpriteRenderer>();

        tileWidth = spriteSettings.bounds.size.x;
        tileHeight = spriteSettings.bounds.size.y;
    }

    void ParallaxBehaviour()
    {
        Vector3 movement = player.position - lastPlayerPos;
        transform.position += new Vector3(movement.x * parallaxMovementX, movement.y * parallaxMovementY, 0f);

        if (transform.position.x > player.position.x + tileWidth)
        {
            transform.position = new Vector3 (player.position.x -  tileWidth, player.position.y, transform.position.z);
        }
        else if(transform.position.x < player.position.x - tileWidth)
        {
            transform.position = new Vector3(player.position.x + tileWidth, player.position.y, transform.position.z);
        }
        lastPlayerPos = player.position;
    }
}
