﻿using AstraLearnAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace AstraLearnAPI.Controllers
{
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly ExerciseRepository _exerciseRepository;

        public ExerciseController(IConfiguration configuration)
        {
            _exerciseRepository = new ExerciseRepository(configuration);
        }

        [HttpGet("[controller]/GetAllExercises")]
        public ActionResult<ResponseModel> GetAllExercises(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _exerciseRepository.GetAllData(id);
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }

        [HttpGet("[controller]/GetExercise")]
        public ActionResult<ResponseModel> GetExercise(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _exerciseRepository.GetData(id);
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }

        [HttpPost("[controller]/InsertExercise")]
        public ActionResult<ResponseModel> InsertExercise([FromBody] ExerciseModel exerciseModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _exerciseRepository.InsertData(exerciseModel);
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

        [HttpPost("[controller]/UpdateExercise")]
        public ActionResult<ResponseModel> UpdateExercise([FromBody] ExerciseModel exerciseModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _exerciseRepository.UpdateData(exerciseModel);
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

        [HttpPost("[controller]/DeleteExercise")]
        public ActionResult<ResponseModel> DeleteExercise(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _exerciseRepository.DeleteData(id);
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
