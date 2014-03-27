using System;
using BloombergWebAPICore.IWebApi;

namespace BloombergWebAPICore.Dto
{
    public sealed class ReferenceDataRequestContract : RequestContract
    {
        public ReferenceDataRequestContract()
        {
            
        }

        // currently no additional functionality over abstract class but this could change with future request

        public override BloombergRequestServiceTypes RequestType
        {
            get
            {
                return BloombergRequestServiceTypes.ReferenceDataRequest;
            }
        }

        public bool ReturnEids { get; set; }

        public bool ReturnFormattedValue { get; set; }

        public bool UseUTCTime { get; set; }

        public bool ForceDelay { get; set; }
    }

    public sealed class HistoricalDataRequestContract : RequestContract
    {
        public enum Periodicity
        {
            Actual,
            Calendar,
            Fiscal
        }

        private Periodicity _periodicity;
        public HistoricalDataRequestContract()
        {
            
        }

        public override BloombergRequestServiceTypes RequestType
        {
            get { return BloombergRequestServiceTypes.HistoricalDataRequest; }
        }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        // Forces user to use set values to set property, but serializes back as string
        public string PeriodicityAdjustment { get { return Enum.GetName(typeof (Periodicity),_periodicity); } }
        public void SetPeriodicityAdjustment(Periodicity periodicity)
        {
            _periodicity = periodicity;
        }
        // currently no additional functionality over abstract class but this could change with future request
    }
}
