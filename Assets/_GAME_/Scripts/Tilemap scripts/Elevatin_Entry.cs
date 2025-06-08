using UnityEngine;

public class Entry : MonoBehaviour
{
    public Collider2D[] collisionObjects;
    public Collider2D[] boundaryObjects;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (Collider2D Collider in collisionObjects)
            {
                Collider.enabled = false;
            }
            foreach (Collider2D boundary in boundaryObjects)
            {
                boundary.enabled = true;
            }
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }    
    }
}
