namespace N4CoreLite.Responses.Bases
{
	public interface IResponseData<out TResponseType>
	{
        public TResponseType Data { get; }
    }
}
