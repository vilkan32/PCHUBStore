using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCHUBStore.Data.Models.Enums
{
    public enum ConfirmationStatus
    {
        Confirmed,
        Rejected,
        [Display(Name = "Awaiting Response")]
        AwaitingResponse,
        [Display(Name = "Un Answered")]
        Unanswered,
        [Display(Name = "Not Confirmed")]
        NotConfirmed
    }
}
