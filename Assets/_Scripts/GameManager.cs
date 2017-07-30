using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text moneyText;

    [SerializeField] private GameObject wavePanel;
    [SerializeField] private Text waveText;
    
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private Text messageText;

    [SerializeField] private GameObject readyPanel;

    public enum TrapsType { SPIKES = 5, TURRET = 10 }
    public enum TrapsDamage { SPIKES = 1, TURRET = 2 }

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
        currentMoney = 10;

        currentState = State.TUTORIAL;
    }

    private void Update()
    {
        waveText.text = currentWave.ToString();
        moneyText.text = currentMoney.ToString();

        if (currentState == State.BUILDING || currentState == State.TUTORIAL)
        {
            if(currentState == State.TUTORIAL)
            {
                messageText.fontSize = 18;
                messageText.text =  "Click on the highlighed areas to build traps." +
                                    "Traps cost money. You can get money by completing waves " +
                                    "but this time I'm giving you some to place the first traps.";
            }
            else
            {
                messageText.fontSize = 20;
                messageText.text = "Wave completed!";
            }
            readyPanel.SetActive(true);
            messagePanel.SetActive(true);
            wavePanel.SetActive(false);            

            if (Input.GetKeyDown(KeyCode.R))
            {
                NextWave();
            }
        }

        if(currentState == State.READY)
        {
            wavePanel.SetActive(true);
            readyPanel.SetActive(false);
            messagePanel.SetActive(false);
        }
    }

    public static void NextWave()
    {
        currentWave++;
        numOfEnemiesInWave += currentWave;
        currentMoney += (currentWave * 10);
        currentState = State.READY;
    }

    public static int getTrapPrice(TrapsType trap)
    {
        return (int)trap;
    }
}
