using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using PlacesYouveBeen.Models;
using System;
using System.Collections.Generic;

namespace PlacesYouveBeen.Tests
{
  [TestClass]
  public class PlacesTests : IDisposable
  {
    public void Dispose()
    {
      Places.ClearAll();
    }

    public PlacesTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=epicodus;port=3306;database=places_youve_been_test;";
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyListFromDatabase_PlacesList()
    {
      //Arrange
      List<Places> newList = new List<Places> { };

      //Act
      List<Places> result = Places.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }
    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Places()
    {
      // Arrange, Act
      Places firstPlace = new Places("seattle");
      Places secondPlace = new Places("seattle");

      // Assert
      Assert.AreEqual(firstPlace, secondPlace);
    }

    [TestMethod]
    public void Save_SavesToDatabase_PlacesList()
    {
    //Arrange
    Places testPlace = new Places("seattle");

    //Act
    testPlace.Save();
    List<Places> result = Places.GetAll();
    List<Places> testList = new List<Places>{testPlace};

    //Assert
    CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectPlaceFromDatabase_Places()
    {
      //Arrange
      Places newPlaces = new Places("seattle");
      newPlaces.Save();
      Places newPlaces2 = new Places("Portland");
      newPlaces2.Save();

      //Act
      Places foundPlaces = Places.Find(newPlaces.Id);
      //Assert
      Assert.AreEqual(newPlaces, foundPlaces);
    }
  }
}