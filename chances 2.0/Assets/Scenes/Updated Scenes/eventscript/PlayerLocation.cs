using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLocation : MonoBehaviour
{
    public Image player1;
    public Image player2;
    public Image player3;
    public Image player4;
    public Image player5;
    public Image player6;
    public Image player7;
    public Image player8;
    public Image player9;
    public Image player10;
    public Image player11;
    public Image player12;

    public string CurrentLocation;

    private void Start()
    {
        string savedLocation = PlayerPrefs.GetString("PlayerLocation", "Hometown");
        UpdateLocation(savedLocation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LocationZone"))
        {
            string locationName = other.gameObject.name;
            UpdateLocation(locationName);
            PlayerPrefs.SetString("PlayerLocation", locationName); // Save new location
        }
    }

    public void UpdateLocation(string location)
    {
        CurrentLocation = location;

        player1.gameObject.SetActive(false);
        player2.gameObject.SetActive(false);
        player3.gameObject.SetActive(false);
        player4.gameObject.SetActive(false);
        player5.gameObject.SetActive(false);
        player6.gameObject.SetActive(false);
        player7.gameObject.SetActive(false);
        player8.gameObject.SetActive(false);
        player9.gameObject.SetActive(false);
        player10.gameObject.SetActive(false);
        player11.gameObject.SetActive(false);
        player12.gameObject.SetActive(false);

        switch (location)
        {
            case "Hometown":
                player1.gameObject.SetActive(true);
                break;
            case "Road1":
                player2.gameObject.SetActive(true);
                break;
            case "Market":
                player3.gameObject.SetActive(true);
                break;
            case "School":
                player4.gameObject.SetActive(true);
                break;
            case "Road2":
                player5.gameObject.SetActive(true);
                break;
            case "UpperCity":
                player6.gameObject.SetActive(true);
                break;
            case "LowerCity":
                player7.gameObject.SetActive(true);
                break;
            case "CityHall":
                player8.gameObject.SetActive(true);
                break;
            case "Bar":
                player9.gameObject.SetActive(true);
                break;
            case "Mountain":
                player10.gameObject.SetActive(true);
                break;
            case "Road3":
                player11.gameObject.SetActive(true);
                break;
            case "RoadToMnt":
                player12.gameObject.SetActive(true);
                break;
        }
    }
}
