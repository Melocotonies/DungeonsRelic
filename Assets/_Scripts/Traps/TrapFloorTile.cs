using UnityEngine;

public class TrapFloorTile : MonoBehaviour
{
    [SerializeField] TrapsManager.TypeOfTrap typeOfTrap;

    public TrapsManager.TypeOfTrap type
    {
        get
        {
            return typeOfTrap;
        }
    }

    public bool isTrapPlaced { get; set; }
}
