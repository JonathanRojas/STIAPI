using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CamaraDeDiputadosLibrary.Application;
using CamaraDeDiputadosLibrary.Commons;
using TipoFactory = STIModel.TipoFactory;

namespace DemoAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TipoController : Controller
    {
        [HttpGet]
        public Response<List<Tipo>> TipoSistemaGetItems()
        {
            Response<List<Tipo>> vResult = TipoFactory.TipoSistemaGetItems();
            return vResult;
        }

        [HttpGet]
        public Response<List<Tipo>> TipoEscritorioVirtualGetItems()
        {
            Response<List<Tipo>> vResult = TipoFactory.TipoEscritorioVirtualGetItems();
            return vResult;
        }


        [HttpGet]
        public Response<List<Tipo>> TipoBaseDatosGetItems()
        {
            Response<List<Tipo>> vResult = TipoFactory.TipoBaseDatosGetItems();
            return vResult;
        }

        [HttpGet]
        public Response<List<Tipo>> TipoEstadoDesarrolloGetItems()
        {
            Response<List<Tipo>> vResult = TipoFactory.TipoEstadoDesarrolloGetItems();
            return vResult;
        }

        [HttpGet]
        public Response<List<Tipo>> TipoUsuarioGetItems()
        {
            Response<List<Tipo>> vResult = TipoFactory.TipoUsuarioGetItems();
            return vResult;
        }

        [HttpGet]
        public Response<List<Tipo>> TipoIISGetItems()
        {
            Response<List<Tipo>> vResult = TipoFactory.TipoIISGetItems();
            return vResult;
        }

        [HttpGet]
        public Response<List<Tipo>> TipoFrameworkWebGetItems()
        {
            Response<List<Tipo>> vResult = TipoFactory.TipoFrameworkWebGetItems();
            return vResult;
        }

        [HttpGet]
        public Response<List<Tipo>> TipoFrameworkNetGetItems()
        {
            Response<List<Tipo>> vResult = TipoFactory.TipoFrameworkNetGetItems();
            return vResult;
        }
    }
}
