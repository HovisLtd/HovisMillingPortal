using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.ComponentModel.DataAnnotations;

namespace HovisMillingPortal.ViewModels
{
    public class CheckBoxListItemObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class SiteAuthModel
    {
        public string Id { get; set; }
        public string SelectedUserName { get; set; }
        public int[] Authorised { get; set; }
    }

    public class PlantAuthModel
    {
        public string Id { get; set; }
        public string SelectedUserName { get; set; }
        public int[] Authorised { get; set; }
    }

}
