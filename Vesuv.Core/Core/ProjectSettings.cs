using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vesuv.Core
{

	public class ProjectSetting
	{
		public string Category { get; private set; }
		public string Name { get; private set; }
		public Type Type { get; private set; }
		public object DefaultValue { get; private set; }

		private object value;
		public object Value {
			get {
				if (this.value == null) {
					return this.DefaultValue;
				}
				return this.value;
			}
			set => this.value = value;
		}

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

	}


	public class ProjectSettings :
		CoreObject
	{

		private readonly List<ProjectSetting> settings;

		public static ProjectSettings Singleton { get; private set; }

		public IEnumerable<IGrouping<string, ProjectSetting>> Categories {
			get => this.settings.GroupBy(setting => setting.Category);
		}

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
	}

}
