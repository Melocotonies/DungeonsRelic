using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public GameObject relic;

    [SerializeField] private GameObject[] doors;
    private List<GameObject> availableDoors = new List<GameObject>();
    private bool[] isDoorFull;
    private int[] currentEnemiesInDoor;

    private GameObject[] enemiesPrefabs;
    public int enemiesKilledInWave { private get; set; }

    private void Awake()
    {
        enemiesPrefabs = Resources.LoadAll<GameObject>("Prefabs/Enemies");
        enemiesKilledInWave = 0;
    }

    private void Update()
    {
        if (GameManager.currentState == GameManager.State.READY)
        {
            GameManager.currentState = GameManager.State.FIGHTING;
            SetEnemies();
        }

        if(GameManager.currentState == GameManager.State.FIGHTING && enemiesKilledInWave == GameManager.numOfEnemiesInWave)
        {
            GameManager.currentState = GameManager.State.BUILDING;
        }
    }

    public void SetEnemies()
    {
        switch (GameManager.currentWave)
        {
            case 0:
                availableDoors.Add(doors[0]);
                break;
            case 5:
                availableDoors.Add(doors[1]);
                GameManager.numOfEnemiesInDoor = GameManager.numOfEnemiesInWave / 2;
                break;
            case 10:
                availableDoors.Add(doors[2]);
                GameManager.numOfEnemiesInDoor = GameManager.numOfEnemiesInWave / 3;
                break;
            case 15:
                availableDoors.Add(doors[3]);
                GameManager.numOfEnemiesInDoor = GameManager.numOfEnemiesInWave / 4;
                break;
        }



        //Debug.Log("Wave: " + GameManager.currentWave);
        //Debug.Log("numOfEnemiesInWave: " + GameManager.numOfEnemiesInWave);
        //Debug.Log("numOfEnemiesInDoor: " + GameManager.numOfEnemiesInDoor);
        //Debug.Log("Money: " + GameManager.currentMoney);


        Vector3 enemyPosition = Vector3.zero;
        Enemy[] enemies = transform.GetComponentsInChildren<Enemy>();
        for (int i = 0; i < GameManager.numOfEnemiesInWave - enemies.Length; i++)
        {
            GameObject newEnemy = Instantiate(enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)], transform);
            if(i < 5)
            {
                enemyPosition = availableDoors[0].transform.position;
            }
            else if(i < 10)
            {
                if (!isDoorFull[0])
                {
                    enemyPosition = availableDoors[0].transform.position;
                    currentEnemiesInDoor[0]++;
                    if (currentEnemiesInDoor[0] == GameManager.numOfEnemiesInDoor) isDoorFull[0] = true;
                }
                else
                {
                    enemyPosition = availableDoors[1].transform.position;
                }
            }
            else if(i < 15)
            {
                if (!isDoorFull[0])
                {
                    enemyPosition = availableDoors[0].transform.position;
                    currentEnemiesInDoor[0]++;
                    if (currentEnemiesInDoor[0] == GameManager.numOfEnemiesInDoor) isDoorFull[0] = true;
                }
                else if (!isDoorFull[1])
                {
                    enemyPosition = availableDoors[1].transform.position;
                    currentEnemiesInDoor[1]++;
                    if (currentEnemiesInDoor[1] == GameManager.numOfEnemiesInDoor) isDoorFull[1] = true;
                }
                else
                {
                    enemyPosition = availableDoors[2].transform.position;
                }
            }
            else
            {
                if (!isDoorFull[0])
                {
                    enemyPosition = availableDoors[0].transform.position;
                    currentEnemiesInDoor[0]++;
                    if (currentEnemiesInDoor[0] == GameManager.numOfEnemiesInDoor) isDoorFull[0] = true;
                }
                else if (!isDoorFull[1])
                {
                    enemyPosition = availableDoors[1].transform.position;
                    currentEnemiesInDoor[1]++;
                    if (currentEnemiesInDoor[1] == GameManager.numOfEnemiesInDoor) isDoorFull[1] = true;
                }
                else if (!isDoorFull[2])
                {
                    enemyPosition = availableDoors[2].transform.position;
                    currentEnemiesInDoor[2]++;
                    if (currentEnemiesInDoor[2] == GameManager.numOfEnemiesInDoor) isDoorFull[2] = true;
                }
                else
                {
                    enemyPosition = availableDoors[3].transform.position;
                }
            }
            
            newEnemy.transform.position = enemyPosition;            
        }

    }
}
