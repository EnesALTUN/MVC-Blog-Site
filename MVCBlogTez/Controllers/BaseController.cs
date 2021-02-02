using MVCBlogTez.Context;
using System.Web.Mvc;

namespace MVCBlogTez.Controllers
{
    public class BaseController : Controller
    {
        public readonly BlogContext blogContext = new BlogContext();
    }
}