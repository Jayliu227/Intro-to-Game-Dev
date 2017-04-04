using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToolTips : MonoBehaviour {

    public Text text;
    
    public void OnExitCursor()
    {
        text.text = "";
    }

    public void EC_Residence()
    {
        text.text = "Residence: 200.";
    }

    public void EC_Faculty()
    {
        text.text = "Faculty Room: 300.";
    }

    public void EC_Hospital()
    {
        text.text = "Hospital: 800.";
    }

    public void EC_Lab()
    {
        text.text = "Lab: 5000.";
    }

    public void EC_DiningHall()
    {
        text.text = "Dining Hall: 600.";
    }

    public void EC_Retailer()
    {
        text.text = "Retailer: 500.";
    }

    public void EC_MadScientist()
    {
        text.text = "Mad Scientist: 450. Prereq: Dining Hall "+ GameController.instance.buildingNum["diningHall"] + "/1" + " and Lab " + GameController.instance.buildingNum["lab"] + "/1";
        Debug.Log("11");
    }

    public void EC_Prisoner()
    {
        text.text = "Prisoner: 100.";
    }

    public void EC_Thief()
    {
        text.text = "Thief: 380. Prereq: Hospital " + GameController.instance.buildingNum["hospital"] + "/1";
    }

    public void EC_ProfKiller()
    {
        text.text = "Professional Killer: 450. Prereq: Retailer " + GameController.instance.buildingNum["retailer"] + "/1";
    }
}
