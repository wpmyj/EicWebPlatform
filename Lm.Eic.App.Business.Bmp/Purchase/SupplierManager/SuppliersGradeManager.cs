﻿using Lm.Eic.App.DomainModel.Bpm.Purchase;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Purchase.SupplierManager
{
    internal class GradeManagerFactory
    {
        /// <summary>
        /// 供应商稽核评分管理
        /// </summary>
        public static SuppliersGradeManager SuppliersGradeManager
        {
            get { return OBulider.BuildInstance<SuppliersGradeManager>(); }
        }
    }
    /// <summary>
    /// 供应商稽核评分管理
    /// </summary>
    public class SuppliersGradeManager
    {

        public List<SupplierGradeInfoModel> GetPurSupGradeInfoBy(string yearQuarter)
        {

            return SupplierCrudFactory.SupplierGradeInfoCrud.GetPurSupGradeInfoBy(yearQuarter);
        }

        public OpResult SavePurSupGradeData(SupplierGradeInfoModel entity)
        {
            if (entity == null) return OpResult.SetResult("数据不为空", false);
            entity.GradeYear = entity.FirstGradeDate.Year.ToString();
            entity.ParameterKey = entity.SupplierId + "&" + entity.GradeYear + "&" + entity.SupGradeType;
            return SupplierCrudFactory.SupplierGradeInfoCrud.Store(entity);
        }
    }
}
