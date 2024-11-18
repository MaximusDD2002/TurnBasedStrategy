using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaveBuildingScript : MonoBehaviour
{
    public GameObject HouseMenu;
    public TextMeshProUGUI BuildingName;
    public GameObject Resources, UnitsHire;
    
    public void LeaveBuilding()
    {
        string name = BuildingName.text;
        HouseMenu.SetActive(false);

        switch (name)
        {
            case "Resource Building":
                LeaveResourceBuilding();
            break;
            case "Unit Building":
                LeaveUnitBuilding();
            break;
        }
    }

    public void LeaveResourceBuilding()
    {
        Resources.SetActive(false);
    }
    public void LeaveUnitBuilding()
    {
        UnitsHire.SetActive(false);
    }
}
