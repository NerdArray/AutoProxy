using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AutoProxy.Models
{
    public abstract class ApiControllerBase : ControllerBase
    {
        protected ApiControllerBase() : base()
        {
        }

        protected IEnumerable<string> ParseFilters(string filters)
        {
            if (filters != null)
            {
                var conditions = filters.Split("+").Select(f => f).ToList();
                return conditions;
            }
            return null;
        }
    }
}
