using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewLocation", menuName ="RPG/Location")]
public class Location : ScriptableObject {
    public string locationName;
    public string description;

    public List<Location> locations;


}
