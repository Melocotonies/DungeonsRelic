using UnityEngine;

public class TrapsManager : MonoBehaviour
{
    public enum TypeOfTrap { SPIKES, TURRET };

    private GameObject spikesTrapPrefab;
    private GameObject turretTrapPrefab;
    private GameObject trapToInstantiate;

    private TrapFloorTile currentTrapFloorTile;
    private TrapFloorTile lastTrapFloorTile;
    private int trapPrice;

    private bool isTrapHighlighted;

    Transform _camera;
    private RaycastHit hit;

    private Renderer[] trapFloorTileRenderers;
    private Color32 emissiveColor;

    private void Awake()
    {
        _camera = Camera.main.transform;

        emissiveColor = new Color32(0, 255, 255, 255);

        spikesTrapPrefab = Resources.Load<GameObject>("Traps/Spikes/SpikesTrap");
        turretTrapPrefab = Resources.Load<GameObject>("Traps/Turret/TurretTrap");
    }

    private void FixedUpdate()
    {
        if (GameManager.currentState != GameManager.State.BUILDING
            && GameManager.currentState != GameManager.State.TUTORIAL)
        {
            ChangeEmissiveColor(currentTrapFloorTile, Color.black);
            ChangeEmissiveColor(lastTrapFloorTile, Color.black);
            return;
        }
        
        if (Physics.Raycast(_camera.position, _camera.forward, out hit, Mathf.Infinity, LayerMask.GetMask("TrapFloor")))
        {
            lastTrapFloorTile = currentTrapFloorTile;
            currentTrapFloorTile = hit.transform.GetComponent<TrapFloorTile>();
            if (!currentTrapFloorTile || currentTrapFloorTile.isTrapPlaced) return;

            //Highlight floortile
            if (lastTrapFloorTile && currentTrapFloorTile != lastTrapFloorTile)
            {
                ChangeEmissiveColor(lastTrapFloorTile, Color.black);
                isTrapHighlighted = false;
            }
            ChangeEmissiveColor(currentTrapFloorTile, emissiveColor);
            isTrapHighlighted = true;

            //Place trap
            if (Input.GetMouseButtonDown(0))
            {
                switch (currentTrapFloorTile.type)
                {
                    case TypeOfTrap.SPIKES:
                        trapToInstantiate = spikesTrapPrefab;
                        trapPrice = GameManager.getTrapPrice(GameManager.TrapsType.SPIKES);
                        break;
                    case TypeOfTrap.TURRET:
                        trapToInstantiate = turretTrapPrefab;
                        trapPrice = GameManager.getTrapPrice(GameManager.TrapsType.TURRET);
                        break;
                }

                if (GameManager.currentMoney >= trapPrice)
                {
                    GameManager.currentMoney -= trapPrice;

                    Vector3 trapPosition = currentTrapFloorTile.transform.position;
                    trapPosition.y += trapToInstantiate.GetComponentInChildren<Renderer>().bounds.size.y * .5f;
                    GameObject newTrap = Instantiate(trapToInstantiate.gameObject, trapPosition, Quaternion.identity);
                    newTrap.transform.parent = transform;
                    currentTrapFloorTile.isTrapPlaced = true;
                }
            }
        }
        else
        {
            ChangeEmissiveColor(currentTrapFloorTile, Color.black);
            ChangeEmissiveColor(lastTrapFloorTile, Color.black);
            isTrapHighlighted = false;
        }
    }

    private void ChangeEmissiveColor(TrapFloorTile trapFloorTile, Color32 color)
    {
        if (!trapFloorTile) return;

        trapFloorTileRenderers = trapFloorTile.GetComponentsInChildren<Renderer>();
        foreach (Renderer _renderer in trapFloorTileRenderers)
        {
            _renderer.material.SetColor("_EmissionColor", color);
        }
    }

    private void OnGUI()
    {
        if (isTrapHighlighted)
        {

        }
    }

    public void EndBuildingTurn(GameObject button)
    {
        button.SetActive(false);
        GameManager.currentState = GameManager.State.READY;
    }
}
