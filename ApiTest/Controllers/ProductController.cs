using ApiTest.Contracts;
using ApiTest.Contracts.Model;
using ApiTest.Entity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductContracts _productRepository;
        public ProductController(IProductContracts productRepository)
        {
            this._productRepository = productRepository;
        }

        /// <summary>
        /// GetProducts : Get All Products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetProducts([FromQuery] PaginationFilterPoco filter)
        {
            try
            {
                return Ok(await _productRepository.GetProducts(filter));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// CreateProduct : Create a New Product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ProductPoco>> CreateProduct([FromBody] ProductPoco product)
        {
            try
            {
                if (product == null)
                    return BadRequest();

                //Check if AnyProduct Available in the Database.
                bool isAvailable = await _productRepository.isProductExist(product.ProductName);
                if (isAvailable)
                {
                    return StatusCode(StatusCodes.Status200OK,
                    "Already Product Exist, Please Enter Other Product Details.");
                }
                var createdProduct = await _productRepository.AddProduct(product);
                return CreatedAtAction(nameof(GetProduct),
                    new { id = createdProduct.ProductId }, createdProduct);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Product record");
            }
        }

        /// <summary>
        /// GetProduct : Retrive Product using productId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductPoco>> GetProduct([FromRoute] int id)
        {
            try
            {
                var result = await _productRepository.GetProduct(id);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Id: {id}, Could Not Found");
                };
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// UpdateProduct : update the exising Records
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductPoco>> UpdateProduct([FromRoute] int id, [FromBody] ProductPoco product)
        {
            try
            {
                if (id != product.ProductId)
                    return BadRequest("Product ID mismatch");
                var productToUpdate = await _productRepository.GetProduct(id);
                if (productToUpdate == null)
                    return NotFound($"Product with Id = {id} not found");
                return await _productRepository.UpdateProduct(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        /// <summary>
        /// DeleteProduct : Remove the Records from the Database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductPoco>> DeleteProduct([FromRoute] int id)
        {
            try
            {
                var productToDelete = await _productRepository.GetProduct(id);
                if (productToDelete == null)
                {
                    return NotFound($"Porduct with Id = {id} not found");
                }
                int result = await _productRepository.DeleteProduct(id);
                string Msg = "";
                Msg = result == 1 ? $"Product with Id = {id} Deleted Successfully." : $"Product with Id = {id} Could Not able to Delete.";
                return Ok(Msg);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateProductPatch([FromRoute] int id, [FromBody] JsonPatchDocument<ProductPoco> product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest();
                }

                //Retrieve the Exisitng Item.
                ProductPoco result = await _productRepository.GetProduct(id);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                   "No Data Found.");
                }
                //Apply the Patch
                product.ApplyTo(result);

                //Update in the Database.
                ProductPoco data = await _productRepository.UpdateProduct(result);
                return StatusCode(StatusCodes.Status200OK,
                    $"Data Updated Successfully\n{JsonConvert.SerializeObject(data)}");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }
    }
}
