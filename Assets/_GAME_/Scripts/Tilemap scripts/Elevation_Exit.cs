using UnityEngine;

public class Elevation_Exit : MonoBehaviour
{
    public Collider2D[] collisionObjects;
    public Collider2D[] boundaryObjects;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (Collider2D Collider in collisionObjects)
            {
                Collider.enabled = true;
            }
            foreach (Collider2D boundary in boundaryObjects)
            {
                boundary.enabled = false;
            }
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
    }
}
