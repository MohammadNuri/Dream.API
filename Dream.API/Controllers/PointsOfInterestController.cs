﻿using Dream.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Dream.API.Controllers
{

    [Produces("application/json")]  
    [Route("api/Cities/{cityId}/PointsOfInterest")]
    [ApiController]
  
    public class PointsOfInterestController : ControllerBase
    {
        //Get All & Get With Id 
        #region Get
        [HttpGet]
        public ActionResult<PointOfInterestDto> GetPointsOfInterest(int cityId)
        {

            var city = CitiesDataStore.CurrentCities.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointOfInterest);
        }
        [HttpGet("{pointOfInterestId}", Name = "GetPointsOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointsOfInterest(int cityId, int pointOfInterestId)
        {
            var city = CitiesDataStore.CurrentCities.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var point = city.PointOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
            if (point == null)
            {
                return NotFound();
            }

            return Ok(point);
        }
        #endregion
        //----------------------------------------------------------------


        //Create 
        #region Create
        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId,[FromBody]PointOfInterestForCreationDto pointOfInterest)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var city = CitiesDataStore.CurrentCities.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)   
            {
                return NotFound();
            }

            var maxPointOfInterestId =
                CitiesDataStore.CurrentCities.Cities.SelectMany(c => c.PointOfInterest).Max(p => p.Id);

            var createPoint = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointOfInterest.Add(createPoint);


            return CreatedAtAction("GetPointsOfInterest", 
                new 
                {
                    cityId = cityId,
                    pointOfInterestId = createPoint.Id
                },
                createPoint
            );
        }
        #endregion
        //----------------------------------------------------------------


        //This is for Update Data in HTTP Put (Have to Change All Properties) 
        #region Update (HttpPut)
        [HttpPut("{pointOfInterestId}")]

        public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestForUpdateDto pointOfInterest)
        {
            // Find a City with cityId 
            var city = CitiesDataStore.CurrentCities.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();
            // Find a Point of Interest with pointOfInterestId
            var point = city.PointOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
            if (point == null)
                return NotFound();

            point.Name = pointOfInterest.Name;
            point.Description = pointOfInterest.Description;

            return NoContent();
        }
        #endregion
        //----------------------------------------------------------------

        //----------------------------------------------------------------
        //This is for Edit Data in HTTP Patch (No Need to Change All Properties, it Keeps the Same Data When u Enter Null)
        //Requirement Install:
        //Microsoft.AspNetCore.JsonPatch
        //Microsoft.AspNetCore.Mvc.NewtonsoftJso
        //Get the Response from Body Like :
        // [
        //   {
        //      "path": "/name",
        //      "op": "replace",
        //      "value": "TestForPatch2"
        //   }
        // ]
        //this will replace the Name Property with the entered value 
        #region Edit (HttpPatch)
        [HttpPatch("{pontiOfInterestid}")]
        public ActionResult EditPointsOfInterest(int cityId, int pontiOfInterestid, JsonPatchDocument<PointOfInterestForUpdateDto> patchPointOfInterest)
        {
            // Find a City with cityId 
            var city = CitiesDataStore.CurrentCities.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();
            // Find a Point of Interest with pointOfInterestId
            var point = city.PointOfInterest.FirstOrDefault(p => p.Id == pontiOfInterestid);
            if (point == null)
                return NotFound();

            var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            {
                Name = point.Name,
                Description = point.Description,
            };

            patchPointOfInterest.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }

            point.Name = pointOfInterestToPatch.Name;
            point.Description = pointOfInterestToPatch.Description;


            return NoContent();
        }
        #endregion
        //-----------------------------------------------------------------


    }
}