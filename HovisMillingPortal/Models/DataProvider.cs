using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HovisMillingPortal.Models
{

    public static class DataProvider
    {
        private static HovisMillingPortalEntities db = new HovisMillingPortalEntities();
        public static IQueryable<t_Milling_Plant> StaticPlantList { get { return db.t_Milling_Plant; } }

        public static IQueryable<t_Milling_Site> StaticSiteList { get { return db.t_Milling_Site; } }

    }
}