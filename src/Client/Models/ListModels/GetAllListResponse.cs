using System.Collections.Generic;

namespace Armut.Iterable.Client.Models.ListModels
{
    public class GetAllListResponse
    {
        public IEnumerable<GetAllListModel> Lists { get; set; }        
    }
}