using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static State currentState;
    public enum State { TUTORIAL, BUILDING, READY, FIGHTING }

    public static int currentWave { get; set; }
    public static int numOfEnemiesInWave { get; set; }
    public static int numOfEnemiesInDoor { get; set; }
    public static int currentMoney { get; set; }

    private void Awake()
    {
        currentWave = 0;
        numOfEnemiesInWave = 3;
        numOfEnemiesInDoor = 0;
        currentMoney = 0;

        //currentState = State.TUTORIAL;
        currentState = State.BUILDING;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentState = State.READY;
        }
    }

    public static void NextWave()
    {
        currentWave++;
        numOfEnemiesInWave += currentWave;
        currentMoney += (currentWave * 10);
        currentState = State.READY;
    }
}
