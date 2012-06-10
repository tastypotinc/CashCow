using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stocks10DMA.Entities
{
    public class WatchListEntry
    {
        public int Id { get; set; }

        public string BSESymbol { get; set; }

        public string NSESymbol { get; set; }

        public string Name { get; set; }

        public string AltName1 { get; set; }

        public string AltName2 { get; set; }

        public string TempName { get; set; }

        public int Active { get; set; }

        public int Alert { get; set; }

        public int ModifiedOn { get; set; }

        public int CreatedOn { get; set; }
    }
}
