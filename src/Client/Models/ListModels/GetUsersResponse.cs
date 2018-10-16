using System.Collections.Generic;

namespace Armut.Iterable.Client.Models.ListModels
{
    public class GetUsersResponse
    {
        public IEnumerable<string> UserIds { get; set; }
    }
}