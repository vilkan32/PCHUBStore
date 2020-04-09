using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PCHUBStore.Areas.Support.Controllers
{
    [Authorize(Roles = "Admin, Support")]
    [Area("Support")]
    public class SupportController : Controller
    {
    }
}