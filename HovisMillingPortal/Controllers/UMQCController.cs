using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HovisMillingPortal.Models;
using HovisMillingPortal.ViewModels;
using DevExpress.Web.Mvc;
using System.Data.Entity;
using DevExpress.Web;

namespace HovisMillingPortal.Controllers
{
    public class UMQCController : Controller
    {

        private HovisMillingPortalEntities db = new HovisMillingPortalEntities();
        //private static HovisMillingPortalEntities db2 = new HovisMillingPortalEntities();

        // GET: UMQC
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin, UMQCAdmin, UMQCUser")]
        public ActionResult ResultNameIndex()
        {

            return View();
        }

        [ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin, UMQCUser")]
        public ActionResult ResultNameGridViewPartial()
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            ViewBag.Category = new SelectList(db.t_Milling_UMQC_Category, "CategoryRecid", "CategoryDesc");
            var model = db.t_Milling_UMQC_Results_Standards_Master;
            return PartialView("_ResultNameGridViewPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin, UMQCUser")]
        public ActionResult ResultNameGridViewPartialAddNew(HovisMillingPortal.Models.t_Milling_UMQC_Results_Standards_Master item)
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            ViewBag.Category = new SelectList(db.t_Milling_UMQC_Category, "CategoryRecid", "CategoryDesc");
            var model = db.t_Milling_UMQC_Results_Standards_Master;
            if (ModelState.IsValid)
            {
                try
                {
                    item.ResultStdActiveFlag = 1;

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
            return PartialView("_ResultNameGridViewPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin, UMQCUser")]
        public ActionResult ResultNameGridViewPartialUpdate(HovisMillingPortal.Models.t_Milling_UMQC_Results_Standards_Master item)
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            ViewBag.Category = new SelectList(db.t_Milling_UMQC_Category, "CategoryRecid", "CategoryDesc");
            var model = db.t_Milling_UMQC_Results_Standards_Master;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.ResultStdRecid == item.ResultStdRecid);
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
            return PartialView("_ResultNameGridViewPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin, UMQCUser")]
        public ActionResult ResultNameGridViewPartialDelete(System.Int32 ResultStdRecid)
        {
            ViewBag.Category = new SelectList(db.t_Milling_UMQC_Category, "CategoryRecid", "CategoryDesc");
            var model = db.t_Milling_UMQC_Results_Standards_Master;
            if (ResultStdRecid >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.ResultStdRecid == ResultStdRecid);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_ResultNameGridViewPartial", model.ToList());
        }

        [Authorize(Roles = "Admin, UMQCAdmin")]
        public ActionResult StandardsMasterIndex()
        {

            return View();
        }

        [ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin")]
        public ActionResult StandardsMasterGridViewPartial()
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            ViewBag.Standards = new SelectList(db.t_Milling_UMQC_Results_Standards_Master, "ResultStdRecid", "ResultStdDescription");
            var RecipeList = recipeList();
            ViewData["Recipe"] = RecipeList.OrderBy(s => s.ProductMasterRecid).ToList().Select(s => new { ProductMasterRecid = s.ProductMasterRecid, RecipeDesc = s.RecipeDesc });

            //ViewBag.Recipe = new SelectList(db.t_Milling_UMQC_Products_Master, "ProductMasterRecid", "RecipeDesc" );
            var model = db.t_Milling_UMQC_Item_Standards_Detail;
            return PartialView("_StandardsMasterGridViewPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin")]
        public ActionResult StandardsMasterGridViewPartialAddNew(HovisMillingPortal.Models.t_Milling_UMQC_Item_Standards_Detail item)
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            ViewBag.Category = new SelectList(db.t_Milling_UMQC_Category, "CategoryRecid", "CategoryDesc");
            ViewBag.Standards = new SelectList(db.t_Milling_UMQC_Results_Standards_Master, "ResultStdRecid", "ResultStdDescription");
            var RecipeList = recipeList();
            ViewData["Recipe"] = RecipeList.OrderBy(s => s.ProductMasterRecid).ToList().Select(s => new { ProductMasterRecid = s.ProductMasterRecid, RecipeDesc = s.RecipeDesc });
            var RecipeDetails = RecipeList.Where(s => s.ProductMasterRecid == item.RecipeProductRecid).FirstOrDefault();
            var model = db.t_Milling_UMQC_Item_Standards_Detail;

            if (item.StdComments == null)
            {
                item.StdComments = "";
            }
            if (ModelState.IsValid)
            {
                try
                {
                    item.StdItem = RecipeDetails.RecipeCode;
                    item.StdRecipe = RecipeDetails.RecipeCode;
 

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
            return PartialView("_StandardsMasterGridViewPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin")]
        public ActionResult StandardsMasterGridViewPartialUpdate(HovisMillingPortal.Models.t_Milling_UMQC_Item_Standards_Detail item)
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            ViewBag.Category = new SelectList(db.t_Milling_UMQC_Category, "CategoryRecid", "CategoryDesc");
            ViewBag.Standards = new SelectList(db.t_Milling_UMQC_Results_Standards_Master, "ResultStdRecid", "ResultStdDescription");
            var RecipeList = recipeList();
            ViewData["Recipe"] = RecipeList.OrderBy(s => s.ProductMasterRecid).ToList().Select(s => new { ProductMasterRecid = s.ProductMasterRecid, RecipeDesc = s.RecipeDesc });
            var model = db.t_Milling_UMQC_Item_Standards_Detail;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.StdDetailRecid == item.StdDetailRecid);
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
            return PartialView("_StandardsMasterGridViewPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin")]
        public ActionResult StandardsMasterGridViewPartialDelete(System.Int64 StdDetailRecid)
        {
            ViewBag.Category = new SelectList(db.t_Milling_UMQC_Category, "CategoryRecid", "CategoryDesc");
            ViewBag.Standards = new SelectList(db.t_Milling_UMQC_Results_Standards_Master, "ResultStdRecid", "ResultStdDescription");
            var RecipeList = recipeList();
            ViewData["Recipe"] = RecipeList.OrderBy(s => s.ProductMasterRecid).ToList().Select(s => new { ProductMasterRecid = s.ProductMasterRecid, RecipeDesc = s.RecipeDesc });
            var model = db.t_Milling_UMQC_Item_Standards_Detail;
            if (StdDetailRecid >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.StdDetailRecid == StdDetailRecid);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_StandardsMasterGridViewPartial", model.ToList());
        }

        [Authorize(Roles = "Admin, UMQCAdmin")]
        public ActionResult TestHeaderIndex()
        {

            return View();
        }

        [ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin")]
        public ActionResult TestHeaderGridViewPartial()
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            //ViewBag.Category = new SelectList(db.t_Milling_UMQC_Category, "CategoryRecid", "CategoryDesc");
            ViewBag.Plant = new SelectList(db.t_Milling_Plant, "PlantRecid", "PlantDesc");
            ViewBag.Function = new SelectList(db.t_Milling_Function, "FuncRecid", "FuncDesc");
            ViewBag.Shift = new SelectList(db.t_Milling_Shift, "ShiftRecid", "ShiftDesc");
            var RecipeList = recipeList();
            ViewData["Recipe"] = RecipeList.OrderBy(s => s.ProductMasterRecid).ToList().Select(s => new { ProductMasterRecid = s.ProductMasterRecid, RecipeDesc = s.RecipeDesc });
            var model = db.t_Milling_UMQC_Results_Header.OrderByDescending(s => s.ResultHeaderRecid).ToList();
            return PartialView("_TestHeaderGridViewPartial", model);
        }


        [Authorize(Roles = "Admin, UMQCAdmin, UMQCUser")]
        public ActionResult Create()
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
            t_Milling_UMQC_Results_Header t_Milling_UMQC_Results_Header = new t_Milling_UMQC_Results_Header();

            var Masterdata = db.t_Milling_UMQC_Results_Header.FirstOrDefault();

            t_Milling_UMQC_Results_Header.ResultActiveFlag = 1;
            t_Milling_UMQC_Results_Header.ResultRunStart = adjusteddate;
            t_Milling_UMQC_Results_Header.ResultRunEnd = adjusteddate;
            t_Milling_UMQC_Results_Header.ResultTestTaken = adjusteddate;

            t_Milling_UMQC_Results_Header.ResultLastChangedBy = User.Identity.Name;
            t_Milling_UMQC_Results_Header.ResultLastChangedDate = adjusteddate;
            t_Milling_UMQC_Results_Header.ResultCreatedBy = User.Identity.Name;
            t_Milling_UMQC_Results_Header.ResultCreationDate = adjusteddate;
            t_Milling_UMQC_Results_Header.ExcludeAverages = 0;

            //To only show sites the user is authorised to
            string username = "";
            int? defaultsite = 0;
            int? defaultfunc = 0;
            int? countplant = 0;
            int? countfunction = 0;
            if (User.Identity.Name != null)
            {
                username = User.Identity.Name;
            }

            var defaultsitemodel = DefaultSite(username);

            defaultsite = defaultsitemodel.Select(s => s.SiteRecid).FirstOrDefault();

            var FunctionList = DefaultFunctionList(defaultsite);


            defaultfunc = FunctionList.Select(s => s.FuncRecid).FirstOrDefault();

            var PlantList = DefaultPlant(defaultsite);

            countplant = PlantList.Count();
            countfunction = FunctionList.Count();

            if (countplant == 1)
            {
                t_Milling_UMQC_Results_Header.ResultPlantRecid = PlantList.Select(s => s.PlantRecid).FirstOrDefault();
            }
            if (countfunction == 1)
            {
                t_Milling_UMQC_Results_Header.ResultFuncRecid = FunctionList.Select(s => s.FuncRecid).FirstOrDefault();
            }

            ViewBag.DefaultSite = defaultsite;
            ViewBag.DefaultFunc = defaultfunc;
            ViewBag.Plant = new SelectList(PlantList, "PlantRecid", "PlantDesc");
            ViewBag.Function = new SelectList(FunctionList, "FuncRecid", "FuncDesc");
            ViewBag.Shift = new SelectList(db.t_Milling_Shift, "ShiftRecid", "ShiftDesc");
            var RecipeList = recipeList();
            ViewData["Recipe"] = RecipeList.OrderBy(s => s.ProductMasterRecid).ToList().Select(s => new { ProductMasterRecid = s.ProductMasterRecid, RecipeDesc = s.RecipeDesc });

            return View(t_Milling_UMQC_Results_Header);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin, UMQCUser")]
        public ActionResult Create([ModelBinder(typeof(DevExpressEditorsBinder))] HovisMillingPortal.Models.t_Milling_UMQC_Results_Header item)
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);


            if (item.ResultProductRecid == null)
            {
                ModelState.AddModelError("ResultProductRecid", "You must choose a Recipe and Item");
            }
            if (item.ResultPlantRecid == null)
            {
                ModelState.AddModelError("ResultPlantRecid", "You must choose a Plant");
            }
            if (item.ResultFuncRecid == null)
            {
                ModelState.AddModelError("ResultFuncRecid", "You must choose a Process");
            }
            if (item.ResultShiftRecid == null)
            {
                ModelState.AddModelError("ResultShiftRecid", "You must choose a Shift");
            }

            var model = db.t_Milling_UMQC_Results_Header;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db.SaveChanges();
                    db.usp_Milling_UMQC_Results_Detail_Creation(item.ResultHeaderRecid);
                    return RedirectToAction("TestDetailIndex", "UMQC", new { resultheaderrecid = item.ResultHeaderRecid });
                }
                catch (Exception e)
                {
                    ViewData["CustomError"] = e.Message;
                }
            }
            else
                ViewData["CustomError"] = "Please, correct all errors.";

            //To only show sites the user is authorised to
            string username = "";
            int? defaultsite = 0;
            int? defaultfunc = 0;
            if (User.Identity.Name != null)
            {
                username = User.Identity.Name;
            }

            var defaultsitemodel = DefaultSite(username);

            defaultsite = defaultsitemodel.Select(s => s.SiteRecid).FirstOrDefault();

            //int passbackfuncchoice = (Request.Params["passbackfuncchoice"] != null) ? int.Parse(Request.Params["passbackfuncchoice"]) : -1;

            var FunctionList = DefaultFunctionList(defaultsite);
            var PlantList = DefaultPlant(defaultsite);

            if (item.ResultFuncRecid == null)
            {
                defaultfunc = FunctionList.Select(s => s.FuncRecid).FirstOrDefault();
                
            }
            else
            {
                defaultfunc = item.ResultFuncRecid;
                PlantList = DefaultPlantchoice(defaultsite, item.ResultFuncRecid);
            }

 

            ViewBag.DefaultSite = defaultsite;
            ViewBag.DefaultFunc = defaultfunc;
            ViewBag.Plant = new SelectList(PlantList, "PlantRecid", "PlantDesc");
            ViewBag.Function = new SelectList(FunctionList, "FuncRecid", "FuncDesc");
            ViewBag.Shift = new SelectList(db.t_Milling_Shift, "ShiftRecid", "ShiftDesc");
            var RecipeList = recipeList();
            ViewData["Recipe"] = RecipeList.OrderBy(s => s.ProductMasterRecid).ToList().Select(s => new { ProductMasterRecid = s.ProductMasterRecid, RecipeDesc = s.RecipeDesc });

            return View(item);
        }

        
        private static List<t_Milling_Plant> _MYPlantlist = DataProvider.StaticPlantList.ToList();

        public static List<DefaultPlantVM> DefaultPlantlist(int? siteno, int? funcno)
        {

            var model = (from a in DataProvider.StaticPlantList
                         join b in DataProvider.StaticSiteList on a.SiteRecid equals b.SiteRecid
                         where a.SiteRecid == siteno && a.PlantFuncRecid == funcno
                         select new DefaultPlantVM
                         {
                             PlantRecid = a.PlantRecid,
                             SiteRecid = b.SiteRecid,
                             PlantDesc = a.PlantDesc
                         }).ToList();

            return model;
        }

        public ActionResult PlantComboPartial()
        {
            string username = "";
            int? defaultsite = 0;
            int? defaultfunc = 0;
            int? countplant = 0;
            int? countfunction = 0;
            if (User.Identity.Name != null)
            {
                username = User.Identity.Name;
            }

            int passbackfuncchoice = (Request.Params["passbackfuncchoice"] != null) ? int.Parse(Request.Params["passbackfuncchoice"]) : -1;
            //return PartialView(new Customer { Country = country });

            var defaultsitemodel = DefaultSite(username);

            defaultsite = defaultsitemodel.Select(s => s.SiteRecid).FirstOrDefault();

            //var defaultFuncmodel = DefaultFunctionList(defaultsite);

            //defaultfunc = defaultFuncmodel.Select(s => s.FuncRecid).FirstOrDefault();

            //var PlantList = DefaultPlant(defaultsite);

            //var model = db.t_Milling_Plant.ToList();
            //t_Milling_UMQC_Results_Header t_Milling_UMQC_Results_Header = new t_Milling_UMQC_Results_Header();
            var model = new t_Milling_UMQC_Results_Header();

            ViewBag.DefaultSite = defaultsite;
            ViewBag.DefaultFunc = passbackfuncchoice;
            //ViewData["PlantList"] = PlantList.OrderBy(s => s.PlantRecid).ToList().Select(s => new { PlantRecid = s.PlantRecid, PlantDesc = s.PlantDesc });

            return PartialView(model);
        }

        [Authorize(Roles = "Admin, UMQCAdmin")]
        public ActionResult TestDetailIndex(System.Int64 resultheaderrecid)
        {
            ViewBag.resultheaderrecid = resultheaderrecid;
            return View();

        }

        public ActionResult DetailPageControl(System.Int64 resultheaderrecid)
        {
            var testtemp = TempData["resultheaderrecid"];
            TempData["resultheaderrecid"] = resultheaderrecid;
            //string nothing = "";
            //var model = db.t_SOP_Order_Header.Where(e => e.orderHeaderRecid == SelectedItemID).ToList();
            var FunctionValue = db.t_Milling_UMQC_Results_Header.Where(e => e.ResultHeaderRecid == resultheaderrecid).FirstOrDefault();
            ViewBag.FunctionValue = FunctionValue.ResultFuncRecid;
            //return PartialView(SelectedOrderHeader);
            return PartialView();
        }


        [ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin, UMQCUser")]
        public ActionResult QualitiesDetailGridViewPartial(System.Int64 resultheaderrecid)
        {

            ViewBag.Standards = new SelectList(db.t_Milling_UMQC_Results_Standards_Master, "ResultStdRecid", "ResultStdDescription");
            //var model = db.t_Milling_UMQC_Results_Detail.Where(s => s.ResultHeaderrecid == resultheaderrecid).ToList();

            var model = (from a in db.t_Milling_UMQC_Results_Detail
                         join b in db.t_Milling_UMQC_Results_Standards_Master on a.ResultMasterStdRecid equals b.ResultStdRecid
                         join c in db.t_Milling_UMQC_Results_Header on a.ResultHeaderrecid equals c.ResultHeaderRecid
                         where b.ResultStdCategoryRecid == 2 && a.ResultDetailActiveFlag == 1 && b.ResultStdActiveFlag == 1 && a.ResultHeaderrecid == resultheaderrecid
                         select new resultDetailViewModel
                         {
                             ResultDetailRecid = a.ResultDetailRecid,
                             ResultHeaderrecid = a.ResultHeaderrecid,
                             ResultMasterStdRecid = a.ResultMasterStdRecid,
                             ResultStdDetailRecid = a.ResultStdDetailRecid,
                             ResultMinScore = a.ResultMinScore,
                             ResultTargetScore = a.ResultTargetScore,
                             ResultMaxScore = a.ResultMaxScore,
                             ResultActualScore = a.ResultActualScore,
                             ResultComments = a.ResultComments,
                             ResultDetailLastChangedBy = a.ResultDetailLastChangedBy,
                             ResultDetailLastChangedDate = a.ResultDetailLastChangedDate,
                             ResultDetailActiveFlag = a.ResultDetailActiveFlag,
                             ConcessionDesc = c.ConcessionDesc,
                             ResultTestTaken = c.ResultTestTaken
                         }).ToList();
            return PartialView("_QualitiesDetailGridViewPartial", model);
        }

        [ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin, UMQCUser")]
        public ActionResult SegregationDetailGridViewPartial(System.Int64 resultheaderrecid)
        {

            ViewBag.Standards = new SelectList(db.t_Milling_UMQC_Results_Standards_Master, "ResultStdRecid", "ResultStdDescription");
            //var model = db.t_Milling_UMQC_Results_Detail.Where(s => s.ResultHeaderrecid == resultheaderrecid).ToList();

            var model = (from a in db.t_Milling_UMQC_Results_Detail
                         join b in db.t_Milling_UMQC_Results_Standards_Master on a.ResultMasterStdRecid equals b.ResultStdRecid
                         join c in db.t_Milling_UMQC_Results_Header on a.ResultHeaderrecid equals c.ResultHeaderRecid
                         where b.ResultStdCategoryRecid == 1 && a.ResultDetailActiveFlag == 1 && b.ResultStdActiveFlag == 1 && a.ResultHeaderrecid == resultheaderrecid
                         select new resultDetailViewModel
                         {
                             ResultDetailRecid = a.ResultDetailRecid,
                             ResultHeaderrecid = a.ResultHeaderrecid,
                             ResultMasterStdRecid = a.ResultMasterStdRecid,
                             ResultStdDetailRecid = a.ResultStdDetailRecid,
                             ResultMinScore = a.ResultMinScore,
                             ResultTargetScore = a.ResultTargetScore,
                             ResultMaxScore = a.ResultMaxScore,
                             ResultActualScore = a.ResultActualScore,
                             ResultComments = a.ResultComments,
                             ResultDetailLastChangedBy = a.ResultDetailLastChangedBy,
                             ResultDetailLastChangedDate = a.ResultDetailLastChangedDate,
                             ResultDetailActiveFlag = a.ResultDetailActiveFlag,
                             ConcessionDesc = c.ConcessionDesc,
                             ResultTestTaken = c.ResultTestTaken
                         }).ToList();
            return PartialView("_SegregationDetailGridViewPartial", model);
        }

        [ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin, UMQCUser")]
        public ActionResult OthersDetailGridViewPartial(System.Int64 resultheaderrecid)
        {

            ViewBag.Standards = new SelectList(db.t_Milling_UMQC_Results_Standards_Master, "ResultStdRecid", "ResultStdDescription");
            //var model = db.t_Milling_UMQC_Results_Detail.Where(s => s.ResultHeaderrecid == resultheaderrecid).ToList();

            var model = (from a in db.t_Milling_UMQC_Results_Detail
                         join b in db.t_Milling_UMQC_Results_Standards_Master on a.ResultMasterStdRecid equals b.ResultStdRecid
                         join c in db.t_Milling_UMQC_Results_Header on a.ResultHeaderrecid equals c.ResultHeaderRecid
                         where b.ResultStdCategoryRecid == 3 && a.ResultDetailActiveFlag == 1 && b.ResultStdActiveFlag == 1 && a.ResultHeaderrecid == resultheaderrecid
                         select new resultDetailViewModel
                         {
                             ResultDetailRecid = a.ResultDetailRecid,
                             ResultHeaderrecid = a.ResultHeaderrecid,
                             ResultMasterStdRecid = a.ResultMasterStdRecid,
                             ResultStdDetailRecid = a.ResultStdDetailRecid,
                             ResultMinScore = a.ResultMinScore,
                             ResultTargetScore = a.ResultTargetScore,
                             ResultMaxScore = a.ResultMaxScore,
                             ResultActualScore = a.ResultActualScore,
                             ResultComments = a.ResultComments,
                             ResultDetailLastChangedBy = a.ResultDetailLastChangedBy,
                             ResultDetailLastChangedDate = a.ResultDetailLastChangedDate,
                             ResultDetailActiveFlag = a.ResultDetailActiveFlag,
                             ConcessionDesc = c.ConcessionDesc,
                             ResultTestTaken = c.ResultTestTaken
                         }).ToList();
            return PartialView("_OthersDetailGridViewPartial", model);
        }

        public ActionResult QualitiesBatchUpdatePartial(MVCxGridViewBatchUpdateValues<HovisMillingPortal.ViewModels.resultDetailViewModel, int> batchValues, System.Int64 resultheaderrecid)
        {
            //This is used for both Qualities and Segregation updates
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);

            foreach (var item in batchValues.Insert)
            {
                if (batchValues.IsValid(item))
                    InsertNewItem(item, batchValues, resultheaderrecid);
                else
                    batchValues.SetErrorText(item, "Correct validation errors");
            }
            foreach (var item in batchValues.Update)
            {
                if (batchValues.IsValid(item))
                    UpdateItem(item, batchValues, resultheaderrecid);
                else
                    batchValues.SetErrorText(item, "Correct validation errors");
            }
            foreach (var itemKey in batchValues.DeleteKeys)
            {
                DeleteItem(itemKey, batchValues, resultheaderrecid);
            }

            //var model = db.t_Milling_UMQC_Results_Detail.Where(s => s.ResultHeaderrecid == resultheaderrecid).ToList();
            var model = (from a in db.t_Milling_UMQC_Results_Detail
                         join b in db.t_Milling_UMQC_Results_Standards_Master on a.ResultMasterStdRecid equals b.ResultStdRecid
                         join c in db.t_Milling_UMQC_Results_Header on a.ResultHeaderrecid equals c.ResultHeaderRecid
                         where b.ResultStdCategoryRecid == 2 && a.ResultDetailActiveFlag == 1 && b.ResultStdActiveFlag == 1 && a.ResultHeaderrecid == resultheaderrecid
                         select new resultDetailViewModel
                         {
                             ResultDetailRecid = a.ResultDetailRecid,
                             ResultHeaderrecid = a.ResultHeaderrecid,
                             ResultMasterStdRecid = a.ResultMasterStdRecid,
                             ResultStdDetailRecid = a.ResultStdDetailRecid,
                             ResultMinScore = a.ResultMinScore,
                             ResultTargetScore = a.ResultTargetScore,
                             ResultMaxScore = a.ResultMaxScore,
                             ResultActualScore = a.ResultActualScore,
                             ResultComments = a.ResultComments,
                             ResultDetailLastChangedBy = a.ResultDetailLastChangedBy,
                             ResultDetailLastChangedDate = a.ResultDetailLastChangedDate,
                             ResultDetailActiveFlag = a.ResultDetailActiveFlag,
                             ConcessionDesc = c.ConcessionDesc,
                             ResultTestTaken = c.ResultTestTaken
                         }).ToList();
            ViewBag.Standards = new SelectList(db.t_Milling_UMQC_Results_Standards_Master, "ResultStdRecid", "ResultStdDescription");
            return PartialView("_QualitiesDetailGridViewPartial", model);
        }

        public ActionResult SegregationBatchUpdatePartial(MVCxGridViewBatchUpdateValues<HovisMillingPortal.ViewModels.resultDetailViewModel, int> batchValues, System.Int64 resultheaderrecid)
        {
            //This is used for both Qualities and Segregation updates
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);

            foreach (var item in batchValues.Insert)
            {
                if (batchValues.IsValid(item))
                    InsertNewItem(item, batchValues, resultheaderrecid);
                else
                    batchValues.SetErrorText(item, "Correct validation errors");
            }
            foreach (var item in batchValues.Update)
            {
                if (batchValues.IsValid(item))
                    UpdateItem(item, batchValues, resultheaderrecid);
                else
                    batchValues.SetErrorText(item, "Correct validation errors");
            }
            foreach (var itemKey in batchValues.DeleteKeys)
            {
                DeleteItem(itemKey, batchValues, resultheaderrecid);
            }

            //var model = db.t_Milling_UMQC_Results_Detail.Where(s => s.ResultHeaderrecid == resultheaderrecid).ToList();
            var model = (from a in db.t_Milling_UMQC_Results_Detail
                         join b in db.t_Milling_UMQC_Results_Standards_Master on a.ResultMasterStdRecid equals b.ResultStdRecid
                         join c in db.t_Milling_UMQC_Results_Header on a.ResultHeaderrecid equals c.ResultHeaderRecid
                         where b.ResultStdCategoryRecid == 1 && a.ResultDetailActiveFlag == 1 && b.ResultStdActiveFlag == 1 && a.ResultHeaderrecid == resultheaderrecid
                         select new resultDetailViewModel
                         {
                             ResultDetailRecid = a.ResultDetailRecid,
                             ResultHeaderrecid = a.ResultHeaderrecid,
                             ResultMasterStdRecid = a.ResultMasterStdRecid,
                             ResultStdDetailRecid = a.ResultStdDetailRecid,
                             ResultMinScore = a.ResultMinScore,
                             ResultTargetScore = a.ResultTargetScore,
                             ResultMaxScore = a.ResultMaxScore,
                             ResultActualScore = a.ResultActualScore,
                             ResultComments = a.ResultComments,
                             ResultDetailLastChangedBy = a.ResultDetailLastChangedBy,
                             ResultDetailLastChangedDate = a.ResultDetailLastChangedDate,
                             ResultDetailActiveFlag = a.ResultDetailActiveFlag,
                             ConcessionDesc = c.ConcessionDesc,
                             ResultTestTaken = c.ResultTestTaken
                         }).ToList();
            ViewBag.Standards = new SelectList(db.t_Milling_UMQC_Results_Standards_Master, "ResultStdRecid", "ResultStdDescription");
            return PartialView("_SegregationDetailGridViewPartial", model);
        }

        public ActionResult OthersBatchUpdatePartial(MVCxGridViewBatchUpdateValues<HovisMillingPortal.ViewModels.resultDetailViewModel, int> batchValues, System.Int64 resultheaderrecid)
        {
            //This is used for both Qualities and Segregation updates
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);

            foreach (var item in batchValues.Insert)
            {
                if (batchValues.IsValid(item))
                    InsertNewItem(item, batchValues, resultheaderrecid);
                else
                    batchValues.SetErrorText(item, "Correct validation errors");
            }
            foreach (var item in batchValues.Update)
            {
                if (batchValues.IsValid(item))
                    UpdateItem(item, batchValues, resultheaderrecid);
                else
                    batchValues.SetErrorText(item, "Correct validation errors");
            }
            foreach (var itemKey in batchValues.DeleteKeys)
            {
                DeleteItem(itemKey, batchValues, resultheaderrecid);
            }

            //var model = db.t_Milling_UMQC_Results_Detail.Where(s => s.ResultHeaderrecid == resultheaderrecid).ToList();
            var model = (from a in db.t_Milling_UMQC_Results_Detail
                         join b in db.t_Milling_UMQC_Results_Standards_Master on a.ResultMasterStdRecid equals b.ResultStdRecid
                         join c in db.t_Milling_UMQC_Results_Header on a.ResultHeaderrecid equals c.ResultHeaderRecid
                         where b.ResultStdCategoryRecid == 3 && a.ResultDetailActiveFlag == 1 && b.ResultStdActiveFlag == 1 && a.ResultHeaderrecid == resultheaderrecid
                         select new resultDetailViewModel
                         {
                             ResultDetailRecid = a.ResultDetailRecid,
                             ResultHeaderrecid = a.ResultHeaderrecid,
                             ResultMasterStdRecid = a.ResultMasterStdRecid,
                             ResultStdDetailRecid = a.ResultStdDetailRecid,
                             ResultMinScore = a.ResultMinScore,
                             ResultTargetScore = a.ResultTargetScore,
                             ResultMaxScore = a.ResultMaxScore,
                             ResultActualScore = a.ResultActualScore,
                             ResultComments = a.ResultComments,
                             ResultDetailLastChangedBy = a.ResultDetailLastChangedBy,
                             ResultDetailLastChangedDate = a.ResultDetailLastChangedDate,
                             ResultDetailActiveFlag = a.ResultDetailActiveFlag,
                             ConcessionDesc = c.ConcessionDesc,
                             ResultTestTaken = c.ResultTestTaken
                         }).ToList();
            ViewBag.Standards = new SelectList(db.t_Milling_UMQC_Results_Standards_Master, "ResultStdRecid", "ResultStdDescription");
            return PartialView("_OthersDetailGridViewPartial", model);
        }

        public void InsertNewItem(HovisMillingPortal.ViewModels.resultDetailViewModel postedItem, MVCxGridViewBatchUpdateValues<HovisMillingPortal.ViewModels.resultDetailViewModel, int> batchValues, System.Int64 resultheaderrecid)
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);


            //We need to check if qty is zero
            if (postedItem.ResultActualScore == null)
            {
                batchValues.SetErrorText(postedItem, "you must enter a quantity....");
                return;
            }

            if (postedItem.ResultActualScore < 0)
            {
                batchValues.SetErrorText(postedItem, "cannot be less than zero....");
                return;
            }

            if (User.Identity.Name != null)
            {
                postedItem.ResultDetailLastChangedBy = User.Identity.Name;

            }
            postedItem.ResultDetailLastChangedDate = adjusteddate;

            var model = db.t_Milling_UMQC_Results_Detail;
            t_Milling_UMQC_Results_Detail newitem = new t_Milling_UMQC_Results_Detail();
            try
            {
                newitem.ResultHeaderrecid = postedItem.ResultHeaderrecid;
                newitem.ResultMasterStdRecid = postedItem.ResultMasterStdRecid;
                newitem.ResultStdDetailRecid = postedItem.ResultStdDetailRecid;
                newitem.ResultMinScore = postedItem.ResultMinScore;
                newitem.ResultTargetScore = postedItem.ResultTargetScore;
                newitem.ResultMaxScore = postedItem.ResultMaxScore;
                newitem.ResultActualScore = postedItem.ResultActualScore;
                newitem.ResultComments = postedItem.ResultComments;
                newitem.ResultDetailLastChangedBy = postedItem.ResultDetailLastChangedBy;
                newitem.ResultDetailLastChangedDate = postedItem.ResultDetailLastChangedDate;
                newitem.ResultDetailActiveFlag = postedItem.ResultDetailActiveFlag;
 
                model.Add(newitem);
                db.SaveChanges();

            }
            catch (Exception e)
            {
                batchValues.SetErrorText(postedItem, e.Message);
            }
        }

        public void UpdateItem(HovisMillingPortal.ViewModels.resultDetailViewModel postedItem, MVCxGridViewBatchUpdateValues<HovisMillingPortal.ViewModels.resultDetailViewModel, int> batchValues, System.Int64 resultheaderrecid)
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
            //var headermodel = db.t_Milling_Bacs_Header.FirstOrDefault(e => e.HeaderRecid == headerrecid);
            var item = db.t_Milling_UMQC_Results_Detail.First(i => i.ResultDetailRecid == postedItem.ResultDetailRecid);

            //We need to check if qty is zero
            if (postedItem.ResultActualScore == null)
            {
                batchValues.SetErrorText(postedItem, "you must enter an amount or zero....");
                return;
            }

            if (postedItem.ResultActualScore < 0)
            {
                batchValues.SetErrorText(postedItem, "cannot be less than zero....");
                return;
            }

            if (User.Identity.Name != null)
            {
                postedItem.ResultDetailLastChangedBy = User.Identity.Name;
            }
            postedItem.ResultDetailLastChangedDate = adjusteddate;

            var modelItem = db.t_Milling_UMQC_Results_Detail.Find(postedItem.ResultDetailRecid);
            try
            {
                if (modelItem != null)
                {
                    modelItem.ResultActualScore = postedItem.ResultActualScore;
                    modelItem.ResultComments = postedItem.ResultComments;
                    modelItem.ResultDetailLastChangedBy = postedItem.ResultDetailLastChangedBy;
                    modelItem.ResultDetailLastChangedDate = postedItem.ResultDetailLastChangedDate;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                batchValues.SetErrorText(postedItem, e.Message);
            }
        }

        public void DeleteItem(int itemKey, MVCxGridViewBatchUpdateValues<HovisMillingPortal.ViewModels.resultDetailViewModel, int> batchValues, System.Int64 resultheaderrecid)
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
        }

        [Authorize(Roles = "Admin, UMQCAdmin, UMQCUser")]
        public ActionResult ViewHeaderPartial(System.Int64 resultheaderrecid)
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
            //t_Milling_UMQC_Results_Header t_Milling_UMQC_Results_Header = new t_Milling_UMQC_Results_Header();

            var Masterdata = db.t_Milling_UMQC_Results_Header.Where(s => s.ResultHeaderRecid == resultheaderrecid).FirstOrDefault();

            //t_Milling_UMQC_Results_Header.ResultActiveFlag = 1;
            //t_Milling_UMQC_Results_Header.ResultRunStart = adjusteddate;
            //t_Milling_UMQC_Results_Header.ResultRunEnd = adjusteddate;

            //t_Milling_UMQC_Results_Header.ResultLastChangedBy = User.Identity.Name;
            //t_Milling_UMQC_Results_Header.ResultLastChangedDate = adjusteddate;
            //t_Milling_UMQC_Results_Header.ResultCreatedBy = User.Identity.Name;
            //t_Milling_UMQC_Results_Header.ResultCreationDate = adjusteddate;
            ViewBag.resultheaderrecid = resultheaderrecid;

            ViewBag.Plant = new SelectList(db.t_Milling_Plant, "PlantRecid", "PlantDesc");
            ViewBag.Function = new SelectList(db.t_Milling_Function, "FuncRecid", "FuncDesc");
            ViewBag.Shift = new SelectList(db.t_Milling_Shift, "ShiftRecid", "ShiftDesc");
            var RecipeList = recipeList();
            ViewData["Recipe"] = RecipeList.OrderBy(s => s.ProductMasterRecid).ToList().Select(s => new { ProductMasterRecid = s.ProductMasterRecid, RecipeDesc = s.RecipeDesc });

            return PartialView("_ViewHeaderPartial",Masterdata);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, UMQCAdmin")]
        public ActionResult IndexV2()
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
 
            Session["TestDDateEdit"] = adjusteddate;
            //TempData["TrackerDateEdit"] = TrackerDateEdit;
            ViewBag.TestDDateEdit = adjusteddate;
            if (Request.IsAjaxRequest()) //IsCallback
                return PartialView();
            return View(); //otherwise load the view that loads the partialview + rootlayout. Like reloading whole page.
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin")]
        public ActionResult IndexV2(DateTime? TestDDateEdit)
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
            if (TestDDateEdit == null && TempData["TestDDateEdit"] != null)
            {
                TestDDateEdit = (DateTime?)TempData["TestDDateEdit"];
            }
            if (TestDDateEdit == null)
            {
                TestDDateEdit = adjusteddate;
            }
            Session["TestDDateEdit"] = TestDDateEdit;
            //TempData["TrackerDateEdit"] = TrackerDateEdit;
            ViewBag.TestDDateEdit = TestDDateEdit;
            if (Request.IsAjaxRequest()) //IsCallback
                return PartialView();
            return View(); //otherwise load the view that loads the partialview + rootlayout. Like reloading whole page.
        }

        [ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin")]
        public ActionResult TestDetail_GridViewPartial(DateTime? TestDDateEdit)
        {

            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            //ViewBag.Category = new SelectList(db.t_Milling_UMQC_Category, "CategoryRecid", "CategoryDesc");
            ViewBag.Plant = new SelectList(db.t_Milling_Plant, "PlantRecid", "PlantDesc");
            ViewBag.Function = new SelectList(db.t_Milling_Function, "FuncRecid", "FuncDesc");
            ViewBag.Shift = new SelectList(db.t_Milling_Shift, "ShiftRecid", "ShiftDesc");
            ViewBag.Standards = new SelectList(db.t_Milling_UMQC_Results_Standards_Master, "ResultStdRecid", "ResultStdDescription");
            var RecipeList = recipeList();
            ViewData["Recipe"] = RecipeList.OrderBy(s => s.ProductMasterRecid).ToList().Select(s => new { ProductMasterRecid = s.ProductMasterRecid, RecipeDesc = s.RecipeDesc });
            var model = (from a in db.t_Milling_UMQC_Results_Detail
                         join b in db.t_Milling_UMQC_Results_Header on a.ResultHeaderrecid equals b.ResultHeaderRecid
                         where DbFunctions.TruncateTime(b.ResultCreationDate) >= DbFunctions.TruncateTime(TestDDateEdit)
                         select new resultDetailandHeaderViewModel
                         {
                             ResultDetailRecid = a.ResultDetailRecid,
                             ResultHeaderrecid = a.ResultHeaderrecid,
                             ResultMasterStdRecid = a.ResultMasterStdRecid,
                             ResultStdDetailRecid = a.ResultStdDetailRecid,
                             ResultMinScore = a.ResultMinScore,
                             ResultTargetScore = a.ResultTargetScore,
                             ResultMaxScore = a.ResultMaxScore,
                             ResultActualScore = a.ResultActualScore,
                             ResultComments = a.ResultComments,
                             ResultDetailLastChangedBy = a.ResultDetailLastChangedBy,
                             ResultDetailLastChangedDate = a.ResultDetailLastChangedDate,
                             ResultDetailActiveFlag = a.ResultDetailActiveFlag,
                             ResultProductRecid = b.ResultProductRecid,
                             ResultCreationDate = b.ResultCreationDate,
                             ResultPlantRecid = b.ResultPlantRecid,
                             ResultFuncRecid = b.ResultFuncRecid,
                             ResultShiftRecid = b.ResultShiftRecid,
                             ResultJobNo = b.ResultJobNo,
                             ResultBinNumber = b.ResultBinNumber,
                             ResultCreatedBy = b.ResultCreatedBy,
                             ResultLastChangedBy = b.ResultLastChangedBy,
                             ResultLastChangedDate = b.ResultLastChangedDate,
                             ResultActiveFlag = b.ResultActiveFlag,
                             ConcessionDesc = b.ConcessionDesc,
                             ResultTestTaken = b.ResultTestTaken,
                             WheatfeedBinNumber = b.WheatfeedBinNumber,
                             GristVersionNumber = b.GristVersionNumber,
                             plannedJobtonnage = b.plannedJobtonnage,
                             finalJobtonnage = b.finalJobtonnage,
                             ExcludeAverages = b.ExcludeAverages
                         }).OrderByDescending(s => s.ResultDetailRecid).ToList();

            TempData["TestDDateEdit"] = TestDDateEdit;
            ViewBag.TestDDateEdit = TestDDateEdit;

            return PartialView("_TestDetail_GridViewPartial", model);
        }

        public List<recipelist> recipeList()
        {
            //var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            //var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
            //var ProductCodesNonEDI = db.t_STG_SOP_Customer_Product_Data.Where(s => s.APLNumber == APLNumber).ToList().Select(s => new { productcode = s.Barcode, productDescription = string.Format("{0} - {1}", s.ItemNumber.Trim(), s.ItemDescription) });

            var model = (from a in db.t_Milling_UMQC_Products_Master
                         where a.RecipeActiveFlag == 1
                         select new recipelist
                         {
                             ProductMasterRecid = a.ProductMasterRecid,
                             RecipeDesc = a.RecipeCode.Trim() + " - " + a.RecipeDesc.Trim(),
                             RecipeCode = a.RecipeCode
                         }).ToList();

            return model;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult IndexShift()
        {
            return View();
        }

        [ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult ShiftGridViewPartial()
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            var model = db.t_Milling_Shift.OrderBy(s => s.ShiftDesc).ToList();
            return PartialView("_ShiftGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult ShiftGridViewPartialAddNew(HovisMillingPortal.Models.t_Milling_Shift item)
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            var model = db.t_Milling_Shift;
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
            var Newmodel = db.t_Milling_Shift.OrderBy(s => s.ShiftDesc).ToList();
            return PartialView("_ShiftGridViewPartial", Newmodel);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult ShiftGridViewPartialUpdate(HovisMillingPortal.Models.t_Milling_Shift item)
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");

            var model = db.t_Milling_Shift;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.ShiftRecid == item.ShiftRecid);
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
            var Newmodel = db.t_Milling_Shift.OrderBy(s => s.ShiftDesc).ToList();
            return PartialView("_ShiftGridViewPartial", Newmodel);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult ShiftGridViewPartialDelete(System.Int32 ShiftRecid)
        {
            var model = db.t_Milling_Shift;
            if (ShiftRecid >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.ShiftRecid == ShiftRecid);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var Newmodel = db.t_Milling_Shift.OrderBy(s => s.ShiftDesc).ToList();
            return PartialView("_ShiftGridViewPartial", Newmodel);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult IndexPlant()
        {
            return View();
        }

        [ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult PlantGridViewPartial()
        {
            ViewBag.Site = new SelectList(db.t_Milling_Site, "SiteRecid", "SiteDesc");
            ViewBag.Function = new SelectList(db.t_Milling_Function, "FuncRecid", "FuncDesc");
            var model = db.t_Milling_Plant.OrderBy(s => s.PlantDesc).ToList();
            return PartialView("_PlantGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult PlantGridViewPartialAddNew(HovisMillingPortal.Models.t_Milling_Plant item)
        {
            if (item.PlantFuncRecid == null)
            {
                ViewData["EditError"] = "You must choose a process.";
                ModelState.AddModelError("PlantFuncRecid", "You must choose a process.");
            }
            if (item.SiteRecid == null)
            {
                ViewData["EditError"] = "You must choose a site.";
                ModelState.AddModelError("SiteRecid", "You must choose a site.");
            }
            ViewBag.Site = new SelectList(db.t_Milling_Site, "SiteRecid", "SiteDesc");
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
            var Newmodel = db.t_Milling_Plant.OrderBy(s => s.PlantDesc).ToList();
            return PartialView("_PlantGridViewPartial", Newmodel);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult PlantGridViewPartialUpdate(HovisMillingPortal.Models.t_Milling_Plant item)
        {
            if (item.PlantFuncRecid == null)
            {
                ViewData["EditError"] = "You must choose a process.";
                ModelState.AddModelError("PlantFuncRecid", "You must choose a process.");
            }
            if (item.SiteRecid == null)
            {
                ViewData["EditError"] = "You must choose a site.";
                ModelState.AddModelError("SiteRecid", "You must choose a site.");
            }
            ViewBag.Site = new SelectList(db.t_Milling_Site, "SiteRecid", "SiteDesc");
            ViewBag.Function = new SelectList(db.t_Milling_Function, "FuncRecid", "FuncDesc");

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
            var Newmodel = db.t_Milling_Plant.OrderBy(s => s.PlantDesc).ToList();
            return PartialView("_PlantGridViewPartial", Newmodel);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult PlantGridViewPartialDelete(System.Int32 PlantRecid)
        {
            ViewBag.Site = new SelectList(db.t_Milling_Site, "SiteRecid", "SiteDesc");
            ViewBag.Function = new SelectList(db.t_Milling_Function, "FuncRecid", "FuncDesc");

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
            var Newmodel = db.t_Milling_Plant.OrderBy(s => s.PlantDesc).ToList();
            return PartialView("_PlantGridViewPartial", Newmodel);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult IndexSite()
        {
            return View();
        }

        [ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult SiteGridViewPartial()
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            var model = db.t_Milling_Site.OrderBy(s => s.SiteDesc).ToList();
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
            var Newmodel = db.t_Milling_Site.OrderBy(s => s.SiteDesc).ToList();
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
            var Newmodel = db.t_Milling_Site.OrderBy(s => s.SiteDesc).ToList();
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
            var Newmodel = db.t_Milling_Site.OrderBy(s => s.SiteDesc).ToList();
            return PartialView("_SiteGridViewPartial", Newmodel);
        }

        [Authorize(Roles = "Admin, UMQCAdmin")]
        public ActionResult IndexTestByFunction()
        {
            return View();
        }

        [ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin")]
        public ActionResult TestByFunctionGridViewPartial()
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            ViewBag.Function = new SelectList(db.t_Milling_Function, "FuncRecid", "FuncDesc");
            ViewBag.Standards = new SelectList(db.t_Milling_UMQC_Results_Standards_Master, "ResultStdRecid", "ResultStdDescription");
            var model = db.t_Milling_UMQC_Results_Standards_Master_ByFunction.OrderBy(s => s.ResultStdRecid).ToList();
            return PartialView("_TestByFunctionGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin")]
        public ActionResult TestByFunctionGridViewPartialAddNew(HovisMillingPortal.Models.t_Milling_UMQC_Results_Standards_Master_ByFunction item)
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            var model = db.t_Milling_UMQC_Results_Standards_Master_ByFunction;
            var alreadyexists = (from a in db.t_Milling_UMQC_Results_Standards_Master_ByFunction
                                 where a.ResultStdRecid == item.ResultStdRecid && a.ResultFuncRecid == item.ResultFuncRecid
                                 select new
                                 {
                                     a.ResultStdRecid
                                 }).FirstOrDefault();

            if (alreadyexists != null)
            {
                ViewData["EditError"] = "Record already exists.";
                ModelState.AddModelError("ResultStdRecid", "Record already exists.");
            }
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

            ViewBag.Function = new SelectList(db.t_Milling_Function, "FuncRecid", "FuncDesc");
            ViewBag.Standards = new SelectList(db.t_Milling_UMQC_Results_Standards_Master, "ResultStdRecid", "ResultStdDescription");
            var Newmodel = db.t_Milling_UMQC_Results_Standards_Master_ByFunction.OrderBy(s => s.ResultStdRecid).ToList();
            return PartialView("_TestByFunctionGridViewPartial", Newmodel);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin")]
        public ActionResult TestByFunctionGridViewPartialUpdate(HovisMillingPortal.Models.t_Milling_UMQC_Results_Standards_Master_ByFunction item)
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");

            var model = db.t_Milling_UMQC_Results_Standards_Master_ByFunction;
            var alreadyexists = (from a in db.t_Milling_UMQC_Results_Standards_Master_ByFunction
                                 where a.ResultStdRecid == item.ResultStdRecid && a.ResultFuncRecid == item.ResultFuncRecid
                                 select new
                                 {
                                     a.ResultStdRecid
                                 }).FirstOrDefault();

            if (alreadyexists != null)
            {
                ViewData["EditError"] = "Record already exists.";
                ModelState.AddModelError("ResultStdRecid", "Record already exists."); 
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.ResultFuncStdRecid == item.ResultFuncStdRecid);
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

            ViewBag.Function = new SelectList(db.t_Milling_Function, "FuncRecid", "FuncDesc");
            ViewBag.Standards = new SelectList(db.t_Milling_UMQC_Results_Standards_Master, "ResultStdRecid", "ResultStdDescription");
            var Newmodel = db.t_Milling_UMQC_Results_Standards_Master_ByFunction.OrderBy(s => s.ResultStdRecid).ToList();
            return PartialView("_TestByFunctionGridViewPartial", Newmodel);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin")]
        public ActionResult TestByFunctionGridViewPartialDelete(System.Int32 ResultFuncStdRecid)
        {
            var model = db.t_Milling_UMQC_Results_Standards_Master_ByFunction;
            if (ResultFuncStdRecid >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.ResultFuncStdRecid == ResultFuncStdRecid);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            ViewBag.Function = new SelectList(db.t_Milling_Function, "FuncRecid", "FuncDesc");
            ViewBag.Standards = new SelectList(db.t_Milling_UMQC_Results_Standards_Master, "ResultStdRecid", "ResultStdDescription");
            var Newmodel = db.t_Milling_UMQC_Results_Standards_Master_ByFunction.OrderBy(s => s.ResultStdRecid).ToList();
            return PartialView("_TestByFunctionGridViewPartial", Newmodel);
        }

        [Authorize(Roles = "Admin, UMQCAdmin, UMQCUser")]
        public ActionResult GenerateNew(System.Int64 resultheaderrecid)
        {

            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
            t_Milling_UMQC_Results_Header t_Milling_UMQC_Results_Header = new t_Milling_UMQC_Results_Header();

            var Masterdata = db.t_Milling_UMQC_Results_Header.Where(s => s.ResultHeaderRecid == resultheaderrecid).FirstOrDefault();

            t_Milling_UMQC_Results_Header.ResultActiveFlag = 1;
            t_Milling_UMQC_Results_Header.ResultRunStart = adjusteddate;
            t_Milling_UMQC_Results_Header.ResultRunEnd = adjusteddate;
            t_Milling_UMQC_Results_Header.ResultTestTaken = adjusteddate;

            t_Milling_UMQC_Results_Header.ResultLastChangedBy = User.Identity.Name;
            t_Milling_UMQC_Results_Header.ResultLastChangedDate = adjusteddate;
            t_Milling_UMQC_Results_Header.ResultCreatedBy = User.Identity.Name;
            t_Milling_UMQC_Results_Header.ResultCreationDate = adjusteddate;
            t_Milling_UMQC_Results_Header.ExcludeAverages = 0;
            t_Milling_UMQC_Results_Header.ResultPlantRecid = Masterdata.ResultPlantRecid;
            t_Milling_UMQC_Results_Header.ResultProductRecid = Masterdata.ResultProductRecid;
            t_Milling_UMQC_Results_Header.ResultShiftRecid = Masterdata.ResultShiftRecid;
            t_Milling_UMQC_Results_Header.WheatfeedBinNumber = Masterdata.WheatfeedBinNumber;
            t_Milling_UMQC_Results_Header.ResultJobNo = Masterdata.ResultJobNo;
            t_Milling_UMQC_Results_Header.ResultFuncRecid = Masterdata.ResultFuncRecid;
            t_Milling_UMQC_Results_Header.ResultBinNumber = Masterdata.ResultBinNumber;
            t_Milling_UMQC_Results_Header.plannedJobtonnage = Masterdata.plannedJobtonnage;
            t_Milling_UMQC_Results_Header.finalJobtonnage = Masterdata.finalJobtonnage;
            t_Milling_UMQC_Results_Header.GristVersionNumber = Masterdata.GristVersionNumber;
            t_Milling_UMQC_Results_Header.ConcessionDesc = Masterdata.ConcessionDesc;

            //To only show sites the user is authorised to
            string username = "";
            if (User.Identity.Name != null)
            {
                username = User.Identity.Name;
            }

            var Sitelist = (from a in db.t_Hovis_Milling_User_Site_Authorisation
                            join b in db.t_Milling_Site on a.SiteRecid equals b.SiteRecid
                            where a.UserName == username
                            select new
                            {
                                SiteRecid = a.SiteRecid,
                                SiteDesc = b.SiteDesc,
                                UserName = a.UserName
                            });

            var Plantlist = (from a in db.t_Milling_Plant
                             join b in Sitelist on a.SiteRecid equals b.SiteRecid
                             select new
                             {
                                 PlantRecid = a.PlantRecid,
                                 PlantDesc = a.PlantDesc
                             });


            ViewBag.Plant = new SelectList(Plantlist, "PlantRecid", "PlantDesc");
            ViewBag.Function = new SelectList(db.t_Milling_Function, "FuncRecid", "FuncDesc");
            ViewBag.Shift = new SelectList(db.t_Milling_Shift, "ShiftRecid", "ShiftDesc");
            var RecipeList = recipeList();
            ViewData["Recipe"] = RecipeList.OrderBy(s => s.ProductMasterRecid).ToList().Select(s => new { ProductMasterRecid = s.ProductMasterRecid, RecipeDesc = s.RecipeDesc });

            return View(t_Milling_UMQC_Results_Header);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin, UMQCAdmin, UMQCUser")]
        public ActionResult GenerateNew([ModelBinder(typeof(DevExpressEditorsBinder))] HovisMillingPortal.Models.t_Milling_UMQC_Results_Header item)
        {
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var adjusteddate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);


            if (item.ResultProductRecid == null)
            {
                ModelState.AddModelError("ResultProductRecid", "You must choose a flour type and Item");
            }
            if (item.ResultPlantRecid == null)
            {
                ModelState.AddModelError("ResultPlantRecid", "You must choose a Plant");
            }

            var model = db.t_Milling_UMQC_Results_Header;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db.SaveChanges();
                    db.usp_Milling_UMQC_Results_Detail_Creation(item.ResultHeaderRecid);
                    return RedirectToAction("TestDetailIndex", "UMQC", new { resultheaderrecid = item.ResultHeaderRecid });
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            //To only show sites the user is authorised to
            string username = "";
            if (User.Identity.Name != null)
            {
                username = User.Identity.Name;
            }

            var Sitelist = (from a in db.t_Hovis_Milling_User_Site_Authorisation
                            join b in db.t_Milling_Site on a.SiteRecid equals b.SiteRecid
                            where a.UserName == username
                            select new
                            {
                                SiteRecid = a.SiteRecid,
                                SiteDesc = b.SiteDesc,
                                UserName = a.UserName
                            });

            var Plantlist = (from a in db.t_Milling_Plant
                             join b in Sitelist on a.SiteRecid equals b.SiteRecid
                             select new
                             {
                                 PlantRecid = a.PlantRecid,
                                 PlantDesc = a.PlantDesc
                             });

            ViewBag.Plant = new SelectList(Plantlist, "PlantRecid", "PlantDesc");
            ViewBag.Function = new SelectList(db.t_Milling_Function, "FuncRecid", "FuncDesc");
            ViewBag.Shift = new SelectList(db.t_Milling_Shift, "ShiftRecid", "ShiftDesc");
            var RecipeList = recipeList();
            ViewData["Recipe"] = RecipeList.OrderBy(s => s.ProductMasterRecid).ToList().Select(s => new { ProductMasterRecid = s.ProductMasterRecid, RecipeDesc = s.RecipeDesc });

            return View(item);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult IndexAssignDefaultSite()
        {
            return View();
        }

        [ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult AssignDefaultSiteGridViewPartial()
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            var model = db.t_Hovis_Milling_User_Default_Site.OrderBy(s => s.UserName).ToList();
            ViewBag.Site = new SelectList(db.t_Milling_Site, "SiteRecid", "SiteDesc");
            return PartialView("_AssignDefaultSiteGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult AssignDefaultSiteGridViewPartialAddNew(HovisMillingPortal.Models.t_Hovis_Milling_User_Default_Site item)
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");
            var model = db.t_Hovis_Milling_User_Default_Site;
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

            ViewBag.Site = new SelectList(db.t_Milling_Site, "SiteRecid", "SiteDesc");
            var Newmodel = db.t_Hovis_Milling_User_Default_Site.OrderBy(s => s.UserName).ToList();
            return PartialView("_AssignDefaultSiteGridViewPartial", Newmodel);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult AssignDefaultSiteGridViewPartialUpdate(HovisMillingPortal.Models.t_Hovis_Milling_User_Default_Site item)
        {
            //ViewBag.Company = new SelectList(db.t_SOP_Company, "CompRecid", "Compcode");

            var model = db.t_Hovis_Milling_User_Default_Site;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.defaultSiteRecid == item.defaultSiteRecid);
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

            ViewBag.Site = new SelectList(db.t_Milling_Site, "SiteRecid", "SiteDesc");
            var Newmodel = db.t_Hovis_Milling_User_Default_Site.OrderBy(s => s.UserName).ToList();
            return PartialView("_AssignDefaultSiteGridViewPartial", Newmodel);
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult AssignDefaultSiteGridViewPartialDelete(System.Int32 defaultSiteRecid)
        {
            var model = db.t_Hovis_Milling_User_Default_Site;
            if (defaultSiteRecid >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.defaultSiteRecid == defaultSiteRecid);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            ViewBag.Site = new SelectList(db.t_Milling_Site, "SiteRecid", "SiteDesc");
            var Newmodel = db.t_Hovis_Milling_User_Default_Site.OrderBy(s => s.UserName).ToList();
            return PartialView("_AssignDefaultSiteGridViewPartial", Newmodel);
        }

        public List<AuthorisedSiteVM> AuthorisedSite(string username)
        {
                var usersite = (from a in db.t_Hovis_Milling_User_Default_Site
                                join b in db.t_Milling_Site on a.SiteRecid equals b.SiteRecid
                                where a.UserName == username
                                select new
                                {
                                    SiteRecid = a.SiteRecid,
                                    SiteDesc = b.SiteDesc,
                                    UserName = a.UserName
                                });

                var Sitelist = (from a in db.t_Hovis_Milling_User_Site_Authorisation
                            join b in db.t_Milling_Site on a.SiteRecid equals b.SiteRecid
                            where a.UserName == username
                            select new
                            {
                                SiteRecid = a.SiteRecid,
                                SiteDesc = b.SiteDesc,
                                UserName = a.UserName
                            });

            var model = (from a in db.t_Milling_Plant
                             join b in Sitelist on a.SiteRecid equals b.SiteRecid
                             select new AuthorisedSiteVM
                             {
                                 PlantRecid = a.PlantRecid,
                                 SiteRecid = b.SiteRecid,
                                 PlantDesc = a.PlantDesc
                             }).ToList();
 
            return model;
        }

        public List<DefaultSiteVM> DefaultSite(string username)
        {
            var model = (from a in db.t_Hovis_Milling_User_Default_Site
                            join b in db.t_Milling_Site on a.SiteRecid equals b.SiteRecid
                            where a.UserName == username
                            select new DefaultSiteVM
                            {
                                SiteRecid = a.SiteRecid,
                                SiteDesc = b.SiteDesc,
                                UserName = a.UserName
                            }).ToList();

             return model;
        }

        public List<DefaultPlantVM> DefaultPlant(int? siteno)
        {

            var model = (from a in db.t_Milling_Plant
                         join b in db.t_Milling_Site on a.SiteRecid equals b.SiteRecid
                         where a.SiteRecid == siteno
                         select new DefaultPlantVM
                         {
                             PlantRecid = a.PlantRecid,
                             SiteRecid = b.SiteRecid,
                             PlantDesc = a.PlantDesc
                         }).ToList();

            return model;
        }

        public List<DefaultFunctionVM> DefaultFunctionList(int? siteno)
        {

            var model = (from a in db.t_Milling_Function
                         join b in db.t_Milling_Plant on a.FuncRecid equals b.PlantFuncRecid
                         where b.SiteRecid == siteno
                         group a by new { FuncRecid = a.FuncRecid, FuncDesc = a.FuncDesc } into g
                         select new DefaultFunctionVM
                         {
                             FuncRecid = g.Key.FuncRecid,
                             FuncDesc = g.Key.FuncDesc
                         }).ToList();

            return model;
        }

        public List<DefaultPlantVM> DefaultPlantchoice(int? siteno, int? funcno)
        {

            var model = (from a in db.t_Milling_Plant
                         join b in db.t_Milling_Site on a.SiteRecid equals b.SiteRecid
                         where a.SiteRecid == siteno && a.PlantFuncRecid == funcno
                         select new DefaultPlantVM
                         {
                             PlantRecid = a.PlantRecid,
                             SiteRecid = b.SiteRecid,
                             PlantDesc = a.PlantDesc
                         }).ToList();

            return model;
        }

    }
}