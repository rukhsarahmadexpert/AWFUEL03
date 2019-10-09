﻿using IT.Core.ViewModels;
using IT.Repository.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace IT.Web.Controllers
{
    public class AwfVehicleController : Controller
    {

        WebServices webServices = new WebServices();
        List<VehicleViewModel> vehicleViewModels = new List<VehicleViewModel>();
        VehicleViewModel vehicleViewModel = new VehicleViewModel();
        List<VehicleTypeViewModel> vehicleTypeViewModels = new List<VehicleTypeViewModel>();

        public List<DriverViewModel> VehicleViewModel { get; private set; }
        public List<VehicleViewModel> VehicleViewModels { get; private set; }


        public ActionResult Index()
        {
            PagingParameterModel pagingParameterModel = new PagingParameterModel();

            pagingParameterModel.pageNumber = 1;
            pagingParameterModel._pageSize = 1;
            pagingParameterModel.CompanyId = 2;
            pagingParameterModel.pageSize = 100;

            var VehicleList = webServices.Post(pagingParameterModel, "AWFVehicle/All");

            if (VehicleList.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                VehicleViewModels = (new JavaScriptSerializer().Deserialize<List<VehicleViewModel>>(VehicleList.Data.ToString()));
            }

            return View(VehicleViewModels);
        }

        public ActionResult Details(int id)
        {
            try
            {

                vehicleViewModel.CompanyId = 2;
                vehicleViewModel.Id = id;

                var result = webServices.Post(vehicleViewModel, "AWFVehicle/Edit");
                if (result.Data != null)
                {
                    vehicleViewModel = (new JavaScriptSerializer()).Deserialize<List<VehicleViewModel>>(result.Data.ToString()).FirstOrDefault();
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return View(vehicleViewModel);
        }
    }
}
