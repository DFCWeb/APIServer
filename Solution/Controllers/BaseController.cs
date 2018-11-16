using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Solution.DataDefinitions;
using Solution.DataDefinitions.Entities;
using Solution.DataDefinitions.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Solution.Controllers
{
    public class BaseController : ControllerBase
    {
        public int CurrentUserId { get; set; }


        public void TrackCreation(TrackableEntity entity)
        {
            if(entity != null)
            {
                entity.CreatedBy = new User() { ID = HttpContext == null? CurrentUserId : HttpContext.User.Identity.Name.ToInt() };
                entity.CreatedDateTime = DateTime.Now;
            }
        }
        public void TrackModification(TrackableEntity entity)
        {
            if (entity != null)
            {
                entity.UpdatedBy = new User() { ID = HttpContext == null ? CurrentUserId : HttpContext.User.Identity.Name.ToInt() };
                entity.UpdatedDateTime = DateTime.Now;
            }
        }
    }
}
