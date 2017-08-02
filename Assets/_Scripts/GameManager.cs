using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;

    [SerializeField] private Text moneyText;

    [SerializeField] private GameObject wavePanel;
    [SerializeField] private Text waveText;
    
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private Text messageText;
    
    [SerializeField] private Text actionText;
    [SerializeField] private GameObject actionPanel;

    private bool isMoneyIncreased;

    private TrapsManager _trapsManager;

    public static Relic relic;

    public enum TrapsType { SPIKES = 5, TURRET = 10 }
    public enum TrapsDamage { SPIKES = 1, TURRET = 2 }

    public static State currentState;
    public enum State { TUTORIAL, BUILDING, READY, FIGHTING, LOST }

    public static int currentWave { get; set; }
    public static int numOfEnemiesInWave { get; set; }
    public static int numOfEnemiesInDoor { get; set; }
    public static int currentMoney { get; set; }

    private void Awake()
    {
        relic = FindObjectOfType<Relic>();
        _trapsManager = FindObjectOfType<TrapsManager>();

        currentWave = 0;
        numOfEnemiesInWave = 1;
        numOfEnemiesInDoor = 0;
        currentMoney = 40;

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
                messageText.text =  "Click on the highlighed areas to build traps. \n" +
                                    "Traps cost money. You can get money by completing waves \n" +
                                    "but this time I'm giving you some to place the first traps.";
            }
            else if (currentState == State.BUILDING && !isMoneyIncreased)
            {
                isMoneyIncreased = true;
                currentMoney += (currentWave * 10);

                messageText.text =  "Wave completed! \n" +
                                    "Place some traps";
                
            }
            actionPanel.SetActive(true);
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
            actionPanel.SetActive(false);
            messagePanel.SetActive(false);
            _trapsManager.isTrapHighlighted = false;
            isMoneyIncreased = false;
        }

        if (currentState == State.LOST)
        {
            Time.timeScale = 0;
            messageText.text = "You lost. Waves completed: " + (currentWave - 1);

            actionText.text = "RESTART";

            actionPanel.SetActive(true);
            messagePanel.SetActive(true);
            wavePanel.SetActive(false);

            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(0);
            }
        }
    }

    private void OnGUI()
    {
        Vector3 mousePosition = Input.mousePosition;
        Rect cursorPosition = new Rect(mousePosition.x - 32f, Screen.height - mousePosition.y - 32f, 16f, 16f);
        GUI.DrawTexture(cursorPosition, cursorTexture);
    }

    public static void NextWave()
    {
        currentWave++;
        numOfEnemiesInWave += currentWave;
        currentState = State.READY;
    }

    public static int GetTrapPrice(TrapsType trap)
    {
        return (int)trap;
    }
}
