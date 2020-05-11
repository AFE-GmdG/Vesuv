using System;

namespace Vesuv.Core
{

	public class Engine :
		IDisposable
	{

		#region Fields
		private readonly Logger logger;

		private bool isDisposed = false;

		private ulong framesDrawn;
		private uint interactionsPerSecond;
		private float fps;
		private uint targetFps;
		private bool pixelSnap;
		#endregion

		#region Properties
		public static Engine Singleton { get; private set; }

#if RUNTIME_ONLY
		public bool EditorHint { get { return false; } set { } }
#else
		public bool EditorHint { get; set; }
#endif

		public uint InteractionsPerSecond {
			get => this.interactionsPerSecond;
			set {
				if (value == 0) {
					this.logger.Warn("Engine interactions per second must be greater than 0.");
					return;
				}
				this.interactionsPerSecond = value;
			}
		}

		#endregion

		#region ctor/dtor/Dispose
		public Engine() {
			Singleton = this;
			this.logger = Logger.GetFor();

			this.logger.Log("Initialize Engine (ctor)");

			this.framesDrawn = 0;
			this.interactionsPerSecond = 60;
			this.fps = 1;
			this.targetFps = 0;
			this.pixelSnap = false;
		}

		protected virtual void Dispose(bool disposing) {
			if (!this.isDisposed) {
				if (disposing) {
					this.logger.Log("Destroy Engine");
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

	}

}
