Library Management System


It is a console application which was build to help with keeping track of book records and of eventual book loans.

For data persistency I used a SQLite database, which I configured in an XML file called "App.Config" should specify the path
towards the database also the code for creating the database can be found in a file called "sqliteScript.txt".
I've used net9.0 for the project environment.

For starting the application the Main function from "Program.cs" file should be run, there is the pre-made setup of the application.
After running the application a menu with options should appear on the console, representing multiple options with suggestive names.
Upon choosing an instruction to be executed, the user should write the number corresponding to the wanted option, then if needed, 
complete the information required further.

    The menu consists of:

          1. Add a new book.
                       ----> adds a new book to the collection
          2. Remove a book.
                        ----> removes a book from the collection
          3. Search book by title.
                        ----> displays all the book records which have the same title
          4. Search book by author.
                        ----> displays all book records written by the same author
          5. Search book by title and author.
                        ----> displays details about a book with a given title and author
          6. Search book by genre.
                        ----> display all book records having the same genre
          7. View all books.
                        ----> display all book records
          8. Make a new loan.
                        ----> performs a new loan to a reader;
                              if there are not enough books in stock to complete a loan,
                              a corresponding message will be displayed;
                              if the reader requesting the loan is new and has never done
                              a loan before, then their details will be saved for future
                              transactions;
          9. Remove a loan
                        ----> removes a loan;
                              if the stock for said book is already full the app will prevent
                              a loan from happening and will display a corresponding message;
                              for identifying the one to be deleted it requires besides the
                              book details, an identification for the person (for the moment, 
                              their phone number was chosen as such)
                              after completing the removing, a rating for the book will
                              be given 
          10. Get a book's rating.
                        ----> displays the rating of a book, which represents the average
                              of all ratings given by readers who returned the book
          11. Get a random book recomandation based on genre.
                        ----> gives a random book recomandation based on a chosen genre
          12. Exit.
                         ---->stops the application

In case of invalid data inputs or other problems, the app will display a corresponding message.

