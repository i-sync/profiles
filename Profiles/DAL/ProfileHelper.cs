using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Profiles.Models;

namespace Profiles.DAL
{
    public class ProfileHelper : DbContextHelper<Profile>
    {
        public ProfileHelper(SqlConnectionSetting setting)
            : base(setting)
        { }

    }
}