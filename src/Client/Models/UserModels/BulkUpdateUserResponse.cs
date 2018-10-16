namespace Armut.Iterable.Client.Models.UserModels
{
    public class BulkUpdateUserResponse
    {
        public int SuccessCount { get; set; }

        public int FailCount { get; set; }

        public string[] InvalidEmails { get; set; }

        public string[] InvalidUserIds { get; set; }
    }
}