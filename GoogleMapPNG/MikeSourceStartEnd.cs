//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GoogleMapPNG
{
    using System;
    using System.Collections.Generic;
    
    public partial class MikeSourceStartEnd
    {
        public int MikeSourceStartEndID { get; set; }
        public int MikeSourceID { get; set; }
        public System.DateTime StartDateAndTime_Local { get; set; }
        public System.DateTime EndDateAndTime_Local { get; set; }
        public double SourceFlowStart_m3_day { get; set; }
        public double SourceFlowEnd_m3_day { get; set; }
        public int SourcePollutionStart_MPN_100ml { get; set; }
        public int SourcePollutionEnd_MPN_100ml { get; set; }
        public double SourceTemperatureStart_C { get; set; }
        public double SourceTemperatureEnd_C { get; set; }
        public double SourceSalinityStart_PSU { get; set; }
        public double SourceSalinityEnd_PSU { get; set; }
        public System.DateTime LastUpdateDate_UTC { get; set; }
        public int LastUpdateContactTVItemID { get; set; }
    
        public virtual MikeSource MikeSource { get; set; }
    }
}