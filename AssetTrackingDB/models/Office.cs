using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTrackingDB.models
{
    public abstract class Office
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="officeName"></param>

        public Office(string officeName)
        {
            OfficeName = officeName;
        }

        //Property with get and set method
        public string OfficeName { get; set; }
    }
}

