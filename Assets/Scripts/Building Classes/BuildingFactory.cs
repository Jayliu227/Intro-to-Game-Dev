using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingFactory {

    Building building;

	public static List<Building> Buildings = new List<Building> ();

    public Building GetBuilding(string buildingType)
    {

        switch (buildingType)
        {
            case "Hospital":
                building = new Hospital();
                if(GameController.instance._PlayerMoney < building.price)
                {
                    UpdateBarManager.current.UpdateInformationOnBar("Money is not enough for a " + building.buildingType);
                    GameObject.Destroy(building.go);
                    return null;
                }

                if(GameController.instance.buildingNum["hospital"] >= building.maxNum)
                {
                    UpdateBarManager.current.UpdateInformationOnBar(building.buildingType + " is overloaded");
                    GameObject.Destroy(building.go);
                    return null;
                }
                
         
                return building;

            case "Residence":
                building = new Residence();
                if (GameController.instance._PlayerMoney < building.price)
                {
                    UpdateBarManager.current.UpdateInformationOnBar("Money is not enough for a " + building.buildingType);
                    GameObject.Destroy(building.go);
                    return null;
                }

                if (GameController.instance.buildingNum["residence"] >= building.maxNum)
                {
                    UpdateBarManager.current.UpdateInformationOnBar(building.buildingType + " is overloaded");
                    GameObject.Destroy(building.go);
                    return null;
                }


                return building;
            case "Lab":
                building = new Labotary();
                if (GameController.instance._PlayerMoney < building.price)
                {
                    UpdateBarManager.current.UpdateInformationOnBar("Money is not enough for a " + building.buildingType);
                    GameObject.Destroy(building.go);
                    return null;
                }

                if (GameController.instance.buildingNum["lab"] >= building.maxNum)
                {
                    UpdateBarManager.current.UpdateInformationOnBar(building.buildingType + " is overloaded");
                    GameObject.Destroy(building.go);
                    return null;
                }

                return building;
            case "Retailer":
                building = new Retailer();
                if (GameController.instance._PlayerMoney < building.price)
                {
                    UpdateBarManager.current.UpdateInformationOnBar("Money is not enough for a " + building.buildingType);
                    GameObject.Destroy(building.go);
                    return null;
                }

                if (GameController.instance.buildingNum["retailer"] >= building.maxNum)
                {
                    UpdateBarManager.current.UpdateInformationOnBar(building.buildingType + " is overloaded");
                    GameObject.Destroy(building.go);
                    return null;
                }

                return building;
            case "FacultyRoom":
                building = new FacultyRoom();
                if (GameController.instance._PlayerMoney < building.price)
                {
                    UpdateBarManager.current.UpdateInformationOnBar("Money is not enough for a " + building.buildingType);
                    GameObject.Destroy(building.go);
                    return null;
                }

                if (GameController.instance.buildingNum["facultyRoom"] >= building.maxNum)
                {
                    UpdateBarManager.current.UpdateInformationOnBar(building.buildingType + " is overloaded");
                    GameObject.Destroy(building.go);
                    return null;
                }

                return building;
            case "DiningHall":
                building = new DiningHall();
                if (GameController.instance._PlayerMoney < building.price)
                {
                    UpdateBarManager.current.UpdateInformationOnBar("Money is not enough for a " + building.buildingType);
                    GameObject.Destroy(building.go);
                    return null;
                }

                if (GameController.instance.buildingNum["retailer"] >= building.maxNum)
                {
                    UpdateBarManager.current.UpdateInformationOnBar(building.buildingType + " is overloaded");
                    GameObject.Destroy(building.go);
                    return null;
                }

                return building;
            default:
                Debug.Log(buildingType + "Building Type is not supported.");
                return null;
        }

    }

}
