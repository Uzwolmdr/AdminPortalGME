using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SampleApiService.Models;
using SampleApiService.Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Web;

namespace SampleApiService.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class MobileController : ControllerBase
    {
        private IService _service;
        private readonly ILogger<MobileController> _logger;
        private readonly IPublishEndpoint _publish;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        public MobileController(IService service, ILogger<MobileController> logger, IPublishEndpoint publish, ISendEndpointProvider sendEndpointProvider)
        {
            _service = service;
            _logger = logger;
            _publish = publish;
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpGet]
        [Route("Hello")]
        public async Task<IActionResult> Hello()
        {
            _logger.LogDebug("Sample API EndPoint Called. New API Service");

            string a = _service.SaveMoRequests();

            _logger.LogInformation($"API Response from Service is :{a}");

            var evt = new SmsEvent
            {
                CustomerId = Request.Headers["CorrelationId"].ToString(),
                FullName = "Neeraj Dhungana",
                MobileNumber = "9851225462",
                RegisteredAt = DateTime.Now
            };
            try
            {
                //// publish event to RabbitMQ . This is a message braodcast.
                //await _publish.Publish(evt, context =>
                //{
                //    context.Headers.Set("CorrelationId", Request.Headers["CorrelationId"].ToString());
                //});


                // specify the queue you want to send directly to
                var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:sms-consumer_send"));

                await endpoint.Send(evt, context =>
                {
                    context.Headers.Set("Authorization", Request.Headers["Authorization"].ToString());
                    context.Headers.Set("CorrelationId", Request.Headers["CorrelationId"].ToString());
                });
                _logger.LogInformation($"Message successfully published to RabbitMq for  CorrelationId:{Request.Headers["CorrelationId"].ToString()}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in Queue :{ex.Message}");
            }

            return Ok(new
            {
                Success = true,
                Message = "New API Service Called Successfully (Hello)",
                Data = new Response() { 
                    code = 200,
                    msg=a
                }
            }) ;

        }

        [HttpGet]
        [Route("Hi")]
        public IActionResult Hi()
        {
            _logger.LogDebug("Sample API EndPoint Called. New API Service");

            string a = _service.SaveMoRequests();

            _logger.LogInformation($"API Response from Service is :{a}");

            return Ok(new
            {
                Success = true,
                Message = "New API Service Called Successfully (Hi)",
                Data = new Response()
                {
                    code = 200,
                    msg = a
                }
            });

        }

        [HttpGet]
        [Route("GetVersion")]
        public IActionResult GetVersion()
        {
            _logger.LogDebug("Sample API EndPoint Called to Get Version of Product.");

            return Ok(new
            {
                Success = true,
                Message = "New API Service Called Successfully (Hi)",

                Data = new VersionResponse()
                {
                    code = 200,
                    version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion
                }
            });

        }

    }
}