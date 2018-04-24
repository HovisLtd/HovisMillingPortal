using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.ComponentModel.DataAnnotations;

namespace HovisMillingPortal.ViewModels
{
    public class recipelist
    {
        public long? ProductMasterRecid { get; set; }
        public string RecipeCode { get; set; }
        public string RecipeDesc { get; set; }
        public string ItemCode { get; set; }
        public string ItemDesc { get; set; }
        public int RecipeActiveFlag { get; set; }
 
    }

    public class resultDetailViewModel
    {
        public long? ResultDetailRecid { get; set; }
        public long? ResultHeaderrecid { get; set; }
        public int? ResultMasterStdRecid { get; set; }
        public long? ResultStdDetailRecid { get; set; }
        public double? ResultMinScore { get; set; }
        public double? ResultTargetScore { get; set; }
        public double? ResultMaxScore { get; set; }
        public double? ResultActualScore { get; set; }
        public string ResultComments { get; set; }
        public string ResultDetailLastChangedBy { get; set; }
        public DateTime? ResultDetailLastChangedDate { get; set; }
        public int? ResultDetailActiveFlag { get; set; }
        public string ConcessionDesc { get; set; }
        public DateTime? ResultTestTaken { get; set; }
    }

    public class resultDetailandHeaderViewModel
    {
        public long? ResultDetailRecid { get; set; }
        public long? ResultHeaderrecid { get; set; }
        public int? ResultMasterStdRecid { get; set; }
        public long? ResultStdDetailRecid { get; set; }
        public double? ResultMinScore { get; set; }
        public double? ResultTargetScore { get; set; }
        public double? ResultMaxScore { get; set; }
        public double? ResultActualScore { get; set; }
        public string ResultComments { get; set; }
        public string ResultDetailLastChangedBy { get; set; }
        public DateTime? ResultDetailLastChangedDate { get; set; }
        public int? ResultDetailActiveFlag { get; set; }
        public long? ResultProductRecid { get; set; }
        public DateTime? ResultCreationDate { get; set; }
        public int? ResultPlantRecid { get; set; }
        public int? ResultFuncRecid { get; set; }
        public int? ResultShiftRecid { get; set; }
        public string ResultJobNo { get; set; }
        public string ResultBinNumber { get; set; }
        public string ResultCreatedBy { get; set; }
        public string ResultLastChangedBy { get; set; }
        public DateTime? ResultLastChangedDate { get; set; }
        public int? ResultActiveFlag { get; set; }
        public string ConcessionDesc { get; set; }
        public DateTime? ResultTestTaken { get; set; }
        public string WheatfeedBinNumber { get; set; }
        public string GristVersionNumber { get; set; }
        public double? plannedJobtonnage { get; set; }
        public double? finalJobtonnage { get; set; }
        public int? ExcludeAverages { get; set; }
    }

}
