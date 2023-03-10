using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using static IotDevicApi.Controllers.DeviceController;
using System.Text;

namespace IotDevicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelemetryController : ControllerBase
    {
       // private readonly string _connectionString;
        private readonly string _deviceId = "myDevice";
        private static DeviceClient deviceClient;
        private readonly string _connectionString = "HostName=PranathiHub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=C5Fj7AKMJ2NoqfszrrSZi0fRi0BwEH3AauSr/b8351M=";

        public TelemetryController()
        {
            // Replace with your IoT hub connection string and device ID
            //_connectionString = "HostName=PranathiHub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=Y7RF10/LvAdey/nUBKWxD+Puzs+QpvUUCzQLdbW3uKQ=";//"HostName=myiothub.azure-devices.net;SharedAccessKeyName=device;SharedAccessKey=abc123";
            /*_deviceId = "myDevice";*/// "mydevice";
             deviceClient = DeviceClient.CreateFromConnectionString(_connectionString, _deviceId);
        }
        [HttpPost]
        public async Task<IActionResult> PostTelemetry([FromBody] TelemetryDataInput telemetryDataInput)
        {
            try
            {
                // Parse user input into telemetry data
                var telemetryData = new TelemetryData
                {
                    Temperature = double.Parse(telemetryDataInput.Temperature),
                    Humidity = double.Parse(telemetryDataInput.Humidity)
                };
                // Send telemetry data to IoT hub

                string messageBody = JsonConvert.SerializeObject(telemetryData);
                var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                message.Properties.Add("timestamp", DateTime.UtcNow.ToString());
                message.Properties.Add("messageId", Guid.NewGuid().ToString());
                await deviceClient.SendEventAsync(message);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error sending message: {ex.Message}");
            }
        }
        public class TelemetryDataInput
        {
            public string Temperature { get; set; }
            public string Humidity { get; set; }
        }
        public class TelemetryData
        {
            public double Temperature { get; set; }
            public double Humidity { get; set; }
        }

    }
}
