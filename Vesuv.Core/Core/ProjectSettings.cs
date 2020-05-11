using System;
using System.Collections.Generic;
using System.Linq;

namespace Vesuv.Core
{

	public class ProjectSetting
	{

		#region Fields
		private object value;
		#endregion

		#region Properties
		public string Category { get; private set; }
		public string Name { get; private set; }
		public Type Type { get; private set; }
		public object DefaultValue { get; private set; }
		public object Value {
			get {
				if (this.value == null) {
					return this.DefaultValue;
				}
				return this.value;
			}
			set => this.value = value;
		}
		#endregion

		#region ctor
		public ProjectSetting(string category, string name, Type type, object defaultValue) {
			this.Category = category;
			this.Name = name;
			this.Type = type;
			this.DefaultValue = defaultValue;
			this.value = defaultValue;
		}

		public ProjectSetting(string category, string name, Type type, object defaultValue, object value) {
			this.Category = category;
			this.Name = name;
			this.Type = type;
			this.DefaultValue = defaultValue;
			this.value = value;
		}
		#endregion

	}


	public class ProjectSettings :
		CoreObject
	{

		#region Fields
		private readonly List<ProjectSetting> settings;
		#endregion

		#region Properties
		public static ProjectSettings Singleton { get; private set; }

		public IEnumerable<IGrouping<string, ProjectSetting>> Categories {
			get => this.settings.GroupBy(setting => setting.Category);
		}
		#endregion

		#region ctor/dtor/Dispose
		public ProjectSettings() {
			Singleton = this;
			this.settings = new List<ProjectSetting> {
				new ProjectSetting("Application/Config", "LogLevel", typeof(LogLevel), LogLevel.Warning),
				new ProjectSetting("Application/Config", "Title", typeof(String), "Vesuv Editor"),
				new ProjectSetting("Application/Config", "Width", typeof(Int32), 1280),
				new ProjectSetting("Application/Config", "Height", typeof(Int32), 720),
			};
		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
				Singleton = null;
			}
			base.Dispose(disposing);
		}
		#endregion

		#region Methods
		public Error Setup(string path, string mainPack, bool upwards) {
			return Error.NYI;
		}

		public T GetSetting<T>(string category, string name) {
			var icic = StringComparer.InvariantCultureIgnoreCase;
			var setting = this.settings.FirstOrDefault(s =>
				String.Equals(s.Category, category, StringComparison.InvariantCultureIgnoreCase) &&
				String.Equals(s.Name, name, StringComparison.InvariantCultureIgnoreCase));
			if (setting == null) {
				throw new VesuvException(Error.NotFound);
			}
			if (typeof(T).IsAssignableFrom(setting.Type)) {
				return (T)setting.Value;
			}
			throw new VesuvException(Error.NotCompatible,
				String.Format("{0} ist not compatible to {1}", typeof(T), setting.Type));
		}

		public T GetSetting<T>(string category, string name, T defaultValue) {
			var icic = StringComparer.InvariantCultureIgnoreCase;
			var setting = this.settings.FirstOrDefault(s =>
				String.Equals(s.Category, category, StringComparison.InvariantCultureIgnoreCase) &&
				String.Equals(s.Name, name, StringComparison.InvariantCultureIgnoreCase));
			if (setting == null) {
				return defaultValue;
			}
			if (typeof(T).IsAssignableFrom(setting.Type)) {
				return (T)setting.Value;
			}
			throw new VesuvException(Error.NotCompatible,
				String.Format("{0} ist not compatible to {1}", typeof(T), setting.Type));
		}

		public T GetDefaultSetting<T>(string category, string name) {
			var icic = StringComparer.InvariantCultureIgnoreCase;
			var setting = this.settings.FirstOrDefault(s =>
				String.Equals(s.Category, category, StringComparison.InvariantCultureIgnoreCase) &&
				String.Equals(s.Name, name, StringComparison.InvariantCultureIgnoreCase));
			if (setting == null) {
				throw new VesuvException(Error.NotFound);
			}
			if (typeof(T).IsAssignableFrom(setting.Type)) {
				return (T)setting.DefaultValue;
			}
			throw new VesuvException(Error.NotCompatible,
				String.Format("{0} ist not compatible to {1}", typeof(T), setting.Type));
		}

		public void SetSetting<T>(string category, string name, T value) {
			var icic = StringComparer.InvariantCultureIgnoreCase;
			var setting = this.settings.FirstOrDefault(s =>
				String.Equals(s.Category, category, StringComparison.InvariantCultureIgnoreCase) &&
				String.Equals(s.Name, name, StringComparison.InvariantCultureIgnoreCase));
			if (setting == null) {
				this.settings.Add(new ProjectSetting(category, name, typeof(T), value));
				return;
			}
			if(setting.Type.IsAssignableFrom(typeof(T))) {
				setting.Value = value;
				return;
			}
			throw new VesuvException(Error.NotCompatible,
				String.Format("{0} ist not compatible to {1}", typeof(T), setting.Type));
		}

		public bool RemoveSetting(string category, string name) {
			var icic = StringComparer.InvariantCultureIgnoreCase;
			var setting = this.settings.FirstOrDefault(s =>
				String.Equals(s.Category, category, StringComparison.InvariantCultureIgnoreCase) &&
				String.Equals(s.Name, name, StringComparison.InvariantCultureIgnoreCase));
			if(setting != null) {
				return this.settings.Remove(setting);
			}
			return false;
		}
		#endregion

	}

}
