using UnityEngine;
using System.Collections.Generic;

public class LocationCleaner : MonoBehaviour
{
    [HideInInspector]
    public static LocationCleaner instance;
    public List<Location> LocList;
    public List<Location> BaseLocList;
    public void LocClear()
    {
        for (int i = 0; i < LocList.Count; i++)
        {
            LocList[i].CoV = BaseLocList[i].CoV;
            LocList[i].CoFW = BaseLocList[i].CoFW;
            LocList[i].NCoV = BaseLocList[i].NCoV;
        }
    }

    public void Awake()
    {
        instance = this;
    }
}
