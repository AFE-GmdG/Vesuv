using System.Diagnostics;

namespace Vesuv.Core
{

	public class Logger :
		CoreObject
	{

		private readonly string context;

		public Logger(string context) {
			this.context = context;
		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
			}
			base.Dispose(disposing);
		}

		public static Logger GetFor() {
			var stackTrace = new StackTrace(1, true);
			var context = stackTrace.GetFrame(0).GetMethod().DeclaringType.FullName;
			return GetFor(context);
		}
		public static Logger GetFor(string context) {
			return new Logger(context);
		}


		public void Log() {
			Debug.WriteLine("");
		}
		public void Log(string message) {
			Debug.WriteLine(message, this.context);
		}

	}

}
