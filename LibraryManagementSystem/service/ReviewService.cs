using LibraryManagementSystem.model;
using LibraryManagementSystem.repository;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LibraryManagementSystem.service;

public class ReviewService
{
    private IReviewRepository reviewRepository;
    private IBookRepository bookRepository;
    public ReviewService(IReviewRepository reviewRepository, IBookRepository bookRepository)
    {
        this.reviewRepository = reviewRepository;
        this.bookRepository = bookRepository;
    }

    public void saveReview(string nameBook, string nameAuthor, float rating)
    {
        Book book = bookRepository.findOneByTitleAndAuthor(nameBook, nameAuthor);
        if (book != null)
        {
            reviewRepository.save(new Review(book.getId(), rating));
        }
        else
        {
            throw new Exception("Book not found");
        }
    }

    public float getRatingForBook(string bookTitle, string bookAuthor)
    {
        Book book = bookRepository.findOneByTitleAndAuthor(bookTitle, bookAuthor);
        if (book != null)
        {
            IList<Review> reviews = reviewRepository.getAllReviewsForBook(book.getId());
            var mean = reviews.Average(r=>r.getRating());
            return mean;
        }
        else
        {
            throw new Exception("Book not found");
        }
    }
}