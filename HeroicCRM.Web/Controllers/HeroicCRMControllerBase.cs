using System.Web.Mvc;
using HeroicCRM.Web.ActionResults;

namespace HeroicCRM.Web.Controllers
{
	public abstract class HeroicCRMControllerBase : Controller
	{
		public CustomJsonResult<T> CustomJson<T>(T model)
		{
			return new CustomJsonResult<T>() { Data = model };
		}
	}
}