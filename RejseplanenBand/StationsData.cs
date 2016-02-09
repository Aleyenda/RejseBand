using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejseplanenBand
{
    public class StationsData : ObservableCollection<string>
    {
        public StationsData()
        {
            Add("Charlotenlund St");
            Add("Lyngby St");
            Add("Norreport St");
        }
    }
}
