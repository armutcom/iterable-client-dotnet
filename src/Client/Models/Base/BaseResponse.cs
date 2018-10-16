namespace Armut.Iterable.Client.Models.Base
{
    public abstract class BaseResponse
    {
        public string Msg { get; set; }

        public string Code { get; set; }

        public object Params { get; set; }
    }
}