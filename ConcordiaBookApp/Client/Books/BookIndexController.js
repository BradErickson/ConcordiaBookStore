angular.module('bookApp')
.controller("BookIndexController", BookIndexController)

function BookIndexController($scope, BookService) {
    getBooks();
    function getBooks() {
        var book = BookService.getBooks().then(function (data) {
            $scope.books = data;
        });
    }

    $scope.delete = function (bookId) {
        BookService.deleteBooks(bookId).then(function (data) {
            console.log("Data: ", data);
            getBooks();
        })
    }

    $scope.rentBook = function (bookId) {
        BookService.rentBook(bookId).then(function (data) {
            console.log("Data: ", data);
            getBooks();
        })
    }

}