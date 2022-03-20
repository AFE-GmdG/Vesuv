using System.Collections;

namespace Vesuv.Core.Config
{
    public abstract class ConfigBase
    {

        public bool IsModified { get; protected set; } = false;

        protected virtual void OnConfigChange(object? sender, EventArgs e)
        {
            IsModified = true;
        }

        public abstract void RevertChanges();
        public abstract void SaveChanges();
    }
}
