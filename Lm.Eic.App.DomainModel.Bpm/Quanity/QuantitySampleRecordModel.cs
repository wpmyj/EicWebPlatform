﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.DomainModel.Bpm.Quanity
{   
    /// <summary>
    /// IQC物料抽样模块 
    /// 对应表 QCMS_IQCSampleRecordTable
    /// </summary>
    public class IQCSampleRecordModel
    {
      public IQCSampleRecordModel()
		{}
		#region Model
		private string _orderid;
		private string _samplematerial;
		private string _samplematerialname;
		private string _samplematerialspec;
		private string _samplematerialsupplier;
		private DateTime? _samplematerialindate;
		private string _samplematerialdrawid;
		private decimal? _samplematerialnumber;
		private string _checkway;
		private int? _samplenumber;
		private int? _samplebadnumber;
		private decimal? _sampleratio;
		private string _sampleresult;
		private string _badreanson;
		private string _samplepersons;
		private DateTime? _finishdate;
		private DateTime? _inputdate;
		private decimal _id_key;
		/// <summary>
		/// 
		/// </summary>
		public string OrderID
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleMaterial
		{
			set{ _samplematerial=value;}
			get{return _samplematerial;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleMaterialName
		{
			set{ _samplematerialname=value;}
			get{return _samplematerialname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleMaterialSpec
		{
			set{ _samplematerialspec=value;}
			get{return _samplematerialspec;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleMaterialSupplier
		{
			set{ _samplematerialsupplier=value;}
			get{return _samplematerialsupplier;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? SampleMaterialInDate
		{
			set{ _samplematerialindate=value;}
			get{return _samplematerialindate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleMaterialDrawID
		{
			set{ _samplematerialdrawid=value;}
			get{return _samplematerialdrawid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? SampleMaterialNumber
		{
			set{ _samplematerialnumber=value;}
			get{return _samplematerialnumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CheckWay
		{
			set{ _checkway=value;}
			get{return _checkway;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SampleNumber
		{
			set{ _samplenumber=value;}
			get{return _samplenumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SampleBadNumber
		{
			set{ _samplebadnumber=value;}
			get{return _samplebadnumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? SampleRatio
		{
			set{ _sampleratio=value;}
			get{return _sampleratio;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleResult
		{
			set{ _sampleresult=value;}
			get{return _sampleresult;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BadReanson
		{
			set{ _badreanson=value;}
			get{return _badreanson;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SamplePersons
		{
			set{ _samplepersons=value;}
			get{return _samplepersons;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FinishDate
		{
			set{ _finishdate=value;}
			get{return _finishdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? InPutDate
		{
			set{ _inputdate=value;}
			get{return _inputdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Id_key
		{
			set{ _id_key=value;}
			get{return _id_key;}
		}
		#endregion Model
    }
   
    /// <summary>
    /// IQC物料抽样项目模块（用于打印内容）
    /// 对应表 QCMS_IQCPrintSampleTable
    /// </summary>
    public class IQCSampleItemRecordModel
    {

        public IQCSampleItemRecordModel()
		{}
		#region Model
		private string _orderid;
		private string _samplematerial;
		private string _samplematerialname;
		private string _samplematerialspec;
		private string _samplematerialsupplier;
		private string _samplematerialdrawid;
		private double ? _samplematerialnumber;
		private DateTime? _samplematerialindate;
		private string _sampleitem;
		private string _equipmentid;
		private string _checkmethod;
		private string _checklevel;
		private string _grade;
		private string _checkway;
		private string _sizespec;
		private string _sizespecup;
		private string _sizespecdown;
		private double ? _acceptgradenumber;
		private double ? _checknumber;
		private double ? _refusegradenumber;
		private int? _printcount;
		private decimal _id_key;
		/// <summary>
		/// 
		/// </summary>
		public string OrderID
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleMaterial
		{
			set{ _samplematerial=value;}
			get{return _samplematerial;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleMaterialName
		{
			set{ _samplematerialname=value;}
			get{return _samplematerialname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleMaterialSpec
		{
			set{ _samplematerialspec=value;}
			get{return _samplematerialspec;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleMaterialSupplier
		{
			set{ _samplematerialsupplier=value;}
			get{return _samplematerialsupplier;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleMaterialDrawID
		{
			set{ _samplematerialdrawid=value;}
			get{return _samplematerialdrawid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double ? SampleMaterialNumber
		{
			set{ _samplematerialnumber=value;}
			get{return _samplematerialnumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? SampleMaterialInDate
		{
			set{ _samplematerialindate=value;}
			get{return _samplematerialindate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleItem
		{
			set{ _sampleitem=value;}
			get{return _sampleitem;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EquipmentID
		{
			set{ _equipmentid=value;}
			get{return _equipmentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CheckMethod
		{
			set{ _checkmethod=value;}
			get{return _checkmethod;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CheckLevel
		{
			set{ _checklevel=value;}
			get{return _checklevel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Grade
		{
			set{ _grade=value;}
			get{return _grade;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CheckWay
		{
			set{ _checkway=value;}
			get{return _checkway;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SizeSpec
		{
			set{ _sizespec=value;}
			get{return _sizespec;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SizeSpecUP
		{
			set{ _sizespecup=value;}
			get{return _sizespecup;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SizeSpecDown
		{
			set{ _sizespecdown=value;}
			get{return _sizespecdown;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double ? AcceptGradeNumber
		{
			set{ _acceptgradenumber=value;}
			get{return _acceptgradenumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double ? CheckNumber
		{
			set{ _checknumber=value;}
			get{return _checknumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double ? RefuseGradeNumber
		{
			set{ _refusegradenumber=value;}
			get{return _refusegradenumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PrintCount
		{
			set{ _printcount=value;}
			get{return _printcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Id_key
		{
			set{ _id_key=value;}
			get{return _id_key;}
		}
		#endregion Model

    }

   
    public class ProductNgRecordModel
    {
        public ProductNgRecordModel()
        { }
        #region Model
        private string _orderid;
        private string _samplematerial;
        private string _samplematerialname;
        private string _samplematerialspec;
        private string _samplematerialsupplier;
        private DateTime? _samplematerialindate;
        private string _samplematerialdrawid;
        private double? _samplematerialnumber;
        private string _checkway;
        private int? _samplenumber;
        private int? _samplebadnumber;
        private double ? _sampleratio;
        private string _sampleresult;
        private string _badreanson;
        private string _samplepersons;
        private string _resultdoway;
        private string _specialorderid;
        private int? _fullcheckworktime;
        private DateTime? _finishdate;
        private DateTime? _inputdate;
        private string _memo;
        private decimal _id_key;
        /// <summary>
        /// 
        /// </summary>
        public string OrderID
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SampleMaterial
        {
            set { _samplematerial = value; }
            get { return _samplematerial; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SampleMaterialName
        {
            set { _samplematerialname = value; }
            get { return _samplematerialname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SampleMaterialSpec
        {
            set { _samplematerialspec = value; }
            get { return _samplematerialspec; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SampleMaterialSupplier
        {
            set { _samplematerialsupplier = value; }
            get { return _samplematerialsupplier; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SampleMaterialInDate
        {
            set { _samplematerialindate = value; }
            get { return _samplematerialindate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SampleMaterialDrawID
        {
            set { _samplematerialdrawid = value; }
            get { return _samplematerialdrawid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double ? SampleMaterialNumber
        {
            set { _samplematerialnumber = value; }
            get { return _samplematerialnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckWay
        {
            set { _checkway = value; }
            get { return _checkway; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SampleNumber
        {
            set { _samplenumber = value; }
            get { return _samplenumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SampleBadNumber
        {
            set { _samplebadnumber = value; }
            get { return _samplebadnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? SampleRatio
        {
            set { _sampleratio = value; }
            get { return _sampleratio; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SampleResult
        {
            set { _sampleresult = value; }
            get { return _sampleresult; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BadReanson
        {
            set { _badreanson = value; }
            get { return _badreanson; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SamplePersons
        {
            set { _samplepersons = value; }
            get { return _samplepersons; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ResultDoWay
        {
            set { _resultdoway = value; }
            get { return _resultdoway; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SpecialOrderId
        {
            set { _specialorderid = value; }
            get { return _specialorderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FullCheckWorkTime
        {
            set { _fullcheckworktime = value; }
            get { return _fullcheckworktime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? FinishDate
        {
            set { _finishdate = value; }
            get { return _finishdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? InPutDate
        {
            set { _inputdate = value; }
            get { return _inputdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Id_key
        {
            set { _id_key = value; }
            get { return _id_key; }
        }
        #endregion Model
    }
    /// <summary>
    /// 设置物料抽测项次
    /// </summary>
  
    public class  MaterialSampleSet
    {
        public MaterialSampleSet ()
        { }
        #region Model
        private string _samplematerial;
        private string _sampleitem;
        private string _sizespec;
        private string _sizespecup;
        private string _sizespecdown;
        private string _equipmetnid;
        private string _checkmethod;
        private string _checkstandard;
        private string _checkway;
        private string _checklevel;
        private string _grade;
        private string _department;
        private string _sampleclass;
        private string _materiaattribute;
        private int? _prioritylevel;
        private string _producttype;
        private decimal _id_key;
        /// <summary>
        /// 
        /// </summary>
        public string SampleMaterial
        {
            set { _samplematerial = value; }
            get { return _samplematerial; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SampleItem
        {
            set { _sampleitem = value; }
            get { return _sampleitem; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SizeSpec
        {
            set { _sizespec = value; }
            get { return _sizespec; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SizeSpecUP
        {
            set { _sizespecup = value; }
            get { return _sizespecup; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SizeSpecDown
        {
            set { _sizespecdown = value; }
            get { return _sizespecdown; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EquipmetnID
        {
            set { _equipmetnid = value; }
            get { return _equipmetnid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckMethod
        {
            set { _checkmethod = value; }
            get { return _checkmethod; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckStandard
        {
            set { _checkstandard = value; }
            get { return _checkstandard; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckWay
        {
            set { _checkway = value; }
            get { return _checkway; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckLevel
        {
            set { _checklevel = value; }
            get { return _checklevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Grade
        {
            set { _grade = value; }
            get { return _grade; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SampleClass
        {
            set { _sampleclass = value; }
            get { return _sampleclass; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MateriaAttribute
        {
            set { _materiaattribute = value; }
            get { return _materiaattribute; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PriorityLevel
        {
            set { _prioritylevel = value; }
            get { return _prioritylevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductType
        {
            set { _producttype = value; }
            get { return _producttype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Id_key
        {
            set { _id_key = value; }
            get { return _id_key; }
        }
        #endregion Model
    }
     
}