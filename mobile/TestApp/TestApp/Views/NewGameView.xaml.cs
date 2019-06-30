using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GayTimer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GayTimer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewGameView 
    {
        public NewGameView()
        {
            InitializeComponent();
        }
    }

    public class GamePageSelector : DataTemplateSelector
    {
        public DataTemplate SelectLifeTotalTemplate { get; set; }
        public DataTemplate SelectPlayerCountTemplate { get; set; }
        public DataTemplate GameTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            switch (item)
            {
                case SelectLifeTotalViewModel lifeTotalVm:
                    return SelectLifeTotalTemplate;
                case SelectPlayerCountViewModel playerCntVm:
                    return SelectPlayerCountTemplate;
                case GamePageViewModel gameVm:
                    return GameTemplate;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}