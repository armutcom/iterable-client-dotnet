namespace Armut.Iterable.Client.Models.ListModels
{
    public class UnsubscribeRequest
    {
        public int ListId { get; set; }

        public int CampaignId { get; set; }

        public bool ChannelUnsubsribe { get; set; }

        public SubscriberModel[] Subscribers { get; set; }
    }
}