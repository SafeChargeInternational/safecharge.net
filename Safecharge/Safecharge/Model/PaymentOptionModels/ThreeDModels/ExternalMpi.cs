using Safecharge.Utils;
using System.Collections.Generic;

namespace Safecharge.Model.PaymentOptionModels.ThreeDModels
{
    /// <summary>
    /// Allows the merchant to provide the 3D Secure authentication result as input. 
    /// </summary>
    public class ExternalMpi
    {
        private string isExternalMpi;
        private string eci;
        private string cavv;
        private string xid;
        private string dsTransID;
        private string threeDProtocolVersion;

        public string IsExternalMpi
        {
            get { return this.isExternalMpi; }
            set
            {
                Guard.RequiresNotNull(value, nameof(this.isExternalMpi));
                Guard.RequiresLengthBetween(value.Length, Constants.MinLengthStringDefault, 1, nameof(this.IsExternalMpi));
                this.isExternalMpi = value;
            }
        }

        public string Eci
        {
            get { return this.eci; }
            set
            {
                Guard.RequiresNotNull(value, nameof(this.isExternalMpi));
                Guard.RequiresLengthBetween(value.Length, Constants.MinLengthStringDefault, 2, nameof(this.Eci));
                this.eci = value;
            }
        }


        public string Cavv
        {
            get { return this.cavv; }
            set
            {
                Guard.RequiresNotNull(value, nameof(this.isExternalMpi));
                Guard.RequiresLengthBetween(value.Length, Constants.MinLengthStringDefault, 50, nameof(this.Cavv));
                this.cavv = value;
            }
        }

        public string Xid
        {
            get { return this.xid; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 50, nameof(this.Xid));
                this.xid = value;
            }
        }

        public string DsTransID
        {
            get { return this.dsTransID; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 36, nameof(this.DsTransID));
                this.dsTransID = value;
            }
        }

        public string ThreeDProtocolVersion
        {
            get { return this.threeDProtocolVersion; }
            set
            {
                Guard.RequiresAllowedValues(value, new List<string> { "1", "2" }, nameof(this.ThreeDProtocolVersion));

                this.threeDProtocolVersion = value;
            }
        }
    }
}
