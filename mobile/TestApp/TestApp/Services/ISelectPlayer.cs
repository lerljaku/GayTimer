using System.Linq;
using System.Threading.Tasks;
using GayTimer.Entities;
using XF.Material.Forms.UI.Dialogs;

namespace GayTimer.Services
{
    public interface ISelectPlayer
    {
        Player Selected { get; }

        Task<bool> Start();
    }

    public class PlayerSelector : ISelectPlayer
    {
        private readonly IDataService m_dataService;

        public PlayerSelector(IDataService dataService)
        {
            m_dataService = dataService;
        }

        public Player Selected { get; private set; }

        public async Task<bool> Start()
        {
            var players = await m_dataService.SelectPlayers();

            var actions = players.Select(d => d.Nick).ToArray();

            if (!actions.Any())
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "There are no players.", actionButtonText: "Got it", msDuration: 3000);
                return false;
            }
            var result = await MaterialDialog.Instance.SelectActionAsync(actions: actions);
            if (result >= 0)
            {
                Selected = players.ElementAt(result);
                return true;
            }

            return false;
        }
    }
}