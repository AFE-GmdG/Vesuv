using System;

namespace Vesuv.Core
{

	public abstract class CoreObject :
		IDisposable
	{

		#region Fields
		private bool isDisposed = false;
		#endregion

		#region ctor/dtor/Dispose
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
		#endregion

	}

}
