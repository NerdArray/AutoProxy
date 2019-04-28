using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
                var conditions = new List<string>();
                var filterParts = filters.Split(',');
                foreach (var f in filterParts)
                {
                    conditions.Add(f);
                }
                return conditions.ToArray();
            }
            return null;
        }
    }
}
