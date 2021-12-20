using Microsoft.AspNetCore.Mvc;
using PlacesYouveBeen.Models;
using System.Collections.Generic;

namespace PlacesYouveBeen
{
  public class PlacesController : Controller
  {
    [HttpGet("/places")]
    public ActionResult Index()
    {
      List<Places> outputList = Places.GetAll();
      return View(outputList);
    }

    [HttpGet("/places/new")]
    public ActionResult CreateForm()
    {
      return View();
    }

    [HttpPost("/places")]
    public ActionResult Create(string cityname)
    {
      Places newPlace = new Places(cityname);
      newPlace.Save();
      return RedirectToAction("Index");
    }


  }
}