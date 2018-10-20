using System;
using Xamarin.Forms;

namespace GayTimer.Controls
{
    public class ContentControl : ContentView
    {
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create("ItemTemplate",
            typeof(DataTemplate), typeof(ContentControl), null, propertyChanged: OnItemTemplateChanged);
        
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public static readonly BindableProperty TemplateSelectorProperty = BindableProperty.Create("TemplateSelector", 
            typeof(DataTemplateSelector), typeof(ContentControl), null, propertyChanged: OnTemplateSelectorChanged);

        public DataTemplateSelector TemplateSelector
        {
            get => (DataTemplateSelector) GetValue(TemplateSelectorProperty);
            set => SetValue(TemplateSelectorProperty, value);
        }

        private static void OnTemplateSelectorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var cc = (ContentControl)bindable;

            var selector = cc.TemplateSelector;
            if (selector != null)
            {
                var content = (View) selector.SelectTemplate(cc.BindingContext, null).CreateContent();
                cc.Content = content;
            }
            else
            {
                cc.Content = null;
            }
        }

        private static void OnItemTemplateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var cc = (ContentControl) bindable;

            var template = cc.ItemTemplate;
            if (template != null)
            {
                var content = (View) template.CreateContent();
                cc.Content = content;
            }
            else
            {
                cc.Content = null;
            }
        }
    }
}