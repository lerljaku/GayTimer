using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GayTimer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeckDetailView : ContentPage
    {
        public DeckDetailView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            NameField.Focus();
        }
    }
}