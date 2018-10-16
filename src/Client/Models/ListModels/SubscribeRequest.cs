using Armut.Iterable.Client.Models.UserModels;

namespace Armut.Iterable.Client.Models.ListModels
{
    public class SubscribeRequest
    {
        public int ListId { get; set; }

        public UserModel[] Subscribers { get; set; }
    }
}