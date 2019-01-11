using Armut.Iterable.Client.Models.Base;
using System.Dynamic;

namespace Armut.Iterable.Client.Models.EventModels
{
    public class EventTrackRequest : BaseUserModel
    {
        public string EventName { get; set; }

        public dynamic DataFields { get; set; } = new ExpandoObject();

        public int CampaingId { get; set; }

        public int TemplateId { get; set; }
    }
}