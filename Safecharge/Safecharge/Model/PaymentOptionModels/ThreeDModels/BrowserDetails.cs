using Safecharge.Utils;

namespace Safecharge.Model.PaymentOptionModels.ThreeDModels
{
    /// <summary>
    /// Holder for browser details information.
    /// </summary>
    public class BrowserDetails
    {
        private string acceptHeader;
        private string ip;
        private string language;
        private string colorDepth;
        private string screenHeight;
        private string screenWidth;
        private string timeZone;
        private string userAgent;

        public string AcceptHeader
        {
            get { return this.acceptHeader; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2048, nameof(this.AcceptHeader));
                this.acceptHeader = value;
            }
        }

        public string Ip
        {
            get { return this.ip; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 45, nameof(this.Ip));
                this.ip = value;
            }
        }

        public string JavaEnabled { get; set; }

        public string JavaScriptEnabled { get; set; }

        public string Language
        {
            get { return this.language; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 8, nameof(this.Language));
                this.language = value;
            }
        }

        public string ColorDepth
        {
            get { return this.colorDepth; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.ColorDepth));
                this.colorDepth = value;
            }
        }

        public string ScreenHeight
        {
            get { return this.screenHeight; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 6, nameof(this.ScreenHeight));
                this.screenHeight = value;
            }
        }

        public string ScreenWidth
        {
            get { return this.screenWidth; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 6, nameof(this.ScreenWidth));
                this.screenWidth = value;
            }
        }

        public string TimeZone
        {
            get { return this.timeZone; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 5, nameof(this.TimeZone));
                this.timeZone = value;
            }
        }

        public string UserAgent
        {
            get { return this.userAgent; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2048, nameof(this.UserAgent));
                this.userAgent = value;
            }
        }
    }
}
