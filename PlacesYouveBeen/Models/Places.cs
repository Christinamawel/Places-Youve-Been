using System;
using System.Collections.Generic;

namespace PlacesYouveBeen.Models
{
  public class Places
  {
    public static List<Places> placeList = new List<Places>{};
    public string CityName { get; set; }

    public Places(string city)
    {
      CityName = city;
      placeList.Add(this);
    }
  }
}