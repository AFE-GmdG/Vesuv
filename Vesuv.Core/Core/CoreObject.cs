using System;

namespace Vesuv.Core
{

	public abstract class CoreObject :
		IDisposable
	{

		private bool isDisposed = false;

		public CoreObject() {}

		protected virtual void Dispose(bool disposing) {
			if (!this.isDisposed) {
				if (disposing) {
				}
				this.isDisposed = true;
			}
		}

		public void Dispose() {
			this.Dispose(true);
		}

	}

}
