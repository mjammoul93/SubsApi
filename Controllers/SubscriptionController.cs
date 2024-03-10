using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SubsApi.DTOs;
using SubsApi.Interfaces;
using SubsApi.Models;

namespace SubsApi.Controllers
{
    [Authorize]
    public class SubscriptionController : BaseAPIController
    {
        private readonly ISubscriptionRepository  _subscriptionRepo;
        private readonly IUserRepository _userRepo;
        private readonly ILogger<SubscriptionController> _logger;

        public SubscriptionController(ILogger<SubscriptionController> logger, ISubscriptionRepository subscriptionRepo, IUserRepository userRepository)
        {
            _subscriptionRepo = subscriptionRepo;
            _userRepo = userRepository;
            _logger = logger;
        }
        [Authorize(Roles = "Admin,Member")]
        [HttpPost("Subscribe")]
        public async Task<ActionResult<bool>> Subscribe(SubscribtionDTO subDTO)
        {
            _logger.LogInformation("Subscribe EndPoint Reached");
            if (subDTO == null ) 
            {
                return BadRequest("Parameters are required");
            }

            try
            {
                AppUser user = await _userRepo.GetAppUsersByIdAsync(subDTO.UserId);
                GymSubscriptionType type = await _subscriptionRepo.GetSubscriptionTypeByIdAsync(subDTO.SubscribtionTypeId);
                MembersSubsciptions subscription = new MembersSubsciptions
                {
                    Member = user,
                    Subscription = type,
                    CreatedDate = DateTime.Now.ToUniversalTime(),
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(type.ValidityInDays)),
                    Active = true,
                };

                _logger.LogInformation("Creating new Subscribtion");

                var result = await _subscriptionRepo.SaveSubsciptionAsync(subscription);
                if (!result)
                {
                    _logger.LogInformation("Failed to create subscription");

                    return NoContent();
                }
                else
                {
                    _logger.LogInformation("Subscription Created Successfully");
                }
                return CreatedAtAction("Subscribe",result);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error creating Subscription: "+ex.Message);

                return StatusCode(500,  ex.Message);
            }
          
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet("GetActiveSubscriptions")]
        public async Task<ActionResult<IList<SubscriptionDetailsDTO>>> GetActiveSubscriptions( )
        {
            try
            {
                _logger.LogInformation("GetActiveSubscriptions EndPoint Reached");

                var result = await _subscriptionRepo.GetActiveSubsciptionsAsync();

                _logger.LogInformation("GetActiveSubscriptions returned Results");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Getting Active Subscriptions: " + ex.Message);

                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetSubscriptionsByUserId")]
        public async Task<ActionResult<IList<SubscriptionDetailsDTO>>> GetSubscriptionsByUserId(int userId)
        {
            try
            {
                _logger.LogInformation("GetSubscriptionsByUserId EndPoint Reached");

                var result = await _subscriptionRepo.GetSubscriptionByUserIdAsync(userId);

                _logger.LogInformation("GetSubscriptionsByUserId returned Results");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Getting Active Subscriptions: " + ex.Message);

                return StatusCode(500, ex.Message);
            }

        }
    }
}
