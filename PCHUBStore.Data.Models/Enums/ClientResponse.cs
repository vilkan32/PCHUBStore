using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCHUBStore.Data.Models.Enums
{
    public enum ClientResponse
    {
        Confirmed,
        [Display(Name = "Un Confirmed")]
        Unconfirmed,
        [Display(Name = "Awaiting Response")]
        AwaitingResponse,
        [Display(Name = "Un Answered")]
        Unanswered
    }
}
