using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HovisMillingPortal.Models;
using HovisMillingPortal.ViewModels;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using System.Collections;

namespace HovisMillingPortal.Controllers
{
    public class SitePlantAuthController : Controller
    {
        private HovisMillingPortalEntities db = new HovisMillingPortalEntities();
        // GET: SitePlantAuth
        [Authorize(Roles = "Admin")]
        public ActionResult SiteIndex()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult IndexBySite(string Id)
        {
            var model = db.AspNetUsers.Where(s => s.Id == Id).FirstOrDefault();
            string selectedusername = model.Email;
            int[] authorisedlist = new int[] { };
            var authsitemodel = (from a in db.t_Hovis_Milling_User_Site_Authorisation
                                 where a.UserName == selectedusername
                                 select new CheckBoxListItemObject
                                 {
                                     ID = (int)a.SiteRecid,
                                     Name = a.UserName
                                 }).ToList();

            authorisedlist = authsitemodel.Select(s => s.ID).ToArray();
            ViewData["UnSelectedList"] = GetSiteItems();
            SiteAuthModel siteauthmodel = new SiteAuthModel()
            {
                Id = Id,
                SelectedUserName = selectedusername,
                Authorised = authorisedlist
            };
            return View(siteauthmodel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult IndexBySite(SiteAuthModel model)
        {
            int[] selectedItems = CheckBoxListExtension.GetSelectedValues<int>("checkBoxList1");

            //firstly lets remove any that are not in the list
            var currentmodel = db.t_Hovis_Milling_User_Site_Authorisation;

            if (ModelState.IsValid)
            {
                try
                {
                    var currentauthlist = db.t_Hovis_Milling_User_Site_Authorisation.Where(s => s.UserName == model.SelectedUserName).ToList();
                    if (currentauthlist != null)
                    {
                        var tobedeleted = currentauthlist.Where(p => !selectedItems.Any(p2 => p2 == p.SiteRecid)).ToList();

                        if (tobedeleted != null)
                        {
                            foreach (var trash in tobedeleted)
                            {
                                currentmodel.Remove(trash);
                                db.SaveChanges();
                            }

                        };

                        var alreadyexist = currentauthlist.Where(q => selectedItems.Any(q2 => q2 == q.SiteRecid)).ToList();

                        var newrecords = selectedItems.Where(t => !alreadyexist.Any(t2 => t2.SiteRecid == t)).ToList();

                        if (newrecords != null)
                        {
                            foreach (var newitem in newrecords)
                            {
                                t_Hovis_Milling_User_Site_Authorisation newauthrec = new t_Hovis_Milling_User_Site_Authorisation();
                                newauthrec.SiteRecid = newitem;
                                newauthrec.UserName = model.SelectedUserName;
                                currentmodel.Add(newauthrec);
                                db.SaveChanges();
                            }
                        };

                        return RedirectToAction("IndexList", "ApplicationUser");
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult IndexByPlant(string Id)
        {
            var model = db.AspNetUsers.Where(s => s.Id == Id).FirstOrDefault();
            string selectedusername = model.Email;
            int[] authorisedlist = new int[] { };
            var authplantmodel = (from a in db.t_Hovis_Milling_User_Plant_Authorisation
                                  where a.UserName == selectedusername
                                  select new CheckBoxListItemObject
                                  {
                                      ID = (int)a.PlantRecid,
                                      Name = a.UserName
                                  }).ToList();

            authorisedlist = authplantmodel.Select(s => s.ID).ToArray();
            ViewData["UnSelectedList"] = GetPlantItems();
            PlantAuthModel plantauthmodel = new PlantAuthModel()
            {
                Id = Id,
                SelectedUserName = selectedusername,
                Authorised = authorisedlist
            };
            return View(plantauthmodel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult IndexByPlant(PlantAuthModel model)
        {
            int[] selectedItems = CheckBoxListExtension.GetSelectedValues<int>("checkBoxList2");

            //firstly lets remove any that are not in the list
            var currentmodel = db.t_Hovis_Milling_User_Plant_Authorisation;

            if (ModelState.IsValid)
            {
                try
                {
                    var currentauthlist = db.t_Hovis_Milling_User_Plant_Authorisation.Where(s => s.UserName == model.SelectedUserName).ToList();
                    if (currentauthlist != null)
                    {
                        var tobedeleted = currentauthlist.Where(p => !selectedItems.Any(p2 => p2 == p.PlantRecid)).ToList();

                        if (tobedeleted != null)
                        {
                            foreach (var trash in tobedeleted)
                            {
                                currentmodel.Remove(trash);
                                db.SaveChanges();
                            }

                        };

                        var alreadyexist = currentauthlist.Where(q => selectedItems.Any(q2 => q2 == q.PlantRecid)).ToList();

                        var newrecords = selectedItems.Where(t => !alreadyexist.Any(t2 => t2.PlantRecid == t)).ToList();

                        if (newrecords != null)
                        {
                            foreach (var newitem in newrecords)
                            {
                                t_Hovis_Milling_User_Plant_Authorisation newauthrec = new t_Hovis_Milling_User_Plant_Authorisation();
                                newauthrec.PlantRecid = newitem;
                                newauthrec.UserName = model.SelectedUserName;
                                currentmodel.Add(newauthrec);
                                db.SaveChanges();
                            }
                        };

                        return RedirectToAction("IndexList", "ApplicationUser");
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            return View(model);
        }

        [ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult SiteGridViewPartial()
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            var model = db.t_Milling_Site.ToList();
            return PartialView("_SiteGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult SiteGridViewPartialAddNew(HovisMillingPortal.Models.t_Milling_Site item)
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            var model = db.t_Milling_Site;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var Newmodel = db.t_Milling_Site.ToList();
            return PartialView("_SiteGridViewPartial", Newmodel);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult SiteGridViewPartialUpdate(HovisMillingPortal.Models.t_Milling_Site item)
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            var model = db.t_Milling_Site;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.SiteRecid == item.SiteRecid);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var Newmodel = db.t_Milling_Site.ToList();
            return PartialView("_SiteGridViewPartial", Newmodel);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult SiteGridViewPartialDelete(System.Int32 SiteRecid)
        {
            var model = db.t_Milling_Site;
            if (SiteRecid >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.SiteRecid == SiteRecid);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var Newmodel = db.t_Milling_Site.ToList();
            return PartialView("_SiteGridViewPartial", Newmodel);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult PlantIndex()
        {
            return View();
        }

        [ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult PlantGridViewPartial()
        {
            ViewBag.Sites = new SelectList(db.t_Milling_Site, "SiteRecid", "SiteDesc");
            ViewBag.Function = new SelectList(db.t_Milling_Function, "FuncRecid", "FuncDesc");
            var model = db.t_Milling_Plant.ToList();
            return PartialView("_PlantGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult PlantGridViewPartialAddNew(HovisMillingPortal.Models.t_Milling_Plant item)
        {
            ViewBag.Sites = new SelectList(db.t_Milling_Site, "SiteRecid", "SiteDesc");
            ViewBag.Function = new SelectList(db.t_Milling_Function, "FuncRecid", "FuncDesc");
            var model = db.t_Milling_Plant;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var Newmodel = db.t_Milling_Plant.ToList();
            return PartialView("_PlantGridViewPartial", Newmodel);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult PlantGridViewPartialUpdate(HovisMillingPortal.Models.t_Milling_Plant item)
        {
            ViewBag.Sites = new SelectList(db.t_Milling_Site, "SiteRecid", "SiteDesc");
            var model = db.t_Milling_Plant;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.PlantRecid == item.PlantRecid);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var Newmodel = db.t_Milling_Plant.ToList();
            return PartialView("_PlantGridViewPartial", Newmodel);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult PlantGridViewPartialDelete(System.Int32 PlantRecid)
        {
            ViewBag.Sites = new SelectList(db.t_Milling_Site, "SiteRecid", "SiteDesc");
            var model = db.t_Milling_Plant;
            if (PlantRecid >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.PlantRecid == PlantRecid);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var Newmodel = db.t_Milling_Plant.ToList();
            return PartialView("_PlantGridViewPartial", Newmodel);
        }

        private IList GetSiteItems()
        {
            List<CheckBoxListItemObject> items = new List<CheckBoxListItemObject>();
            var ProdSites = db.t_Milling_Site.ToList().Select(s => new { ID = s.SiteRecid, Name = s.SiteDesc }).ToList();
            foreach (var item in ProdSites)
            {
                items.Add(new CheckBoxListItemObject()
                {
                    ID = Convert.ToInt32(item.ID),
                    Name = (string)item.Name
                });
            }

            return items;
        }

        private IList GetPlantItems()
        {
            List<CheckBoxListItemObject> items = new List<CheckBoxListItemObject>();
            //var ProdPlants = db.t_Hovis_Baking_Plants.ToList().Select(s => new { ID = s.PlantRecid, Name = s..SiteDesc }).ToList();
            var ProdPlants = (from a in db.t_Milling_Plant
                              join b in db.t_Milling_Site on a.SiteRecid equals b.SiteRecid
                              select new
                              {
                                  ID = a.PlantRecid,
                                  Name = b.SiteDesc + " - " + a.PlantDesc
                              }).ToList();

            foreach (var item in ProdPlants)
            {
                items.Add(new CheckBoxListItemObject()
                {
                    ID = Convert.ToInt32(item.ID),
                    Name = (string)item.Name
                });
            }

            return items;
        }
    }
}