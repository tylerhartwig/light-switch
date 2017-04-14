using System;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace LightSwitch
{
	public class EventToCommandBehavior : Behavior<VisualElement>
	{
		Delegate eventHandler;
		VisualElement associatedObject;

		public static readonly BindableProperty EventNameProperty = BindableProperty.Create(nameof(EventName), typeof(string), typeof(EventToCommandBehavior), propertyChanged: onEventNameChanged);
		public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(EventToCommandBehavior));
		public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(EventToCommandBehavior));
		public static readonly BindableProperty InputConverterProperty = BindableProperty.Create(nameof(Converter), typeof(IValueConverter), typeof(EventToCommandBehavior));

		public string EventName
		{
			get { return (string)GetValue(EventNameProperty); }
			set { SetValue(EventNameProperty, value); }
		}

		public object CommandParameter
		{
			get { return GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		public IValueConverter Converter
		{
			get { return (IValueConverter)GetValue(InputConverterProperty); }
			set { SetValue(InputConverterProperty, value); }
		}

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		protected override void OnAttachedTo(VisualElement bindable)
		{
			base.OnAttachedTo(bindable);
			associatedObject = bindable;

			bindable.BindingContextChanged += onBindingContextChanged;

			registerEvent(EventName);
		}

		protected override void OnDetachingFrom(VisualElement bindable)
		{
			base.OnDetachingFrom(bindable);
			associatedObject = null;

			bindable.BindingContextChanged -= onBindingContextChanged;

			unregisterEvent(EventName);
		}

		void onBindingContextChanged(object sender, EventArgs e)
		{
			var bindable = (BindableObject)sender;
			this.BindingContext = bindable.BindingContext;
		}


		private void registerEvent(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return;
			}

			EventInfo eventInfo = associatedObject.GetType().GetRuntimeEvent(name);
			if (eventInfo == null)
			{
				throw new ArgumentException(string.Format("EventToCommandBehavior: Can't register the '{0}' event.", EventName));
			}

			MethodInfo methodInfo = typeof(EventToCommandBehavior).GetTypeInfo().GetDeclaredMethod(nameof(onEvent));
			eventHandler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);
			eventInfo.AddEventHandler(associatedObject, eventHandler);
		}

		private void unregisterEvent(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return;
			}

			if (eventHandler == null)
			{
				return;
			}

			var eventInfo = associatedObject.GetType().GetRuntimeEvent(name);
			if (eventInfo == null)
			{
				throw new ArgumentException(string.Format("EventToCommandBehavior: Can't de-register the '{0}' event.", EventName));
			}

			eventInfo.RemoveEventHandler(associatedObject, eventHandler);
			eventHandler = null;
		}

		private void onEvent(object sender, object eventArgs)
		{
			if (Command == null)
			{
				return;
			}

			object resolvedParameter;
			resolvedParameter = eventArgs;

			if (Command.CanExecute(resolvedParameter))
			{
				Command.Execute(resolvedParameter);
			}
		}

		private static void onEventNameChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var behavior = (EventToCommandBehavior)bindable;
			if (behavior.associatedObject == null)
			{
				return;
			}

			string oldEventName = (string)oldValue;
			string newEventName = (string)newValue;

			behavior.unregisterEvent(oldEventName);
			behavior.registerEvent(newEventName);
		}
	}
}
