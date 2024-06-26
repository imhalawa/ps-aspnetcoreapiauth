﻿using Globomantics.Api.Extensions.Attributes;
using Globomantics.Client.Models;
using Globomantics.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Globomantics.Server.Controllers
{
    [ApiController]
    [Route("conference")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ConferenceController : Controller
    {
        private readonly IConferenceRepository _Repo;

        public ConferenceController(IConferenceRepository repo)
        {
            _Repo = repo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [TypeFilter(typeof(ApiKeyAttribute))]
        public IActionResult GetAll()
        {
            var conferences = _Repo.GetAll();
            if (conferences == null || !conferences.Any())
            {
                return NoContent();
            }
            return Ok(conferences);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var conference = _Repo.GetById(id);
            if (conference == null)
            {
                return NotFound();
            }
            return Ok(conference);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Add(ConferenceModel model)
        {
            var id = _Repo.Add(model);
            return CreatedAtAction(nameof(GetById), new { id }, model);
        }
    }
}
