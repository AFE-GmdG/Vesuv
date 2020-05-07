using Vesuv.Core;

namespace Vesuv.Server
{

	public abstract class DisplayServer :
		CoreObject
	{

		private readonly Logger logger;

		public static DisplayServer Singleton { get; private set; }

		protected DisplayServer() {
			Singleton = this;
			this.logger = Logger.GetFor();

			this.logger.Log("Initialize DisplayServer");
		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
				this.logger.Log("Destroy DisplayServer");
				this.logger.Dispose();
				Singleton = null;
			}
			base.Dispose(disposing);
		}

	}

}
