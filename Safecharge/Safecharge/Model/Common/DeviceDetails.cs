using Safecharge.Utils;

namespace Safecharge.Model.Common
{
    /// <summary>
    /// Holder for device details information.
    /// </summary>
    /// <remarks>
    /// Supported device types include: DESKTOP, SMARTPHONE, TABLET, TV, UNKNOWN (if device type cannot be recognised).
    /// </remarks>
    public class DeviceDetails
    {
        private string deviceType;
        private string deviceName;
        private string deviceOS;
        private string browser;
        private string ipAddress;

        public string DeviceType
        {
            get { return this.deviceType; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 10, nameof(this.DeviceType));
                this.deviceType = value;
            }
        }

        public string DeviceName
        {
            get { return this.deviceName; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.DeviceName));
                this.deviceName = value;
            }
        }

        public string DeviceOS
        {
            get { return this.deviceOS; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.DeviceOS));
                this.deviceOS = value;
            }
        }

        public string Browser
        {
            get { return this.browser; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.Browser));
                this.browser = value;
            }
        }

        public string IpAddress
        {
            get { return this.ipAddress; }
            set
            {
                Guard.RequiresPattern(value, Constants.PatternIpAddress, nameof(this.IpAddress));
                this.ipAddress = value;
            }
        }
    }
}
