#nullable disable

using N4CoreLite.Responses.Bases;

namespace N4CoreLite.Responses
{
	public class ErrorResponse<TResponseType> : Response<TResponseType>
	{
		public ErrorResponse(string message, TResponseType data) : base(false, message, data)
		{
		}

		public ErrorResponse(string message) : base(false, message, default)
		{
		}

		public ErrorResponse(TResponseType data) : base(false, "İşlem gerçekleştirilemedi!", data)
		{
		}

		public ErrorResponse() : base(false, "İşlem gerçekleştirilemedi!", default)
		{
		}
	}
}
