namespace Armut.Iterable.Client.Models.UserModels
{
    public abstract class BaseResponse
    {
        public string Msg { get; set; }

        public string Code { get; set; }

        public object Params { get; set; }
    }
}