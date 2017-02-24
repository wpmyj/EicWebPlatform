﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.Business.Bmp.Quality.InspectionManage;
namespace EicWorkPlatfrom.Controllers
{
    public class QuaInspectionManageController : EicBaseController
    {
        //
        // GET: /QuaInspectionManage/

        public ActionResult Index()
        {
            return View();
        }

        #region IQC
        /// <summary>
        /// IQC检验项目配置
        /// </summary>
        /// <returns></returns>
        public ActionResult IqcInspectionItemConfiguration()
        {
            return View();
        }
        [NoAuthenCheck]
        public ActionResult InspectionDataGatheringOfIQC()
        {
            return View();
        }
        [NoAuthenCheck]

        public JsonResult GetMaterialDatas(string materialId)
        {
            var datas = InspectionService.InspectionItemConfigurator.GetIqcspectionItemConfigBy(materialId);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
