using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LightSwitch
{
	public class AwaitableCommand : AwaitableCommand<object>, IAsyncCommand
	{
		public AwaitableCommand(Func<Task> executeMethod) : base(_ => executeMethod()) { }
		public AwaitableCommand(Func<Task> executeMethod, Func<bool> canExecuteMethod) : base(_ => executeMethod(), _ => canExecuteMethod()) { }
	}

	public class AwaitableCommand<T> : IAsyncCommand<T>, ICommand
	{
		private readonly Func<T, Task> executeMethod;
		private readonly Func<T, bool> canExecuteMethod;
		private bool isExecuting;

		public event EventHandler CanExecuteChanged;

		public AwaitableCommand(Func<T, Task> executeMethod) : this(executeMethod, _ => true) { }

		public AwaitableCommand(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod)
		{
			this.executeMethod = executeMethod;
			this.canExecuteMethod = canExecuteMethod;
		}

		public async Task ExecuteAsync(T obj)
		{
			try
			{
				isExecuting = true;
				raiseCanExecuteChanged();
				await executeMethod(obj);
			}
			finally
			{
				isExecuting = false;
				raiseCanExecuteChanged();
			}
		}

		public bool CanExecute(object parameter)
		{
			return !isExecuting && canExecuteMethod((T)parameter);
		}

		public async void Execute(object parameter)
		{
			await ExecuteAsync((T)parameter);
		}

		private void raiseCanExecuteChanged()
		{
			var handler = this.CanExecuteChanged;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}
	}
}
