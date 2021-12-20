using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace PlacesYouveBeen.Models
{
  public class Places
  {
    public string CityName { get; set; }
    public int Id { get; set; }

    public Places(string city)
    {
      CityName = city;
    }

    public Places(string city, int id)
    {
      CityName = city;
      Id = id;
    }

    public override bool Equals(System.Object otherPlace)
    {
      if (!(otherPlace is Places))
      {
        return false;
      }
      else
      {
        Places newPlace = (Places) otherPlace;
        bool nameEquality = (this.CityName == newPlace.CityName);
        bool idEquality = (this.Id == newPlace.Id);
        return (nameEquality && idEquality);
      }
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM places;";
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

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;

      // Begin new code

      cmd.CommandText = @"INSERT INTO places (name) VALUES (@placesName);";
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@placesName";
      name.Value = this.CityName;
      cmd.Parameters.Add(name);    
      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;

      // End new code

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Places Find(int id)
    {
      // We open a connection.
      MySqlConnection conn = DB.Connection();
      conn.Open();

      // We create MySqlCommand object and add a query to its CommandText property. We always need to do this to make a SQL query.
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM places WHERE id = @thisId;";

      // We have to use parameter placeholders (@thisId) and a `MySqlParameter` object to prevent SQL injection attacks. This is only necessary when we are passing parameters into a query. We also did this with our Save() method.
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      // We use the ExecuteReader() method because our query will be returning results and we need this method to read these results. This is in contrast to the ExecuteNonQuery() method, which we use for SQL commands that don't return results like our Save() method.
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int placeId = 0;
      string placeName = "";
      while (rdr.Read())
      {
        placeId = rdr.GetInt32(0);
        placeName = rdr.GetString(1);
      }
      Places foundPlace= new Places(placeName, placeId);

      // We close the connection.
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundPlace;
    }
  }
}