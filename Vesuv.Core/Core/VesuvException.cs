using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
