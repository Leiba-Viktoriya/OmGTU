using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary
{
    public struct BookInfo : IEquatable<BookInfo>
    {
        public string AuthorName;
        public string BookTitle;
        public int PublicationYear;
        public string PublishingHouse;

        public bool Equals(BookInfo other)
        {
            return AuthorName == other.AuthorName && BookTitle == other.BookTitle;
        }

        public override bool Equals(object obj)
        {
            return obj is BookInfo other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AuthorName, BookTitle);
        }

        public static bool operator ==(BookInfo left, BookInfo right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BookInfo left, BookInfo right)
        {
            return !left.Equals(right);
        }
    }

    public struct LoanRecord
    {
        public BookInfo Book;
        public DateTime BorrowDate;
        public DateTime? DueDate;
    }

    class LibraryCatalog
    {
        private List<BookInfo> bookCollection = new List<BookInfo>();
        private List<LoanRecord> loanHistory = new List<LoanRecord>();

        public void AddBook(BookInfo book)
        {
            bookCollection.Add(book);
        }

        public void IssueBook(BookInfo book, string issueDateString)
        {
            if (!DateTime.TryParse(issueDateString, out DateTime issueDate))
            {
                Console.WriteLine($"Неверный формат даты: {issueDateString}\n");
                return;
            }

            bool alreadyIssued = loanHistory.Any(record => record.Book.Equals(book) && record.DueDate == null);
            if (!bookCollection.Contains(book))
            {
                Console.WriteLine($"Книга \"{book.BookTitle}\" отсутствует в каталоге!\n");
            }
            else if (alreadyIssued)
            {
                Console.WriteLine($"Книга \"{book.BookTitle}\" уже выдана!\n");
            }
            else
            {
                loanHistory.Add(new LoanRecord { Book = book, BorrowDate = issueDate, DueDate = null });
            }
        }

        public void ReturnBook(BookInfo book, string returnDateString)
        {
            if (!DateTime.TryParse(returnDateString, out DateTime returnDate))
            {
                Console.WriteLine($"Неверный формат даты: {returnDateString}\n");
                return;
            }

            var existingRecord = loanHistory.LastOrDefault(record => record.Book.Equals(book) && record.DueDate == null);

            if (existingRecord.Equals(default(LoanRecord)))
            {
                Console.WriteLine($"Книга \"{book.BookTitle}\" не числится в выданных или уже возвращена!\n");
            }
            else
            {
                int index = loanHistory.IndexOf(existingRecord);
                LoanRecord updatedRecord = existingRecord;
                updatedRecord.DueDate = returnDate;
                loanHistory[index] = updatedRecord;
            }
        }

        public void DisplayNeverBorrowedBooks()
        {
            Console.WriteLine("Книги, которые еще не выдавались:");
            var neverBorrowed = bookCollection.Where(book => !loanHistory.Any(record => record.Book.Equals(book)));

            foreach (var book in neverBorrowed)
            {
                Console.WriteLine($"{book.AuthorName} - \"{book.BookTitle}\" ({book.PublicationYear})");
            }
        }

        public void DisplayOverdueBooks()
        {
            Console.WriteLine("Книги, которые не возвращены:");
            var overdueBooks = loanHistory.Where(record => record.DueDate == null);

            foreach (var record in overdueBooks)
            {
                Console.WriteLine($"\"{record.Book.BookTitle}\" - выдана: {record.BorrowDate.ToShortDateString()}");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            LibraryCatalog library = new LibraryCatalog();

            BookInfo theHobbit = new BookInfo
            {
                AuthorName = "Джон Р.Р. Толкин",
                BookTitle = "Хоббит, или Туда и обратно",
                PublicationYear = 1937,
                PublishingHouse = "Allen & Unwin"
            };
            BookInfo dune = new BookInfo
            {
                AuthorName = "Фрэнк Герберт",
                BookTitle = "Дюна",
                PublicationYear = 1965,
                PublishingHouse = "Chilton Books"
            };
            BookInfo foundation = new BookInfo
            {
                AuthorName = "Айзек Азимов",
                BookTitle = "Основание",
                PublicationYear = 1951,
                PublishingHouse = "Gnome Press"
            };

            library.AddBook(theHobbit);
            library.AddBook(dune);
            library.AddBook(foundation);

            library.IssueBook(theHobbit, "2024-01-15");
            library.IssueBook(dune, "2024-02-01");

            library.ReturnBook(dune, "2024-02-15");
            library.ReturnBook(foundation, "2023-12-25");

            library.IssueBook(dune, "2024-03-01");
            library.IssueBook(theHobbit, "invalid date");

            library.DisplayNeverBorrowedBooks();
            Console.WriteLine();
            library.DisplayOverdueBooks();
        }
    }
}