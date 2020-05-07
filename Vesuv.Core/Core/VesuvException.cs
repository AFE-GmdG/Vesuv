using System;

namespace Vesuv.Core
{
	public class VesuvException :
		Exception
	{

		public Error Error { get; private set; }

		public VesuvException(Error error) {
			this.Error = error;
		}
		public VesuvException(Error error, string message) :
			base(message) {
			this.Error = error;
		}
		public VesuvException(Error error, string message, Exception innerException) :
			base(message, innerException) {
			this.Error = error;
		}

	}
}
