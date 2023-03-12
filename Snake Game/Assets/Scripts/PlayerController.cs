using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float TimetoMove = 0.2f;
    public Food food;
    public GameObject Segment;
    [SerializeField]
    public List<GameObject> segments;

    [HideInInspector]
    public bool ShieldUp = false;
    [HideInInspector]
    public int foodPoints = 10;
    public static bool MultiPlayer;

    private bool IsMoving = false;
    private Vector3 orignalPos, TargetPos;
    private Vector3 direction = Vector2.up;
    private Vector3 SpriteRotate = new Vector3(0f, 0f, 0f);
    private Vector3 BodyOffset;
    public float horizontal;
    public float vertical;

    private void Awake()
    {
        transform.SetPositionAndRotation(new Vector3(direction.x, direction.y, 0f), Quaternion.Euler(SpriteRotate.x, SpriteRotate.y, SpriteRotate.z));
    }

    private void Start()
    {
        segments = new List<GameObject>();
        segments.Add(gameObject);
        segments.Add(Segment);
    }

    private void Update()
    {
        if (!MultiPlayer)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }
        else
        {
            PlayerInput();
        }
        if (!IsMoving)
        {
            float x = 0.05f;
            if (horizontal > 0 && direction != Vector3.left)
            {
                direction = Vector3.right;
                transform.rotation = Quaternion.Euler(SpriteRotate.x, SpriteRotate.y, SpriteRotate.z - 90f);
                BodyOffset = new Vector3(-x, 0, 0);
            }
            else if (horizontal < 0 && direction != Vector3.right)
            {
                direction = Vector3.left;
                transform.rotation = Quaternion.Euler(SpriteRotate.x, SpriteRotate.y, SpriteRotate.z + 90);
                BodyOffset = new Vector3(x, 0, 0);
            }

            if (vertical > 0 && direction != Vector3.down)
            {
                direction = Vector3.up;
                transform.rotation = Quaternion.Euler(SpriteRotate.x, SpriteRotate.y, SpriteRotate.z);
                BodyOffset = new Vector3(0, -x, 0);
            }
            else if (vertical < 0 && direction != Vector3.up)
            {
                direction = Vector3.down;
                transform.rotation = Quaternion.Euler(SpriteRotate.x, SpriteRotate.y, SpriteRotate.z + 180);
                BodyOffset = new Vector3(0, x, 0);
            }

            StartCoroutine(Snake_Move(direction));
            for (int i = (segments.Count - 1); i > 0; i--)
            {
                segments[i].transform.SetPositionAndRotation(segments[i - 1].transform.localPosition + BodyOffset, segments[i - 1].transform.rotation);
            }
        }
    }

    public virtual void PlayerInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    } 

    private IEnumerator Snake_Move(Vector3 Direction)
    {
        float timePassed = 0f;
        IsMoving = true;
        orignalPos = transform.position;
        TargetPos = orignalPos + Direction;

        while (timePassed < TimetoMove)
        {
            transform.position = Vector3.Lerp(orignalPos, TargetPos, (timePassed / TimetoMove));
            timePassed += Time.deltaTime;
            yield return null;
        }
        transform.position = TargetPos;
        IsMoving = false;
    }

    public void Grow()
    {
        GameObject NewSegment = Instantiate(Segment);
        NewSegment.transform.SetParent(Segment.transform.parent, false);
        segments.Add(NewSegment);
    }



}
