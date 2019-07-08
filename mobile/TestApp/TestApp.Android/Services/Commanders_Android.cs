using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GayTimer.Services;

namespace TestApp.Droid.Services
{
    public class Commanders_Android : ICommanders
    {
        private List<string> m_commanders;

        public List<string> GetCommanderList()
        {
            if (m_commanders == null)
            {
                using (var str = Android.App.Application.Context.Assets.Open("commanders.txt"))
                {
                    using (var sr = new StreamReader(str))
                    {
                        m_commanders = sr.ReadToEnd().Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).Select(d => d.Trim()).ToList();
                    }
                }
            }

            return m_commanders;
        }
    }
}