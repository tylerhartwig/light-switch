using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.Forms;

namespace LightSwitch
{
	public class DynamicListBehavior : Behavior<TableView>
	{
		public IEnumerable ItemsSource
		{
			get { return (IEnumerable)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}
		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource),
		                                                                                      typeof(IEnumerable),
		                                                                                      typeof(DynamicListBehavior),
		                                                                                      propertyChanged: onItemsSourceChanged);
		public DataTemplate ItemTemplate
		{
			get { return (DataTemplate)GetValue(ItemTemplateProperty); }
			set { SetValue(ItemTemplateProperty, value); }
		}
		public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate),
																							   typeof(DataTemplate),
																							   typeof(DynamicListBehavior));

		public string TableSectionName
		{
			get { return (string)GetValue(TableSectionNameProperty); }
			set { SetValue(TableSectionNameProperty, value); }
		}
		public static readonly BindableProperty TableSectionNameProperty = BindableProperty.Create(nameof(TableSectionName),
		                                                                                           typeof(string),
		                                                                                           typeof(DynamicListBehavior), 
		                                                                                           propertyChanged: onTableSectionNameChanged);

		private TableSection tableSection;
		private TableView tableView;

		protected override void OnAttachedTo(TableView bindable)
		{
			base.OnAttachedTo(bindable);
			tableView = bindable;
			setSectionByName(this);
			bindable.BindingContextChanged += (sender, _) =>
			{
				this.BindingContext = ((BindableObject)sender).BindingContext;
			};
		}

		protected override void OnDetachingFrom(TableView bindable)
		{
			base.OnDetachingFrom(bindable);
			tableView = null;
		}

		private static void onTableSectionNameChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var dynamicListBehavior = (DynamicListBehavior)bindable;
			setSectionByName(dynamicListBehavior);
		}

		private static void setSectionByName(DynamicListBehavior dynamicListBehavior)
		{
			if (dynamicListBehavior.tableView != null)
			{
				dynamicListBehavior.tableSection = dynamicListBehavior.tableView.FindByName<TableSection>(dynamicListBehavior.TableSectionName);
			}		
		}

		private static void onItemsSourceChanged(BindableObject bindable, object genericOldValue, object genericNewValue)
		{
			var dynamicListBehavior = (DynamicListBehavior)bindable;

			if (genericNewValue != null)
			{
				((INotifyCollectionChanged)genericNewValue).CollectionChanged += dynamicListBehavior.onItemsSourceCollectionChanged;
			}

			if (genericOldValue != null)
			{
				((INotifyCollectionChanged)genericOldValue).CollectionChanged -= dynamicListBehavior.onItemsSourceCollectionChanged;
			}
		}

		private void onItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					foreach (var item in e.NewItems)
					{
						var cell = (Cell)ItemTemplate.CreateContent();
						cell.BindingContext = item;
						tableSection.Insert(tableSection.Count - 1, cell);
					}
					break;
			}
		}
	}
}
