﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.App.Erp.Bussiness.CopManage;

namespace Lm.Eic.App.Business.Bmp.Ast.Tests
{
    [TestClass()]
    public class EquipmentMaintenanceManagerTests
    {
        [TestMethod]
        public void AutoTest()
        {
            GetWaitingMaintenanceListByTest();
            BuildWaitingMaintenanceListTest();
        }

        [TestMethod()]
        public void GetWaitingMaintenanceListByTest()
        {
            string planMaintenanceDate = "201607";
            var waitingMainteanceList = AstService.EquipmentManager.MaintenanceManager.GetWaitingMaintenanceListBy(planMaintenanceDate);
            if (waitingMainteanceList.Count() > 0) { } else { Assert.Fail(); }

        }

        [TestMethod()]
        public void BuildWaitingMaintenanceListTest()
        {
            var tem = AstService.EquipmentManager.MaintenanceManager.BuildWaitingMaintenanceList();


            #region 输出到Excel
            string path = @"E:\\IQC.xls";
            using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                byte[] bArr = tem.ToArray();
                fs.Write(bArr, 0, bArr.Length);
                fs.Flush();
            }
            #endregion
            Assert.Fail();

        }
        public void BuildInventoryList()
        {
            var tem = AstService.EquipmentManager.BuildInventoryList();


            #region 输出到Excel
            //string path = @"E:\\IQC.xls";
            //using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            //{
            //    byte[] bArr = tem.ToArray();
            //    fs.Write(bArr, 0, bArr.Length);
            //    fs.Flush();
            //}
            #endregion
            Assert.Fail();
        }

        [TestMethod()]
        public void MaintenanceStoreTest()
        {
            //ceshi 
            EquipmentMaintenanceRecordModel model = new EquipmentMaintenanceRecordModel();
            model.AssetNumber = "Z160001";
            model.MaintenanceDate = DateTime.Now.ToDate();
            model.MaintenanceResult = "";
            model.OpSign = "add";
            var tem = AstService.EquipmentManager.MaintenanceManager.Store(model);
            if (!tem.Result) { Assert.Fail(); }
        }
    }
}