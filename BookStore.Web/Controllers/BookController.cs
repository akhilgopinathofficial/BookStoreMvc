using AutoMapper;
using BookStore.BL.BusinessModels;
using BookStore.BL.Interfaces;
using BookStore.Web.Models;
using IronPdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;
        public BookController(IMapper mapper, IBookService bookService)
        {
            _mapper = mapper;
            _bookService = bookService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Book()
        {
            return View();
        }

        /// <summary>
        /// Creates book information
        /// </summary>
        /// <param name="vmBook">view model to create book</param>
        /// <returns>Return created object and redirect to book list</returns>
        [HttpPost]
        public async Task<ActionResult> CreateUpdate(BookViewModel vmBook)
        {
            try
            {
                var bookDto = _mapper.Map<BookDto>(vmBook);
                await _bookService.AddBook(bookDto);
                return Json(new { Message = "success" });
            }
            catch (Exception e)
            {
                return Json(new { Message = "failed" });
            }
        }

        /// <summary>
        /// Get list of all books
        /// </summary>
        /// <param name=""></param>
        /// <returns>return list of view model with books</returns>
        [HttpGet]
        public async Task<ActionResult> GetBookList(bool filterDeleted)
        {
            try
            {
                var result = await _bookService.GetAllBooks(filterDeleted);
                var dtoList = _mapper.Map<List<BookViewModel>>(result);
                return Json(new { Data = dtoList, Message = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { Data = "", Message = "failed" });
            }
        }

        /// <summary>
        /// Get the edit view for book with edit model
        /// </summary>
        /// <param name="id">id of book detail to edit</param>
        /// <returns>Returns edit view with edit model bind on it</returns>
        [HttpPost]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var result = await GetBookById(id);
                return Json(new { Data = result, Message = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "failed" });
            }
        }

        /// <summary>
        /// Get the edit view for book with edit model
        /// </summary>
        /// <param name="id">id of book detail to edit</param>
        /// <returns>Returns edit view with edit model bind on it</returns>
        [HttpPost]
        public async Task<ActionResult> View(int id)
        {
            try
            {
                var result = await GetBookById(id);
                return Json(new { Data = result, Message = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "failed" });
            }
        }

        /// <summary>
        /// Get book by Id
        /// </summary>
        /// <param name="id">id of book detail to edit</param>
        /// <returns>Returns book</returns>
        public async Task<BookViewModel> GetBookById(int id)
        {
            try
            {
                var result = await _bookService.GetBookById(id);
                var vmBook = _mapper.Map<BookViewModel>(result);
                return vmBook;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Delete book information of specific id provided
        /// </summary>
        /// <param name="id">Id of book to delete</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _bookService.DeleteBook(id);
                return Json(new { Data = "", Message = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "failed" });
            }
        }

        /// <summary>
        /// Download pdf file for Book against the id passed
        /// </summary>
        /// <param name="id">Id of book to generate file</param>
        /// <returns></returns>
        public async Task<ActionResult> DownloadPDFSample(int id)
        {
            var result = await GetBookById(id);
            var pdfHTMLString = GetPDFHTMLString(result);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Sample.pdf");
            System.IO.File.Delete(path);
            var renderer = new HtmlToPdf();
            renderer.RenderHtmlAsPdf(pdfHTMLString).SaveAs("Sample.pdf");
            var stream = new FileStream(@"Sample.pdf", FileMode.Open);
            return File(stream, "application/pdf", "BookDetails.pdf");
        }

        private string GetPDFHTMLString(BookViewModel bookData)
        {
            var htmlString = "<h1>BookDetails</h1><br/><h4>Book Code</h4><p>"+ bookData.Code + "<p><br/>";
            htmlString += "<h4>Book Name</h4><p>" + bookData.Name + "<p><br/>";
            htmlString += "<h4>Price</h4><p>" + bookData.Price + "<p><br/>";
            htmlString += "<h4>Is Available</h4><p>" + bookData.IsAvailable + "<p><br/>";
            htmlString += "<h4>Shelf ID</h4><p>" + bookData.ShelfId + "<p><br/>";
            return htmlString;
        }
    }
}
