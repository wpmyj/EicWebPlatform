﻿using Lm.Eic.App.Erp.Bussiness.CopManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers.Product
{
    /// <summary>
    /// 2016-10-17
    /// 排程管理子模块
    /// 工单管理控制器
    /// </summary>
    public class ProMocManageController : EicBaseController
    {
        //
        // GET: /MocManage/

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 订单工单比对视图
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckOrderBills()
        {
            return View();
        }

        /// <summary>
        /// 获取校验清单
        /// </summary>
        /// <param name="planDate"></param>
        /// <returns></returns>
        [HttpGet]
        [NoAuthenCheck]
        public JsonResult GetMS589ProductTypeMonitor(string department)
        {
            var datas =  CopService.OrderManageManager.GetMS589ProductTypeMonitor();
            return Json(datas, JsonRequestBehavior.AllowGet);
        }


        [NoAuthenCheck]
        public FileResult CreateProductTypeMonitoList()
        {
            var ds = CopService.OrderManageManager.BuildProductTypeMonitoList() ;
            return this.ExportToExcel(ds, "工单核对清单", "工单核对清单");
        }
    }
}