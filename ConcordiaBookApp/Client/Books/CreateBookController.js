angular.module('bookApp')
.controller("AddBookController", AddBookController)

function AddBookController($scope, BookService) {
    getBooks();
    function getBooks() {
        var book = BookService.getBooks().then(function (data) {
            debugger;
            $scope.books = data;
        });
    }

}