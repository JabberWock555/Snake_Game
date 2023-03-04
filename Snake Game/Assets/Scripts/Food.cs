
using System.Collections;
using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D Boundry;
    public GameObject ApplePrefab;
    public GameObject SkullPrefab;

    private GameObject Apple;
    private GameObject Skull;
    private float timer;
    private bool snakeSizeLow = true;
    private void Start()
    {
        StartCoroutine(SpawnFood(ApplePrefab, Random.Range(0, 4f)));
        StartCoroutine(SpawnFood(SkullPrefab, 5f));

    }
    private Vector2 RandomPosition()
    {
        Bounds bounds = Boundry.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector2(Mathf.Round(x), Mathf.Round(y));
    }

    public void AppleEaten(int SnakeSize)
    {
        //Apple.SetActive(false);
        Destroy(Apple);
        snakeSizeLow = SnakeSize < 3;
        StartCoroutine(SpawnFood(ApplePrefab, Random.Range(0, 4f)));
        ApplePrefab = Apple;
    }

    public void SkullEaten()
    {
        Destroy(Skull);
        //Skull.SetActive(false);
        StartCoroutine(SpawnFood(SkullPrefab, Random.Range(5f, 10f)));
        SkullPrefab = Skull;
    }


    private IEnumerator SpawnFood(GameObject food, float time)
    {
        yield return new WaitForSeconds(time);
        GameObject Food = Instantiate(food);
        Food.transform.position = new Vector3(RandomPosition().x, RandomPosition().y, 0f);
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (!snakeSizeLow && timer >= 10f && Skull.activeSelf)
        {
            SkullEaten();
            timer = 0;
        }
    }
}
