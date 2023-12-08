using AstraLearnAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace AstraLearnAPI.Controllers
{
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly SectionRepository _sectionRepository;

        public SectionController(IConfiguration configuration)
        {
            _sectionRepository = new SectionRepository(configuration);
        }

        [HttpGet("[controller]/GetAllSections")]
        public ActionResult<ResponseModel> GetAllSections()
        {
                ResponseModel responseModel = new ResponseModel();
                try
                {
                    responseModel.message = "Berhasil";
                    responseModel.status = 200;
                    responseModel.data = _sectionRepository.GetAllData();
                }
                catch (Exception ex)
                {
                    responseModel.message = ex.Message;
                    responseModel.status = 500;
                }
                return Ok(responseModel);
        }

        [HttpGet("[controller]/GetAllSectionsById")]
        public ActionResult<ResponseModel> GetAllSections2(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _sectionRepository.GetDataByPelatihan(id);
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }

        [HttpGet("[controller]/GetSection")]
        public ActionResult<ResponseModel> GetSection(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _sectionRepository.GetData(id);
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }

        [HttpPost("[controller]/InsertSection")]
        public ActionResult<ResponseModel> InsertSection([FromBody] SectionModel sectionModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _sectionRepository.InsertData(sectionModel);
                responseModel.message = "Data berhasil ditambahkan";
                responseModel.status = 200;
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }

        [HttpPost("[controller]/UpdateSection")]
        public ActionResult<ResponseModel> UpdateSection([FromBody] SectionModel sectionModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _sectionRepository.UpdateData(sectionModel);
                responseModel.message = "Data berhasil diupdate";
                responseModel.status = 200;
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }

        [HttpPost("[controller]/DeleteSection")]
        public ActionResult<ResponseModel> DeleteSection(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _sectionRepository.DeleteData(id);
                responseModel.message = "Data berhasil dihapus";
                responseModel.status = 200;
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }
    }
}
