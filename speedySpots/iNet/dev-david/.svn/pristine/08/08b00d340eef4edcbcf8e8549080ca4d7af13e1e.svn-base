using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SpeedySpots.DataAccess;
using SpeedySpots.Business.Models;

namespace SpeedySpots.Business.Services
{
    public static class MusicSamplesService
    {
        public static IAMusic GetMusicSample(int id, DataAccessDataContext context)
        {
            IAMusic musicSample = (from n in context.IAMusics
                                   where n.IAMusicID == id
                                   select n).SingleOrDefault();
            return musicSample;
        }

    }
}
