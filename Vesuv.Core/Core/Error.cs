namespace Vesuv.Core
{

	public enum Error
	{
		Ok = 0,
		Failed,

		NotFound = 100,
		NotCompatible,
		Unavailable,
		Unconfigured,
		Unauthorized,

		// Not yet implemented.
		NYI = 99999,
	}

}
