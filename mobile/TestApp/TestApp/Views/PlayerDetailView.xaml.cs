using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GayTimer.Entities;
using GayTimer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GayTimer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerDetailView 
    {
        public PlayerDetailView()
        {
            InitializeComponent();
        }
        
        protected override void OnAppearing()
        {
            NickField.Focus();
        }
    }

    public class DeckTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DecksTemplate { get; set; }
        public DataTemplate NoDecksTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var decks = (PlayerDetailViewModel)item;

            if (decks?.Decks == null || !decks.Decks.Any())
                return NoDecksTemplate;
            else
            {
                return DecksTemplate;
            }
        }
    }
}