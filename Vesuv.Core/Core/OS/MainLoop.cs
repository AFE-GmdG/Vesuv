using System.Diagnostics.CodeAnalysis;

namespace Vesuv.Core.OS
{

	public abstract class MainLoop :
		CoreObject
	{

		protected MainLoop() { }

		protected override void Dispose(bool disposing) {
			if (disposing) {
			}
			base.Dispose(disposing);
		}

		public void Init() { }

		[SuppressMessage("Style", "IDE0060: Nicht verwendete Parameter entfernen")]
		public bool Iteration(float time) { return false; }

		[SuppressMessage("Style", "IDE0060: Nicht verwendete Parameter entfernen")]
		public bool Idle(float time) { return false; }

		public void Finish() { }

	}

}
