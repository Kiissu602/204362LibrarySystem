using _204362LibrarySystem.Models;
using _204362LibrarySystem.Models.DTO;
using LibrarySystem.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Services
{
    public class BookService
    {
        private readonly DatabaseContext _context;
        private readonly ImageService _imageService;

        public BookService(DatabaseContext context, ImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }
        public Book Post([FromForm]AddBookDTO book)
        {
            var publisher = new Publisher { PublisherName = book.PublisherName };
            _context.Attach(publisher);
            var category = new Category { CategoryID = book.CategoryID };
            _context.Attach(category);
            string Img = _imageService.SaveImg(book.Image);

            var newBook = new Book
            {
                BookImgUrl = Img,
                ISBN = book.ISBN,
                Title = book.Title,
                Category = category,
                Publisher = publisher,
                PublicationDate = book.PublicationDate,
                Edition = book.Edition,
                Pagination = book.Pagination,
                Price = book.Price,
                Plot = book.Plot,
            };

            return newBook;
        }
    }
}
