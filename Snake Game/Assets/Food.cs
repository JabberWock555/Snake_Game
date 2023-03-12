
using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D Boundry;

    private void Start()
    {
        RandomPosition();
    }
    private void RandomPosition()
    {
        Bounds bounds = Boundry.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.FoodEaten();
            RandomPosition();
        }
    }
}
