using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace PlacesYouveBeen.Models
{
  public class Places
  {
    public string CityName { get; set; }
    public int Id { get; }

    public Places(string city)
    {
      CityName = city;
    }

    public Places(string city, int id)
    {
      CityName = city;
      Id = id;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Places> GetAll()
    {
      List<Places> allPlaces = new List<Places> { };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM places;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
          int placesId = rdr.GetInt32(0);
          string placesName = rdr.GetString(1);
          Places newPlace = new Places(placesName, placesId);
          allPlaces.Add(newPlace);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return allPlaces;
    }
  }
}