namespace N4CoreLite.Responses.Bases
{
    public class Response<TResponseType> : IResponseData<TResponseType>
	{
		public bool IsSuccessful { get; set; }
		public string Message { get; set; }
        public TResponseType Data { get; }

		public Response(bool isSuccessful, string message, TResponseType data)
		{
			IsSuccessful = isSuccessful;
			Message = message;
			Data = data;
		}
	}
}
