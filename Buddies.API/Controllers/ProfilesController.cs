﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Buddies.API.Entities;
using Buddies.API.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Buddies.API.IO;
using Microsoft.EntityFrameworkCore;

namespace Buddies.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {

        private readonly ApiContext _context;
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Initializes a new ProfileController.
        /// </summary>
        /// <param name="userManager">UserManager from ASP.NET Core Identity.</param>
        public ProfilesController(ApiContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// API route GET /api/v1/profiles/:id for fetching profile.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProfile(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);

            if (profile == null)
            {
                return NotFound("PROFILE NOT FOUND");
            }

            var profileResponse = new UserProfileResponse();
            profileResponse.FirstName = profile.FirstName;
            profileResponse.LastName = profile.LastName;
            profileResponse.UserId = profile.UserId;
            profileResponse.AboutMe = profile.AboutMe;
            profileResponse.Headline = profile.Headline;

            for (int i = 0; i < profile.Skills.Count; i++)
            {
                var skill = new SkillResponse();
                skill.Name = profile.Skills[i].Name;
                skill.Delete = profile.Skills[i].Delete;
                if (!skill.Delete)
                {
                    profileResponse.Skills.Add(skill);
                }
            }

            var userEntity = _userManager.GetUserAsync(User).Result;
            if (userEntity == null || userEntity.Id != id)
            {
                profileResponse.Success = 0;
            }
            profileResponse.Success = 1;

            return Ok(profileResponse);
        }

        /// <summary>
        /// API route PUT /api/v1/profiles for updating profile.
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> UpdateProfile(UpdateProfileRequest profile)
        {
            var dbProfile = await _context.Profiles.FindAsync(profile.UserId);
            if (dbProfile == null)
            {
                return NotFound("PROFILE NOT FOUND");
            }
            dbProfile.FirstName = profile.FirstName;
            dbProfile.LastName = profile.LastName;
            dbProfile.Headline = profile.Headline;
            dbProfile.AboutMe = profile.AboutMe;
            dbProfile.Skills = new List<Skills>();
            for (int i = 0; i < profile.Skills.Count; i++)
            {
                var skill = new Skills(profile.Skills[i].Name);
                skill.Delete = profile.Skills[i].Delete;
                dbProfile.Skills.Add(skill);
            }
            _context.SaveChanges();
           
            return Ok();
        }


    }
}
