﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private GameObject[] doors;
    private List<GameObject> availableDoors = new List<GameObject>();
    private int[] currentEnemiesInDoor = new int[3];

    private bool areDoorsUpdated;

    private GameObject[] enemiesPrefabs;

    private Vector3 enemyPosition;

    private void Awake()
    {
        enemiesPrefabs = Resources.LoadAll<GameObject>("Enemies");
    }

    private void Update()
    {
        if((GameManager.currentState == GameManager.State.BUILDING || GameManager.currentState == GameManager.State.TUTORIAL) && !areDoorsUpdated)
        {
            areDoorsUpdated = true;

            UpdateAvailableDoors();

            foreach (GameObject door in availableDoors)
            {
                door.GetComponentInParent<Door>().ShowWaveCanvas();
            }
        }

        if (GameManager.currentState == GameManager.State.READY)
        {
            GameManager.currentState = GameManager.State.FIGHTING;
            areDoorsUpdated = false;
            SetEnemies();
        }

        if(GameManager.currentState == GameManager.State.FIGHTING && transform.childCount == 0)
        {
            GameManager.currentState = GameManager.State.BUILDING;
        }
    }

    private void UpdateAvailableDoors()
    {
        currentEnemiesInDoor[0] = 0;
        currentEnemiesInDoor[1] = 0;
        currentEnemiesInDoor[2] = 0;

        int currentWave = GameManager.currentWave;
        if (currentWave == 0)
        {
            availableDoors.Add(doors[0]);
        }
        else if (currentWave >= 2 && currentWave < 6)
        {
            if (currentWave == 2) availableDoors.Add(doors[1]);
            GameManager.numOfEnemiesInDoor = GameManager.numOfEnemiesInWave / 2;
        }
        else if (currentWave >= 6 && currentWave < 11)
        {
            if (currentWave == 6) availableDoors.Add(doors[2]);
            GameManager.numOfEnemiesInDoor = GameManager.numOfEnemiesInWave / 3;
        }
        else if (currentWave >= 11)
        {
            if (currentWave == 11) availableDoors.Add(doors[3]);
            GameManager.numOfEnemiesInDoor = GameManager.numOfEnemiesInWave / 4;
        }
    }

    public void SetEnemies()
    {
        for (int i = 0; i < GameManager.numOfEnemiesInWave; i++)
        {
            GameObject newEnemy = Instantiate(enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)], transform);
            SetEnemyPosition(newEnemy);
        }
    }

    private void SetEnemyPosition(GameObject newEnemy)
    {
        if (GameManager.currentWave < 3)
        {
            enemyPosition = availableDoors[0].transform.position;
        }
        else if (GameManager.currentWave < 7)
        {
            if (currentEnemiesInDoor[0] < GameManager.numOfEnemiesInDoor)
            {
                enemyPosition = availableDoors[0].transform.position;
                currentEnemiesInDoor[0]++;
            }
            else
            {
                enemyPosition = availableDoors[1].transform.position;
            }
        }
        else if (GameManager.currentWave < 12)
        {
            if (currentEnemiesInDoor[0] < GameManager.numOfEnemiesInDoor)
            {
                enemyPosition = availableDoors[0].transform.position;
                currentEnemiesInDoor[0]++;
            }
            else if (currentEnemiesInDoor[1] < GameManager.numOfEnemiesInDoor)
            {
                enemyPosition = availableDoors[1].transform.position;
                currentEnemiesInDoor[1]++;
            }
            else
            {
                enemyPosition = availableDoors[2].transform.position;
            }
        }
        else
        {
            if (currentEnemiesInDoor[0] < GameManager.numOfEnemiesInDoor)
            {
                enemyPosition = availableDoors[0].transform.position;
                currentEnemiesInDoor[0]++;
            }
            else if (currentEnemiesInDoor[1] < GameManager.numOfEnemiesInDoor)
            {
                enemyPosition = availableDoors[1].transform.position;
                currentEnemiesInDoor[1]++;
            }
            else if (currentEnemiesInDoor[2] < GameManager.numOfEnemiesInDoor)
            {
                enemyPosition = availableDoors[2].transform.position;
                currentEnemiesInDoor[2]++;
            }
            else
            {
                enemyPosition = availableDoors[3].transform.position;
            }
        }
        newEnemy.transform.position = enemyPosition;
    }
}
