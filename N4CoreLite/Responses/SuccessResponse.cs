#nullable disable

using N4CoreLite.Responses.Bases;

namespace N4CoreLite.Responses
{
	public class SuccessResponse<TResponseType> : Response<TResponseType>
	{
		public SuccessResponse(string message, TResponseType data) : base(true, message, data)
		{
		}

		public SuccessResponse(string message) : base(true, message, default)
		{
		}

		public SuccessResponse(TResponseType data) : base(true, string.Empty, data)
		{
		}

		public SuccessResponse() : base(true, string.Empty, default)
		{
		}
	}
}
