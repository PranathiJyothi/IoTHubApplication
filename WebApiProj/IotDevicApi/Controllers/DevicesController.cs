using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using System.Text;
using System.Configuration;


namespace IotDevicApi.Controllers
{
   
    [ApiController]
    [Route("[controller]")]
    
    public class DeviceController : ControllerBase
    {
        private static RegistryManager _registryManager;
       // private static DeviceClient deviceClient;
        private readonly RegistryManager registryManager;

        //device connection string  you registered a device in the IoT Hub
        static string connectionString = "HostName=PranathiHub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=Y7RF10/LvAdey/nUBKWxD+Puzs+QpvUUCzQLdbW3uKQ=";

        //var connectionString = configuration.GetValue<string>("HostName=PranathiHub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=Y7RF10/LvAdey/nUBKWxD+Puzs+QpvUUCzQLdbW3uKQ=");// static DeviceClient Client = null;

        public DeviceController()
        {
            
            _registryManager = RegistryManager.CreateFromConnectionString(connectionString);
        }
        //private static RegistryManager  _registryManager = RegistryManager.CreateFromConnectionString("IoTHubConnectionString");         [HttpPost("devices")]
        [HttpPost("devices")]
        public async Task<ActionResult> CreateDeviceAsync(string deviceId)
        {
            try
            {
                //var device = new Device(deviceId);
                //device = await _registryManager.AddDeviceAsync(device);
                //return Ok(device);
                return Ok();
            }
            /*catch (DeviceAlreadyExistsException)
            {                 device = await _registryManager.GetDeviceAsync(deviceId);
            }*/
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating device {deviceId}:{ex.Message}");
            }
        }

        [HttpGet("{deviceId}")]
        public async Task<ActionResult> GetDeviceAsync(string deviceId)
        {
            try
            {
                var device = new Device(deviceId);
                //device = await _registryManager.AddDeviceAsync(device);
                //var device = new Device(deviceId);
                device = await _registryManager.GetDeviceAsync(deviceId);
                if (device == null)
                {
                    return NotFound();
                }
                return Ok(device);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving device {deviceId}:{ex.Message} ");
            }
        }

        [HttpPut("{deviceId}/ {status}")]
        public async Task<IActionResult> UpdateDeviceAsync(string deviceId, string status)
        {
            try
            {
                Device device = await _registryManager.GetDeviceAsync(deviceId);
                device.Status = (DeviceStatus)Enum.Parse(typeof(DeviceStatus), status, true);
                await _registryManager.UpdateDeviceAsync(device); 

               /* var device = new Device(deviceId);                
                await _registryManager.UpdateDeviceAsync(device);*/
                /*if (device != null)
                {
                    device.Status = device.Status; 
                    device.StatusReason = "Disabled for test";
                    //device = await _registryManager.UpdateDeviceAsync(device);                 }
                   
                }*/
                return Ok(device);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating device {deviceId}:{ex.Message}");
            }
        }

        [HttpDelete("{deviceId}")]
        public async Task<IActionResult> RemoveDeviceAsync(string deviceId)
        {
            try
            {
                await _registryManager.RemoveDeviceAsync(deviceId);
                return Ok($"Device {deviceId} removed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while removing device {deviceId}:{ex.Message}");
            }
        }

      
      

        /* //UpdateReportedPropertiesAsync
         public static async void InitClient()
         {
             try
             {
                 //Console.WriteLine("Connecting to hub");
                 Client = DeviceClient.CreateFromConnectionString(DeviceConnectionString*//*, TransportType.Mqtt*//*);
                // Console.WriteLine("Retrieving twin");
                 await Client.GetTwinAsync();
             }
             catch (Exception ex)
             {

                // return StatusCode(500,$"Error in sample:{ ex.Message}");
             }
         }
         public static async void ReportConnectivity()
         {
             try
             {
                 Console.WriteLine("Sending connectivity data as reported property");

                 TwinCollection reportedProperties, connectivity;
                 reportedProperties = new TwinCollection();
                 connectivity = new TwinCollection();
                 connectivity["type"] = "cellular";
                 reportedProperties["connectivity"] = connectivity;
                 await Client.UpdateReportedPropertiesAsync(reportedProperties);
             }
             catch (Exception ex)
             {
                 Console.WriteLine();
                 Console.WriteLine("Error in sample: {0}", ex.Message);
             }
         }*/

        /*//telemetry message
        private static async void SendDeviceToCloudMessagesAsync(DeviceClient s_deviceClient)
        {
            try
            {
                double minTemperature = 20;
                double minHumidity = 60;
                Random rand = new Random();

                while (true)
                {
                    double currentTemperature = minTemperature + rand.NextDouble() * 15;
                    double currentHumidity = minHumidity + rand.NextDouble() * 20;

                    // Create JSON message

                    var telemetryDataPoint = new
                    {

                        temperature = currentTemperature,
                        humidity = currentHumidity
                    };

                    string messageString = "";



                    messageString = JsonConvert.SerializeObject(telemetryDataPoint);

                    var message = new Message(Encoding.ASCII.GetBytes(messageString));

                    // Add a custom application property to the message.
                    // An IoT hub can filter on these properties without access to the message body.
                    //message.Properties.Add("temperatureAlert", (currentTemperature > 30) ? "true" : "false");

                    // Send the telemetry message
                    await s_deviceClient.SendEventAsync(message);
                    Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);
                    await Task.Delay(1000 * 10);

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }*/
    }
}