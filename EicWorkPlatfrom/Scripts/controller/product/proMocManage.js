﻿/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/print/print.min.js" />
/// <reference path="../../common/angulee.js" />
/// <reference path="../../angular.min.js" />
/// <reference path="E:\杨垒 含系统\Project\EicWebPlatform\EicWorkPlatfrom\Content/pdfmaker/pdfmake.js" />

var productModule = angular.module('bpm.productApp');
productModule.factory('MocDataOpService', function (ajaxService) {
    var urlPrefix = "/" + leeHelper.controllers.mocManage + "/";
    var mocDataOp = {};
    //-------------------------工单管理-------------------------------------
    //待填写
    mocDataOp.getProductFlowList = function (department, productName, orderId, searchMode) {
        var url = urlPrefix + 'GetProductFlowList';
        return ajaxService.getData(url, {
            department: department,
            productName: productName,
            orderId: orderId,
            searchMode: searchMode,
        });
    };
});