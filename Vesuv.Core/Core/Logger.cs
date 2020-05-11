using System.Diagnostics;

namespace Vesuv.Core
{

	// Todo: Implement a better Logging Solution
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


		public virtual void Log() {
			Debug.WriteLine("");
		}
		public virtual void Log(string message) {
			Debug.WriteLine(message, this.context);
		}

		public virtual void Warn() {
			Debug.WriteLine("");
		}
		public virtual void Warn(string message) {
			Debug.WriteLine(message, this.context);
		}

		public virtual void Error() {
			Debug.WriteLine("");
		}
		public virtual void Error(string message) {
			Debug.WriteLine(message, this.context);
		}
	}

}
