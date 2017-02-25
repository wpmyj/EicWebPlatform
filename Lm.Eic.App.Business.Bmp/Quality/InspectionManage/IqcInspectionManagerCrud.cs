﻿using Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
 internal  class IqcInspectionManagerCrudFactory
    {
        /// <summary>
        /// 检验方式配置CRUD
        /// </summary>
        public static InspectionModeConfigCrud InspectionModeConfigCrud
        {
            get { return OBulider.BuildInstance<InspectionModeConfigCrud>(); }
        }
        /// <summary>
        /// IQC物料检验配置CRUD
        /// </summary>
        public static InspectionItemConfigCrud InspectionItemConfigCrud
        {
            get { return OBulider.BuildInstance<InspectionItemConfigCrud>(); }
        }
        /// <summary>
        /// 物料检验项次CRUD
        /// </summary>
        public static IqcInspectionMasterCrud IqcInspectionMasterCrud
        {
            get { return OBulider.BuildInstance<IqcInspectionMasterCrud>(); }
        }
        /// <summary>
        ///  物料检验项次数据CRUD
        /// </summary>
        public static IqcInspectionDetailCrud IqcInspectionDetailCrud
        {
            get { return OBulider.BuildInstance<IqcInspectionDetailCrud>(); }
        }
    }


    #region  IQC
    /// <summary>
    /// IQC物料检验配置
    /// </summary>
    public  class InspectionItemConfigCrud : CrudBase<IqcInspectionItemConfigModel, IIqcInspectionItemConfigRepository>
    {
        public InspectionItemConfigCrud():base(new IqcInspectionItemConfigRepository (),"IQC物料检验配置")
            { }
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddInspectionItemConfig);
            this.AddOpItem(OpMode.Edit, EidtInspectionItemConfig);
            this.AddOpItem(OpMode.Delete, DeleteInspectionItemConfig);
        }

        private OpResult DeleteInspectionItemConfig(IqcInspectionItemConfigModel model)
        {
            OpResult opResult = OpResult.SetResult(OpContext);
            opResult= irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
            opResult.Attach = model;
            return opResult;
        }

        private OpResult EidtInspectionItemConfig(IqcInspectionItemConfigModel model)
        {
            OpResult opResult = OpResult.SetResult(OpContext);
            opResult= irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
            opResult.Attach = model;
            return opResult;
        }

        private OpResult AddInspectionItemConfig(IqcInspectionItemConfigModel model)
        {

            OpResult opResult = OpResult.SetResult(OpContext);
            opResult= irep.Insert(model).ToOpResult_Add(OpContext);
            opResult.Attach = model;
            return opResult;
        }




        public bool IsExistInspectionConfigItem(string materialId, string inspectionItem)
        {
            return irep.IsExist(e => e.MaterialId == materialId && e.InspectionItem == inspectionItem);
        }
        public List<IqcInspectionItemConfigModel> FindIqcInspectionItemConfigsBy(string materialId)
        {
            return irep.Entities.Where(e => e.MaterialId == materialId).OrderBy(e => e.InspectionItemIndex).ToList();
        }
        public OpResult AddInspectionItemConfiList(List<IqcInspectionItemConfigModel> modelList)
        {
            SetFixFieldValue(modelList, OpMode.Add);
            //如果存在 就修改   然后从列表中剔除 最后批量加入 （册除就直接册除）
            modelList.ForEach((m) =>
            {
                if (IsExistInspectionConfigItem(m.MaterialId,m.InspectionItem))
                {
                    m.OpSign = "Eidt";
                    this.Store(m);
                    modelList.Remove(m);
                }
            });
            OpResult opResult = OpResult.SetResult("未执行任何操作！");
            opResult = irep.Insert(modelList).ToOpResult_Add(OpContext);
            opResult.Attach = modelList;

            return opResult;

           
        }
        public int GetInspectionIndex(string materialId)
        {
            var listEntities = FindIqcInspectionItemConfigsBy(materialId);
            if (listEntities == null || listEntities.Count <= 0) return 0;
            return listEntities.Select(e => e.InspectionItemIndex).Max()+1;
        }
    }




    /// <summary>
    /// 物料检验项次
    /// </summary>
    internal class IqcInspectionMasterCrud : CrudBase<IqcInspectionMasterModel, IIqcInspectionMasterRepository>
    {
        public IqcInspectionMasterCrud() : base(new IqcInspectionMasterRepository(), "物料检验")
        {
        }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddIqcInspectionMaster);
            this.AddOpItem(OpMode.Edit, EidtIqcInspectionMaster);
            this.AddOpItem(OpMode.Delete, DeleteIqcInspectionMaster);
        }

        private OpResult DeleteIqcInspectionMaster(IqcInspectionMasterModel model)
        {
            return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
        }

        private OpResult EidtIqcInspectionMaster(IqcInspectionMasterModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }

        private OpResult AddIqcInspectionMaster(IqcInspectionMasterModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
    }


    /// <summary>
    /// 物料检验项次数据
    /// </summary>
    internal class IqcInspectionDetailCrud : CrudBase<IqcInspectionDetailModel, IIqcInspectionDetailRepository>
    {
        public IqcInspectionDetailCrud() : base(new IqcInspectionDetailRepository(), "物料检验项次数据")
        {
        }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddIqcInspectionDetail);
            this.AddOpItem(OpMode.Edit, EidtIqcInspectionDetail);
            this.AddOpItem(OpMode.Delete, DeleteIqcInspectionDetail);
        }

        private OpResult DeleteIqcInspectionDetail(IqcInspectionDetailModel model)
        {
            return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
        }

        private OpResult EidtIqcInspectionDetail(IqcInspectionDetailModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }

        private OpResult AddIqcInspectionDetail(IqcInspectionDetailModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
    }

    #endregion


}
