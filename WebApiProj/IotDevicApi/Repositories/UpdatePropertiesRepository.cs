﻿using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Azure.Devices;
using static IotDevicApi.Controllers.UpdatePropertiesController;

namespace IotDevicApi.Repositories
{
    public class UpdatePropertiesRepository
    {
        private static string connectionString = "HostName=PranathiHub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=34vx1Se1/KLP1xItr8yuKl/VZh52gbtBfFyOO4p303U=";

        public static async Task<bool> IsDeviceAvailable(string deviceId)
        {
            var registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            Device device = await registryManager.GetDeviceAsync(deviceId);
            if (device.Status == DeviceStatus.Enabled)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<string> UpdateReportedPropertiesAsync(UpdateProperties properties, string deviceId)
        {
            var deviceClient = DeviceClient.CreateFromConnectionString(connectionString, deviceId);
            {
                if (await IsDeviceAvailable(deviceId))
                {
                    var reportedProperties = new TwinCollection();
                    reportedProperties[properties.Key] = properties.Value;
                    await deviceClient.UpdateReportedPropertiesAsync(reportedProperties);
                    //var twin = await registryManager.GetTwinAsync(deviceId);
                    //return twin;
                    return "Reported Properties Updated Successfully";
                }
                return "Device is disabled";
            }
        }
        public async Task<string> UpdateDesiredPropertiesAsync(UpdateProperties properties, string deviceId)
        {
            var registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            {
                if (await IsDeviceAvailable(deviceId))
                {
                    var desiredProperties = new TwinCollection();
                    desiredProperties[properties.Key] = properties.Value;

                    var twin = await registryManager.GetTwinAsync(deviceId);
                    twin.Properties.Desired = desiredProperties;

                    await registryManager.UpdateTwinAsync(twin.DeviceId, twin, twin.ETag);
                    //return twin;
                    return "Desired Properties Updated Successfully";
                }
                return "Device is disabled";
            }
        }
    
}
}
