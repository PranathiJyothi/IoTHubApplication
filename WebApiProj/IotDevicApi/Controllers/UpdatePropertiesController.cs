using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using Microsoft.Azure.Devices.Shared;
using System.Text;
using Microsoft.Azure.Devices;
using IotDevicApi.Repositories;

namespace IotDevicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdatePropertiesController : ControllerBase
    {
        private readonly UpdatePropertiesRepository repository;

        public UpdatePropertiesController(UpdatePropertiesRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("reported")]
        public async Task<IActionResult> UpdateReportedProperties([FromBody] UpdateProperties model, string deviceId)
        {
            var twin = await repository.UpdateReportedPropertiesAsync(model, deviceId);
            return Ok(twin);
        }

        [HttpPost("desired")]
        public async Task<IActionResult> UpdateDesiredProperties([FromBody] UpdateProperties model, string deviceId)
        {
            var twin = await repository.UpdateDesiredPropertiesAsync(model, deviceId);
            return Ok(twin);
        }
        public class UpdateProperties
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
        /*// Connection string and device ID of your IoT Hub
        static string connectionString = "YOUR_IOT_HUB_CONNECTION_STRING";
        static string deviceId = "YOUR_DEVICE_ID";
        //static RegistryManager registryManager;

        // Endpoint for updating reported and desired properties
        [HttpPost("properties")]
        public async Task<IActionResult> UpdateProperties([FromBody] PropertiesInput propertiesInput)
        {
            // Create an instance of the DeviceClient class
            DeviceClient deviceClient = null;
            deviceClient = DeviceClient.CreateFromConnectionString(connectionString, deviceId);
            await deviceClient.GetTwinAsync();

            //Define the reported and desired properties as C# objects
            var reportedProperties = new
            {
                Temperature = propertiesInput.Temperature,
                Humidity = propertiesInput.Humidity
            };
            var desiredProperties = new
            {
                Light = propertiesInput.Light,
                Fan = propertiesInput.Fan
            };
            *//*   TwinCollection reportedProperties , connectivity;
               reportedProperties = new TwinCollection();
               connectivity = new TwinCollection();
               connectivity["type"] = "cellular";
               reportedProperties["connectivity"] = connectivity;
               await deviceClient.UpdateReportedPropertiesAsync(reportedProperties);
   *//*
            // Convert the C# objects to JSON strings
            string reportedPropertiesJson = JsonConvert.SerializeObject(reportedProperties);


            string desiredPropertiesJson = JsonConvert.SerializeObject(desiredProperties);
            // Convert the JSON strings to byte arrays
            byte[] reportedPropertiesPayload = Encoding.UTF8.GetBytes(reportedPropertiesJson);
            byte[] desiredPropertiesPayload = Encoding.UTF8.GetBytes(desiredPropertiesJson);


            // Update the reported and desired properties using the DeviceClient
            
               // await deviceClient.UpdateReportedPropertiesAsync(reportedProperties);
            
            await deviceClient.UpdateReportedPropertiesAsync(reportedPropertiesPayload);

            //var twin = await registryManager.GetTwinAsync("myDeviceId");
          *//*  var desiredProperties = new
            {
                Light = propertiesInput.Light,
                Fan = propertiesInput.Fan
            };*//*
            await deviceClient.UpdateDesiredPropertiesAsync(desiredPropertiesPayload);
           // await registryManager.UpdateTwinAsync(twin.DeviceId, twin.ETag);
            return Ok();
        }
    }*/
        // Input class for updating properties
        /*   public class PropertiesInput
           {
               public double Temperature { get; set; }
               public double Humidity { get; set; }
               public string Light { get; set; }
               public string Fan { get; set; }
           }*/
    }
}

