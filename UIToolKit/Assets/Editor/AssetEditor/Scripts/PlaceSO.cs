using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu()]
public class PlaceSO : ScriptableObject
{
    public Location[] Locations;
}

[System.Serializable]
public struct Location
{
    public string placeName;
    public string placeTag;
}