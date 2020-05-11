using System;
using System.Diagnostics;
using System.IO;

namespace Vesuv.Core.OS
{

	public abstract class OS :
		IDisposable
	{

		#region Fields
		private readonly Logger logger;

		private bool isDisposed = false;

		protected MainLoop mainLoop;
		#endregion

		#region Properties
		public static OS Singleton { get; private set; }

		public string Executable { get; private set; }
		public string[] Parameter { get; private set; }

		public virtual int ExitCode { get; protected set; }
		public virtual int ProcessorCount { get { return 1; } }
		#endregion

		#region ctor/dtor/Dispose
		protected OS(ref string executable, ref string[] parameter) {
			Singleton = this;
			this.logger = Logger.GetFor();
			this.mainLoop = null;
			this.Executable = executable;
			this.Parameter = parameter;

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
		#endregion

		#region Abstract Methods
		public abstract void Initialize();
		public abstract ulong GetTicksUsec();
		#endregion

		#region Methods
		public virtual string GetSystemDir(SystemDir directory) {
			return ".";
		}

		public virtual Error SetCwd(string cwd) {
			return Error.CantOpen;
		}
		#endregion

	}

}
