using HRSolutions.BusinessLayer;
using HRSolutionsCore.RequestModel;
using HRSolutionsCore.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace HRSolutionsCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MasterController : ControllerBase
    {
        //New Chages fffff
        public readonly MasterBusiness _masterBusiness;
        public MasterController(MasterBusiness masterBusiness)
        {
            _masterBusiness = masterBusiness;
        }
        #region CategoryMaster

        //Add new category
        [HttpPost]
        [Route("Category")]
        public async Task<IActionResult> Category(MasterCategoryModel req)
        {
            try
            {
                //Validating request body

                var validator = new MasterCategoryValidator();
                var validationResult = await validator.ValidateAsync(req);
                if (!validationResult.IsValid)
                {
                    return BadRequest(new ErrorResponseModel { error = new ErrorModel { code = "400", message = "Validation error", innerError = validationResult.Errors.Select(z => z.ErrorMessage) } });
                }
                var response = _masterBusiness.AddCategory(req);
                if (response.Success == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("Category")]
        public IActionResult Category()
        {
            try
            {
                var response = _masterBusiness.GetCategory();
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch]
        [Route("ActivateCategory")]
        public async Task<IActionResult> ActivateCategory([FromBody] CategoryStatusModel req)
        {
            //Validating request body
            var validator = new CategoryStatusModelValidator();
            var validationResult = await validator.ValidateAsync(req);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ErrorResponseModel { error = new ErrorModel { code = "400", message = "Validation error", innerError = validationResult.Errors.Select(z => z.ErrorMessage) } });
            }
            var response = _masterBusiness.ChangeCategoryStatus(req);
            if (response.Success)
            {
                return Ok(response);
            }
            return Ok();
        }
        [HttpDelete]
        [Route("Category")]
        public IActionResult DeleteCategory(int id)
        {
     
            try
            {
                if (id == 0)
                {
                    return BadRequest("Id is required");
                }
                var response = _masterBusiness.DeleteCategory(id);
                if (response.Success == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region SubCategoryMaster

        [HttpPost]
        [Route("SubCategory")]
        public async Task<IActionResult> SubCategory(MasterSubCategoryModel req)
        {
            try
            {
                //Validating request body
                var validator = new MasterSubCategoryValidator();
                var validationResult = await validator.ValidateAsync(req);
                if (!validationResult.IsValid)
                {
                    return BadRequest(new ErrorResponseModel { error = new ErrorModel { code = "400", message = "Validation error", innerError = validationResult.Errors.Select(z => z.ErrorMessage) } });
                }
                var response = _masterBusiness.AddSubCategory(req);
                if (response.Success == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("SubCategory")]
        public IActionResult SubCategory()
        {
            try
            {
                var response = _masterBusiness.GetSubCategory();
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("ActiveCategory")]
        public IActionResult ActiveCategory()
        {
            try
            {
                var response = _masterBusiness.GetActiveCategory();
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch]
        [Route("ActivateSubCategory")]
        public async Task<IActionResult> ActivateSubCategory([FromBody] SubCategoryStatusModel req)
        {
            //Validating request body
            var validator = new SubCategoryStatusModelValidator();
            var validationResult = await validator.ValidateAsync(req);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ErrorResponseModel { error = new ErrorModel { code = "400", message = "Validation error", innerError = validationResult.Errors.Select(z => z.ErrorMessage) } });
            }
            var response = _masterBusiness.ActivateSubCategory(req);
            if (response.Success)
            {
                return Ok(response);
            }
            return Ok();
        }
        #endregion
    }
}