using UnityEngine;
using System.Collections;

public class BuildingFactory {

    Building building = null;

    public Building GetBuilding(string buildingType)
    {

        switch (buildingType)
        {
            case "Hospital":
                building = new Hospital();
                return building;
            case "Residence":
                building = new Residence();
                return building;
            default:
                Debug.Log(buildingType + "Building Type is not supported.");
                return null;
        }

    }

}
