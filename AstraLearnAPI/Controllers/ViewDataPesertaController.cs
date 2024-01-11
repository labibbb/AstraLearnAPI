using AstraLearnAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace AstraLearnAPI.Controllers
{
    [ApiController]
    public class ViewDataPesertaController : Controller
    {
        private readonly ViewDataPesertaRepository _viewDataPesertaRepository;

        public ViewDataPesertaController(IConfiguration configuration)
        {
            _viewDataPesertaRepository = new ViewDataPesertaRepository(configuration);
        }

        [HttpGet("[controller]/GetAllDataPesertas")]
        public ActionResult<ResponseModel> GetAllDataPesertas(int id_pengguna)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _viewDataPesertaRepository.GetAllData(id_pengguna);
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
