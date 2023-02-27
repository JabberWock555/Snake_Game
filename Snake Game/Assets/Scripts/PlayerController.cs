using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public GameObject Segment;

    private Vector2Int direction = Vector2Int.up;
    private Vector3 SpriteRotate = new Vector3(0f, 0f, 0f);
    private float horizontal;
    private float vertical;
    private List<GameObject> segments;

    private void Awake()
    {
        transform.position = new Vector3(direction.x, direction.y, 0f);
        transform.rotation = new Quaternion(SpriteRotate.x, SpriteRotate.y, SpriteRotate.z, 0f);
    }

  
    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        Movement(horizontal, vertical);
    }

    private void Movement(float Horizontal, float Vertical)
    {

        if (Horizontal > 0)
        {
            direction = Vector2Int.right;
            transform.rotation = Quaternion.Euler(SpriteRotate.x, SpriteRotate.y, SpriteRotate.z - 90f);
        }
        else if (Horizontal < 0)
        {
            direction = Vector2Int.left;
            transform.rotation = Quaternion.Euler(SpriteRotate.x, SpriteRotate.y, SpriteRotate.z + 90);
        }

        if (Vertical > 0)
        {
            direction = Vector2Int.up;
            transform.rotation = Quaternion.Euler(SpriteRotate.x, SpriteRotate.y, SpriteRotate.z);
        }
        else if(Vertical < 0)
        {
            direction = Vector2Int.down;
            transform.rotation = Quaternion.Euler(SpriteRotate.x, SpriteRotate.y, SpriteRotate.z + 180);
        }
        Vector2 MoveDistance = new Vector2(direction.x *Time.deltaTime * Speed, direction.y * Time.deltaTime * Speed);
        transform.position = new Vector3(transform.position.x + MoveDistance.x, transform.position.y + MoveDistance.y, 0f);


    }

    public void FoodEaten()
    {
        Debug.Log("Snake ate the egg");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            this.enabled = false;
        }
    }
}
