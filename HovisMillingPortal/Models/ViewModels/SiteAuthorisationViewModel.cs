using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.ComponentModel.DataAnnotations;

namespace HovisMillingPortal.ViewModels
{
    public class SiteAuthorisationViewModel
    {
        public string Id { get; set; }
        public string SelectedUserName { get; set; }
        public string SiteAuthselectedIDsHF { get; set; }
    }
    public class AuthorisedSitesViewModel
    {
        public long? SiteRecid { get; set; }
        public string UserName { get; set; }
    }
    public class AuthAndUnAuthSitesViewModel
    {
        public long? SiteRecid { get; set; }
        public string UserName { get; set; }
        public string SiteDesc { get; set; }
        public string Authorised { get; set; }
    }

}
