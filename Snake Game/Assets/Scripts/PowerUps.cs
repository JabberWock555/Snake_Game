using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType { Egg, Potion, Meat }

public class PowerUps : MonoBehaviour
{
    [SerializeField] private List<GameObject> Prefabs;
    [SerializeField] private Food Spwan;
    [SerializeField] private PlayerController Player;
    [SerializeField] private List<GameObject> PowerItems;

    private bool Spawned = false;
    private bool Eaten = false;
    private int PowerNo = 0;
    private void Start()
    {
        PowerItems = new List<GameObject>();
        LoadPowerUps();
    }
    private void Update()
    {
        if(Spwan.snakeSize > 4 && !Spawned)
        {
            Spawned = true;
            StartCoroutine(Spwan.SpawnFood(PowerItems[PowerNo], Random.Range(10f, 20f)));
        }

        if (PowerItems.Count < 1)
        {
            LoadPowerUps();
        }
    }

    private void LoadPowerUps()
    {
        for (int i =0; i<=5; i++)
        {
            GameObject power = Instantiate(Prefabs[Random.Range(0, Prefabs.Count)]);
            power.SetActive(false);
            power.transform.SetParent(transform);
            PowerItems.Add(power);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>() != null)
        {

            SoundManager.Instance.Play(SoundEvents.EatPowerup);
            Eaten = true;
            switch (PowerItems[0].tag)
            {
                case "Egg":
                    StartCoroutine(powerUps((int)PowerUpType.Egg));
                    break;

                case "Potion":
                    StartCoroutine(powerUps((int)PowerUpType.Potion));
                    break;

                case "Meat":
                    StartCoroutine(powerUps((int)PowerUpType.Meat));
                    break;
            }
            StartCoroutine(DestroyPower());

        }
    }

    private IEnumerator powerUps(int type)
    {
        GameUIManager.Power = type;
        switch (type)
        {
             
            case 0:
               
                Player.ShieldUp = true;
                yield return new WaitForSeconds(10f);
                Player.ShieldUp = false;
                Spawned = false;
                break;

            case 1:
                Player.TimetoMove = 0.12f;
                yield return new WaitForSeconds(15f);
                Player.TimetoMove = 0.2f;
                Spawned = false;
                break;

            case 2:
                Player.foodPoints = 20;
                yield return new WaitForSeconds(15f);
                Player.foodPoints = 10;
                Spawned = false;
                break;

            default:
                break;
        }
        GameUIManager.Power = 4;
        Eaten = false;
    }

    private IEnumerator DestroyPower() 
    {
        if (!Eaten)
        {
            yield return new WaitForSeconds(10f);
            Destroy(PowerItems[PowerNo]);
            PowerItems.RemoveAt(PowerNo);
        }
        else
        {
            Destroy(PowerItems[PowerNo]);
            PowerItems.RemoveAt(PowerNo);
            yield return null;
        }
    }
}
