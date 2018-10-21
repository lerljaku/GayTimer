using GayTimer.ViewModels;
using Xamarin.Forms;

namespace GayTimer.Converters
{
    public class PlayerCntToTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var players = item as PlayerViewModel[];

            //var players = vm?.AllPlayers;
            if (players == null)
                return null;
       
            if (players.Length == 2)
                return Player2Template;

            if (players.Length == 3)
                return Player3Template;

            if (players.Length == 4)
                return Player4Template;

            if (players.Length == 5)
                return Player5Template;
           
            throw new System.NotImplementedException();
        }

        public DataTemplate Player2Template { get; set; }
        public DataTemplate Player3Template { get; set; }
        public DataTemplate Player4Template { get; set; }
        public DataTemplate Player5Template { get; set; }
    }
}