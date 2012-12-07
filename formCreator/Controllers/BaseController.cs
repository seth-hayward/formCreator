using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client;

// base controller used from RavenDBTest sample code provided by AppHarbor
// https://github.com/friism/RavenDBMvcSample/blob/master/RavenDBTest/Controllers/BaseController.cs
namespace formCreator.Controllers
{
    public abstract class BaseController : Controller
    {
        public IDocumentSession RavenSession { get; private set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            RavenSession = MvcApplication.documentStore.OpenSession();
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;

            using (RavenSession)
            {
                if (filterContext.Exception != null)
                    return;

                if (RavenSession != null)
                    RavenSession.SaveChanges();
            }
        }
    }
}
