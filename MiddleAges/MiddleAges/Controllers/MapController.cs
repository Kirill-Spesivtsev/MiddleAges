﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiddleAges.Data;
using MiddleAges.Entities;
using MiddleAges.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiddleAges.Controllers
{
    [Authorize]
    public class MapController : Controller
    {
        private readonly ILogger<MapController> _logger;
        private readonly ApplicationDbContext _context;

        public MapController(ILogger<MapController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            Player player = _context.Players.FirstOrDefault();
            return View("Map", player);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
                
        public JsonResult GetLandDataById(string id)
        {
            Land land = _context.Lands.FirstOrDefault(k => k.LandId == id);
            
            if (land == null)
            {
                return Json("NotFound");
            }

            return Json(JsonSerializer.Serialize(land));
        }
    }
}
