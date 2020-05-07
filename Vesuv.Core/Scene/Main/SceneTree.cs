using Vesuv.Core.OS;

namespace Vesuv.Scene.Main
{

	class SceneTree :
		MainLoop
	{

		public SceneTree() :
			base() {
		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
			}
			base.Dispose(disposing);
		}

	}

}
