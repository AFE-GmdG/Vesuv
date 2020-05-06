using System;

namespace Vesuv.Core.OS
{

	public abstract class OS :
		IDisposable
	{

		private readonly Logger logger;
		private readonly string executable;
		private readonly string[] parameter;

		private bool isDisposed = false;

		protected MainLoop mainLoop;

		public static OS Singleton { get; private set; }

		public virtual int ExitCode { get; protected set; }
		public virtual int ProcessorCount { get { return 1; } }

		protected OS(ref string executable, ref string[] parameter) {
			Singleton = this;
			this.logger = Logger.GetFor();
			this.executable = executable;
			this.parameter = parameter;
			this.mainLoop = null;

			this.logger.Log("Initialize OS");
		}


		protected virtual void Dispose(bool disposing) {
			if(!this.isDisposed) {
				if(disposing) {
					this.logger.Log("Destroy OS");
					this.logger.Dispose();
					Singleton = null;
				}
				this.isDisposed = true;
			}
		}
		public void Dispose() {
			this.Dispose(true);
		}


		protected abstract void Initialize();
		protected abstract ulong GetTicksUsec();


		public virtual string GetSystemDir(SystemDir directory) {
			return ".";
		}

	}

}
