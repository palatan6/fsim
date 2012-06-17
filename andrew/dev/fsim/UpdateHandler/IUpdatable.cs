using System;

namespace UpdateHandler
{
    public interface IUpdatable
    {
        void Refresh();
        int Value { set;  get; }
        int Maximum { set;  get; }
        object Invoke(Delegate method, params object [] parameters);
    }
}
