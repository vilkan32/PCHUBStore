using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCHUBStore.Data.Models.Enums
{
    public enum ActivityType
    {
        [Display(Name = "Email Inbound")]
        EmailInbound,
        Chat,
        Web,
        Issue,
        Shipment,
        Service,
        [Display(Name = "Call Inbound")]
        CallInbound,
        [Display(Name = "Call Outbound")]
        CallOutbound,
        [Display(Name = "Open In Error")]
        OpenInError,
    }
}
