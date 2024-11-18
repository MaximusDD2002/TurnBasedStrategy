using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnterBuildingScript : MonoBehaviour
{
    public GameObject HouseMenu, Resources, UnitsHire;
    public TextMeshProUGUI BuildingName;
    public TextMeshProUGUI woodResource, oreResource, manpowerResource, meleeUnit, rangedUnit;
    public void EnterBuilding(string name)
    {
        HouseMenu.SetActive(true);
        BuildingName.text = name;

        switch (name)
        {
            case "Resource Building":
                EnterResourceBuilding();
            break;
            case "Unit Building":
                EnterUnitBuilding();
            break;
        }
    }

    public void EnterResourceBuilding()
    {
        Resources.SetActive(true);
        woodResource.text = "Wood: " + 1;
        oreResource.text = "Ore: " + 1;
        manpowerResource.text = "Manpower: " + 1;
    }

    public void EnterUnitBuilding()
    {
        UnitsHire.SetActive(true);
        meleeUnit.text = "Hire melee unit";
        rangedUnit.text = "Hire ranged unit";
    }
}

