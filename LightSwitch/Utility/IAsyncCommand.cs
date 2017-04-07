using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LightSwitch
{
	public interface IAsyncCommand : IAsyncCommand<object> { }

	public interface IAsyncCommand<in T>
	{
		Task ExecuteAsync(T obj);
		bool CanExecute(object obj);
	}
}