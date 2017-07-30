using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapsManager : MonoBehaviour
{
    public enum TypeOfTrap { SPIKES, TURRET };

    private GameObject spikesTrapPrefab;
    private GameObject turretTrapPrefab;
    private GameObject trapToInstantiate;

    private RaycastHit hit;

    private void Awake()
    {
        spikesTrapPrefab = Resources.Load<GameObject>("Traps/Spikes/SpikesTrap");
        turretTrapPrefab = Resources.Load<GameObject>("Traps/Turret/TurretTrap");
    }

    private void Update()
    {
        if (GameManager.currentState != GameManager.State.BUILDING) return;

        Transform _camera = Camera.main.transform;
        if (Physics.Raycast(_camera.position, _camera.forward, out hit, Mathf.Infinity, LayerMask.GetMask("TrapFloor")))
        {
            TrapFloorTile _trapFloorTile = hit.transform.GetComponent<TrapFloorTile>();
            if (!_trapFloorTile || _trapFloorTile.isTrapPlaced) return;

            //Highlight floortile
            //Debug.Log("Place trap here!");

            //Place trap
            if (Input.GetMouseButtonDown(0))
            {
                switch (_trapFloorTile.type)
                {
                    case TypeOfTrap.SPIKES:
                        trapToInstantiate = spikesTrapPrefab;
                        break;
                    case TypeOfTrap.TURRET:
                        trapToInstantiate = turretTrapPrefab;
                        break;
                }

                Vector3 trapPosition = _trapFloorTile.transform.position;
                trapPosition.y += trapToInstantiate.GetComponentInChildren<Renderer>().bounds.size.y * .5f;
                GameObject newTrap = Instantiate(trapToInstantiate, trapPosition, Quaternion.identity);
                newTrap.transform.parent = transform;
                _trapFloorTile.isTrapPlaced = true;
            }
        }
    }

    public void EndBuildingTurn(GameObject button)
    {
        button.SetActive(false);
        GameManager.currentState = GameManager.State.READY;
    }
}
