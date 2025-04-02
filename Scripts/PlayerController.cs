using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // player speed
    public float speed;
    // snapping distance to the ground
    public float groundDist;
    // terrain layer
    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        terrainLayer = LayerMask.NameToLayer("Terrain");
    }

    
    void Update()
    {
        // looks for raycast downwards looking for terrain layer. 
        // If it hits, should put player right above the layer
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer)) {
            if (hit.collider != null) {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }

        //uses rigidbody to move player
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(x, 0, y);
        rb.linearVelocity = moveDir * speed;

        // flips the sprite
        if (x != 0 && x < 0) {
            sr.flipX = true;
        } else if (x != 0 && x > 0) {
            sr.flipX = false;
        }
    }
}
