using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayerApi.Data;
using PlayerApi.Models;

namespace PlayerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly AppDBContext _context;
        public PlayerController(AppDBContext context)
        {
            _context = context;
        }

        
        [HttpGet("GetPlayers")]
        public async Task<IActionResult> GetPlayers()
        {
            var player = await _context.player.Select(x => new player
            {
                id = x.id,
                age = x.age,
                name = x.name,
                position = x.position,
                team = x.team,

            }).ToListAsync();

            return Ok(player);
        }

        [HttpGet("GetPlayerByID")]
        public IActionResult GetPlayer(int playerId)
        {
            var player = _context.player.FirstOrDefault(x => x.id == playerId);

            if(player == null)
            {
                return NotFound("The player requested doens't exist");
            }
            return Ok(player);
        }

        [HttpPost("CreatePlayer")]
        public async Task<IActionResult> PostPlayer(playerInsert introPlayer)
        {
            var newPlayer = new player
            {
                age = introPlayer.age,
                name = introPlayer.name,
                position = introPlayer.position,
                team = introPlayer.team,
            };

            _context.player.Add(newPlayer);
            await _context.SaveChangesAsync();

            return Ok(introPlayer);
        }

        [HttpPut("UpdatePlayer")]
        public async Task<IActionResult> PutPlayer(int playerId,playerInsert updatePlayer)
        {
            var playerToUpdate = _context.player.FirstOrDefault(x=> x.id == playerId);
            if(playerToUpdate == null)
            {
                return NotFound("The player requested doens't exist");
            }

            playerToUpdate.age = updatePlayer.age;
            playerToUpdate.name = updatePlayer.name;
            playerToUpdate.position = updatePlayer.position;
            playerToUpdate.team = updatePlayer.team;
            
            await _context.SaveChangesAsync();

            return Ok(playerToUpdate);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePlayer(int playerId)
        {
            var playerToDelete = _context.player.FirstOrDefault(x=> x.id == playerId);
            
            if(playerToDelete == null)
            {
                return NotFound("The player requested doens't exist");
            }

            _context.player.Remove(playerToDelete);
            await _context.SaveChangesAsync();

            return Ok("Player deleted");
        }

    }
}