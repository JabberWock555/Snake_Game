using System.Collections;
using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D Boundry;
    public GameObject Apple;
    public GameObject Skull;
    public int snakeSize;

    private float timer;

    private void Start()
    {
        StartCoroutine(SpawnFood(Apple, Random.Range(0, 4f)));
        StartCoroutine(SpawnFood(Skull, 5f));

    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (snakeSize < 3 && timer >= 10f && Skull.activeSelf)
        {
            SkullEaten();
            timer = 0;
        }
    }

    public void AppleEaten(int SnakeSize)
    {
        Apple.SetActive(false);
        snakeSize= SnakeSize;
        StartCoroutine(SpawnFood(Apple, Random.Range(0, 4f)));
    }

    public void SkullEaten()
    {
        Skull.SetActive(false);
        StartCoroutine(SpawnFood(Skull, Random.Range(5f, 10f)));
    }

    //----------------------------
    private Vector2 RandomPosition()
    {
        Bounds bounds = Boundry.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y + 1f, bounds.max.y - 1f);

        return new Vector2(Mathf.Round(x), Mathf.Round(y));
    }

    public IEnumerator SpawnFood(GameObject food, float time)
    {
        yield return new WaitForSeconds(time);
        food.SetActive(true);
        food.transform.position = new Vector3(RandomPosition().x, RandomPosition().y, 0f);
    }
    
}
